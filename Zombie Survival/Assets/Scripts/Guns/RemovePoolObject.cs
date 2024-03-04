using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePoolObject : MonoBehaviour
{
    public float lifespan = 1f; 

    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(lifespan);
        BulletHolePool.Instance.ReturnBulletHole(gameObject);
    }
}
