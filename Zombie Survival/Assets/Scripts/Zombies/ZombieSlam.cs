using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSlam : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator animator;
    [SerializeField] private float coolDown;
    [SerializeField] private float splashDamage;
    [SerializeField] private float splashRange;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject explosionPrefab;
    private bool canSlam = true;
    void Start()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canSlam)
        {
            animator.SetTrigger("PowerSlam");
            canSlam = false;
            //SplashArea();
            StartCoroutine(CoolDown());
        }
    }

    private void SplashArea()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, splashRange, Vector3.up, 0f);//, playerMask);
        int numHits = 0;

        if (animator.GetBool("isDead") == true)
        {
            splashDamage = 0f;
        }
        foreach (RaycastHit hit in hits)
        {
            Damage player = hit.transform.GetComponent<Damage>();
            player?.TakeDamage(splashDamage);
            numHits++;
            //numHits++;
            //if (numHits > 0)
            //{
            //    break;
           // }
        }
        Debug.Log("Hits: " + numHits);
        GameObject areaOfEffect = Instantiate(explosionPrefab,transform.position, Quaternion.identity);
        Destroy(areaOfEffect, 1.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, splashRange);
    } 

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        canSlam = true;
    }
}
