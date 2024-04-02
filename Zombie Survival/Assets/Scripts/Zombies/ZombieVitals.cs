using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieVitals : MonoBehaviour, Damage
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float RunTimeMaxHealth;
    [SerializeField] private int moneyDropped;
    [SerializeField] private GameObject bloodLocation;
    [SerializeField] private GameObject nukeDrop;
    private Animator animate;
    private bool isDead = false;
    private float speed;
    private int randomNumer;
    private void Start()
    {
        currentHealth = RunTimeMaxHealth;
        animate = GetComponent<Animator>();
    }
    private void OnEnable() 
    {
        randomNumer = Random.Range(0, 100);
        GetComponent<Collider>().enabled = true;
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            // ADD: If not already hit (play hit animation OR make animation restarts from the beginning) 
            animate.SetTrigger("Hit");

            if (currentHealth <= 0)
            {
                if (randomNumer == 62)
                {
                    Debug.Log("SUMMON NUKE");
                    Instantiate(nukeDrop, transform.position, Quaternion.identity);
                }
                PlayerVitals.instance?.IncreaseMoney(moneyDropped);
                PlayerVitals.instance?.IncreaseKillAmount();
                ZombieSpawner.onZombieKilled.Invoke();  // Call listener in Spawner
                animate.SetBool("isDead", true);
                isDead = true;
                StartCoroutine(DeathDelay());
            }
        }
    }

    IEnumerator DeathDelay()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(.65f);
        GameObject bloodSplatter = BloodSplatterPool.Instance.GetBloodSplatter(); // spawn blood splatter from object pool
        bloodSplatter.transform.position = bloodLocation.transform.position; 
        bloodSplatter.transform.rotation = bloodLocation.transform.rotation;
        bloodSplatter.SetActive(true);
        yield return new WaitForSeconds(.15f);
        ZombiePoolManager.Instance.ReturnZombie(this); // return zombie to object pool
    }

    public void IncreaseHealth(int amount)
    {
        RunTimeMaxHealth += amount;
        currentHealth += amount;
    }

    public void ResetHealth()
    {
        RunTimeMaxHealth = maxHealth;
        currentHealth = maxHealth;
        isDead = false;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }



}
