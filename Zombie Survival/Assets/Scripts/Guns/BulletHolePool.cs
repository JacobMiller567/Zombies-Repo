using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolePool : MonoBehaviour
{
    public static BulletHolePool Instance;

    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private int poolSize = 20;
    private List<GameObject> bulletHolePool = new List<GameObject>();

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
        foreach(GameObject bulletHole in bulletHolePool)
        {
            if (!bulletHole.activeSelf)
            {
                return bulletHole;
            }
        }

        // Expand pool
        GameObject newBulletHole = Instantiate(bulletHolePrefab, Vector3.zero, Quaternion.identity);
        newBulletHole.SetActive(false);
        bulletHolePool.Add(newBulletHole);
        return newBulletHole;
    }

    public void ReturnBulletHole(GameObject bulletHole)
    {
        bulletHole.SetActive(false);
    }



}
