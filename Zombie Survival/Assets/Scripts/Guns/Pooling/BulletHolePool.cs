using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolePool : MonoBehaviour
{
    public static BulletHolePool Instance;
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private int poolSize = 50;
    private List<GameObject> bulletHolePool = new List<GameObject>(); // REMOVE SerializeField
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        InitializePool();
    }

    public void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bulletHole = Instantiate(bulletHolePrefab, Vector3.zero, Quaternion.identity);
            bulletHole.SetActive(false);
            bulletHolePool.Add(bulletHole);
        }
    }
    public GameObject GetBulletHole()
    {
       if (bulletHolePool.Count > 0)
        {
            GameObject bulletHole = bulletHolePool[0];
            bulletHolePool.RemoveAt(0);
            bulletHole.SetActive(true);  // Activate the bullet hole before returning
            return bulletHole;
        }

        // If there are no inactive bullet holes available, reuse existing ones if possible
        GameObject reusedBulletHole = ReuseBulletHole();
        if (reusedBulletHole != null)
        {
            Debug.Log("Reusing bullet holes");
            reusedBulletHole.SetActive(true);  // Activate the reused bullet hole
            return reusedBulletHole;
        }

        Debug.Log("Pool is too large");
        return null;
    }

    private GameObject ReuseBulletHole()
    {
        foreach (GameObject bulletHole in bulletHolePool)
        {
            if (!bulletHole.activeSelf)
            {
                bulletHole.SetActive(true);  // Activate the bullet hole
                return bulletHole;
            }
        }
        Debug.LogWarning("ERROR: All bullet holes are currently active.");
        return null;
    }

    public void ReturnBulletHole(GameObject bulletHole)
    {
        bulletHole.SetActive(false);
        bulletHolePool.Add(bulletHole);
    }
}