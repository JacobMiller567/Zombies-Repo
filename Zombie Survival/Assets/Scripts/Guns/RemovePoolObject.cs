using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePoolObject : MonoBehaviour
{
    public float lifespan = .5f;
    public bool isBullet;

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(lifespan);

        if (isBullet && BulletHolePool.Instance != null)
        {
            BulletHolePool.Instance.ReturnBulletHole(gameObject);
        }
        else if (!isBullet && BloodSplatterPool.Instance != null)
        {
            BloodSplatterPool.Instance.ReturnBloodSplatter(gameObject);
        }
        else
        {
            Debug.LogWarning("Instance is Null");
        }
    }
}
