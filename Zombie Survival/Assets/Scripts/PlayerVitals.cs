using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerVitals : MonoBehaviour, Damage
{
    public static PlayerVitals instance;

    public float currentHealth;
    public float maxHealth;
    public int money;
    public int killAmount;
    public TextMeshProUGUI moneyText;

    private bool recentlyDamaged = false;
    [SerializeField] private float regenDelay = 5f;
    [SerializeField] private float regenRate = 5f;
    [SerializeField] private float regenAmount = .1f; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        currentHealth = maxHealth;
        InvokeRepeating("RegenerateHealth", regenDelay, regenRate);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        DamageIndicator.instance.DisplayScreenDamage(currentHealth);
        recentlyDamaged = true;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("GameOver");
            Time.timeScale = 0;
        }
        moneyText.text = money.ToString();
    }

    public void IncreaseMoney(int moneyDropped)
    {
        money += moneyDropped;
    }

    public void IncreaseHealth(int health)
    {
        maxHealth += health;
    }

    public void IncreaseKillAmount()
    {
        killAmount++;
    }

    private void RegenerateHealth()
    {
        if (!recentlyDamaged)
        {
            float newHealth = currentHealth + (maxHealth * regenAmount);
            currentHealth = Mathf.Min(newHealth, maxHealth);
        }
        recentlyDamaged = false;
        DamageIndicator.instance.DisplayScreenDamage(currentHealth);
    }
}
