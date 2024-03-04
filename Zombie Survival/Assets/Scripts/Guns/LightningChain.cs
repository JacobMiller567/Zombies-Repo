using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightningChain : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public int maxHits; // Number of enemies bolt can hit
    [SerializeField] private int rangeBetweenTargets; // How far bolts can travel between enemies
    [SerializeField] private LayerMask enemyMask;

    public List<Transform> hitEnemies;
    private Transform target;
    private int numHits = 0; 

    private void Start() 
    {
        hitEnemies = new List<Transform>();
    }

    public void SetTarget(Transform _target)
    {
       target = _target;
    }

    private void FixedUpdate()
    {
        if (numHits >= maxHits) // if max number of enemies have been hit
        {
            Destroy(gameObject);
            return;
        }

        if (!target) return;

        Vector3 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;

        StartCoroutine(MissedShot());
    }

    void OnCollisionEnter(Collision other)
    {
        if (other == null || other.gameObject == null || other.transform == null || other.gameObject.transform == null) return;

        if (other.gameObject.CompareTag("Zombie") && !hitEnemies.Contains(other.transform))
        {
            ZombieVitals enemy = other.transform.GetComponent<ZombieVitals>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                hitEnemies.Add(other.transform);
                numHits++;
                NextTarget(); 
            }
        }
    }


    private void NextTarget() // Detect enemies inside of circle radius and go towards next closest enemy
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, rangeBetweenTargets, Vector3.up, 0f, enemyMask);
        float minDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy") && !hitEnemies.Contains(hit.transform))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = hit.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            SetTarget(closestEnemy.transform); // Change target to nearest enemy
        }
    }
        

    IEnumerator MissedShot()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
