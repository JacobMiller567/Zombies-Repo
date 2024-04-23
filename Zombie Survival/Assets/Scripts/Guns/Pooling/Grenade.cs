using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float explosionRange;
    [SerializeField] private float explosionTime;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject explosionPrefab;
    private void Start()
    {
        StartCoroutine(Thrown()); // Change to once player has thrown grenade
    }
    private IEnumerator Thrown()
    {
        yield return new WaitForSeconds(explosionTime);
        SplashArea();
    }

    private void SplashArea()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, explosionRange, Vector3.up, 0f, enemyMask);
        foreach (RaycastHit hit in hits)
        {
            ZombieVitals enemy = hit.transform.GetComponent<ZombieVitals>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); 
            }
        }
        Instantiate(explosionPrefab,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }       
}
