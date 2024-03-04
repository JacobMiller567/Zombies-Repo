using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePoolManager : MonoBehaviour
{
    public static ZombiePoolManager Instance;
    [SerializeField] private GameObject[] zombiePrefabs; // Array of zombie prefabs
    [SerializeField] private int poolSizePerType = 50;

    private List<List<ZombieVitals>> zombiePools = new List<List<ZombieVitals>>(); // List of pools for each zombie type

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogWarning("Multiple ZombiePoolManager instances found. Only one will be used.");
            Destroy(gameObject);
            return;
        }

        InitializePools();
    }

    private void InitializePools()
    {
        // Initialize a pool for each zombie prefab
        foreach (GameObject prefab in zombiePrefabs)
        {
            List<ZombieVitals> pool = new List<ZombieVitals>();
            for (int i = 0; i < poolSizePerType; i++)
            {
                GameObject zombieObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                ZombieVitals zombie = zombieObject.GetComponent<ZombieVitals>();
                if (zombie != null)
                {
                    zombie.Deactivate();
                    pool.Add(zombie);
                }
                else
                {
                    Debug.LogWarning("One of the zombie prefabs is missing the ZombieVitals component.");
                    Destroy(zombieObject);
                }
            }
            zombiePools.Add(pool);
        }
    }

    public ZombieVitals GetZombie(int typeIndex)
    {
        if (typeIndex >= 0 && typeIndex < zombiePools.Count)
        {
            foreach (ZombieVitals zombie in zombiePools[typeIndex])
            {
                if (!zombie.IsActive())
                {
                    return zombie;
                }
            }
        }
        return null; // No inactive zombies available for the specified type
    }

    public void ReturnZombie(ZombieVitals zombie)
    {
        zombie.gameObject.SetActive(false); 
        zombie.ResetHealth(); 
    }


}