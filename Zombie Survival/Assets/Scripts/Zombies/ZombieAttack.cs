using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour, Damage
{
    [SerializeField] private Animator animator;
    [SerializeField] private float attackDamage = 20f;

    /// <summary>
    /// If zombies hand hits the player when the attack animation is still playing then damage the player
    /// </summary>
    /// 

    public void TakeDamage(float damage){}

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player")) && animator.GetBool("AttackEvent") == true)
        {
            if (animator.GetBool("isDead") == true)
            {
                attackDamage = 0f;
            }
            Damage player = other.gameObject.transform.GetComponent<Damage>();
           // other.gameObject.GetComponent<PlayerVitals>()?.TakeDamage(attackDamage);
            player?.TakeDamage(attackDamage);
        }
    }
}
