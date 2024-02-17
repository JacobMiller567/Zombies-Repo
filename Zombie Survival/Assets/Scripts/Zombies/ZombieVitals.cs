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
    private Animator animate;
    private NavMeshAgent Agent; // TEST
    private bool isDead = false;
    private float speed;
    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>(); // TEST
        currentHealth = RunTimeMaxHealth;
        animate = GetComponent<Animator>();
        speed = Agent.speed;
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            animate.SetTrigger("Hit");
        // Agent.speed = 0; // TEST

            if (currentHealth <= 0)
            {
                PlayerVitals.instance?.IncreaseMoney(moneyDropped);
                PlayerVitals.instance?.IncreaseKillAmount();
                ZombieSpawner.onZombieKilled.Invoke();  // Call listener in Spawner
                animate.SetBool("isDead", true);
                isDead = true;
                Destroy(gameObject, 0.8f); // Death Animation
            }
        }
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
    }



}
