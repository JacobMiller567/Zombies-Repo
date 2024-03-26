using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    public int level;
    [SerializeField] private float damage;
    [SerializeField] private float updateRate = 0.3f;
    [SerializeField] private int maxAmmo = 8;
    [SerializeField] private float reloadSpeed = 1.5f;
    [SerializeField] private float fireRate = 0.75f;
    [SerializeField] private float turnSpeed = 75f;
    [SerializeField] private float wanderRadius = 5f;
    [SerializeField] private float wanderSpeed = 1.75f;


    [SerializeField] private Transform muzzle;
    [SerializeField] private ParticleSystem muzzleFlash;
    

    private Animator animator;
    private int runtimeLevel;
    private float runtimeDamage;
    private int runtimeMaxAmmo;
    private float runtimeFireRate;
    private int currentAmmo;
    private bool isReloading = false;
    private bool lockedOnTarget = false;
    private GameObject currentEnemy;
    private NavMeshAgent Agent;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        runtimeLevel = level;
        runtimeDamage = damage;
        runtimeMaxAmmo = maxAmmo;
        runtimeFireRate = fireRate;
        currentAmmo = maxAmmo;
        StartCoroutine(Wandering()); // TEST
    }

    void FixedUpdate() 
    {
        if (lockedOnTarget)
        {
           transform.LookAt(currentEnemy.transform.position);
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag("Zombie") && !lockedOnTarget)
        {
            lockedOnTarget = true;
            currentEnemy = other.gameObject;
            StartCoroutine(AimTowardsEnemy(other.gameObject));
            //animator.SetBool("inCombat", true);
            Agent.stoppingDistance = 3;
            InvokeRepeating("Shoot", 0f, fireRate);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie") && other.gameObject == currentEnemy)
        {
            lockedOnTarget = false;
            currentEnemy = null;
            CancelInvoke("Shoot");
            //Agent.enabled = true; // Enable agent
            Agent.stoppingDistance = 1;
            //animator.SetBool("inCombat", false);
        }
    }

    private IEnumerator AimTowardsEnemy(GameObject zombie)
    {
        WaitForSeconds wait = new WaitForSeconds(updateRate);
        //Agent.enabled = false; // Disable agent
        while (lockedOnTarget)
        {
            if (zombie == null || currentEnemy == null || !currentEnemy.activeSelf)
            {
                lockedOnTarget = false;
                CancelInvoke("Shoot");
                //Agent.enabled = true; // Enable agent
                animator.SetBool("inCombat", false);
                Agent.stoppingDistance = 1;
                break;
            }

            Agent.SetDestination(currentEnemy.transform.position);
            yield return wait;
        }
    }
    private void Shoot()
    {
        if (currentAmmo < 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
        if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo) && !isReloading)
        {
            if (hitInfo.collider.CompareTag("Zombie")) 
            {
                if (muzzleFlash.isPlaying)
                { 
                    muzzleFlash.Stop();
                }   
                muzzleFlash.Play();
                Damage damagable = hitInfo.transform.GetComponent<Damage>();
                damagable?.TakeDamage(damage);
                currentAmmo--;
            }
        }
    }


    private IEnumerator Wandering() // TEST
    {
        WaitForSeconds wait = new WaitForSeconds(updateRate);
        Agent.speed = wanderSpeed;
        while (true)
        {
            if (!Agent.enabled || !Agent.isOnNavMesh)
            {
                yield return wait;
            }
            else if (Agent.remainingDistance <= Agent.stoppingDistance && !lockedOnTarget)
            {
                Vector2 point = Random.insideUnitCircle * wanderRadius; // Random point in unit circle multiplied by radius
                NavMeshHit hit;
                if (NavMesh.SamplePosition(Agent.transform.position + new Vector3(point.x, 0, point.y), out hit, 3f, Agent.areaMask))
                {
                    Agent.SetDestination(hit.position);
                }
            }
            yield return wait;
        }
    }


    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = runtimeMaxAmmo;
        isReloading = false;
    }

    public void UpgradeSoldier()
    {
        if (runtimeLevel <= 3)
        {
            runtimeLevel++;
            runtimeDamage++;
            runtimeMaxAmmo += 5;
            runtimeFireRate -= 0.1f;
        }
    }


}
