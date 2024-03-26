using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterPool : MonoBehaviour
{
    public static BloodSplatterPool Instance;

    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private int poolSize = 40;
    private List<GameObject> bloodSplatterPool = new List<GameObject>();

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
            GameObject bloodSplatter = Instantiate(bloodPrefab, Vector3.zero, Quaternion.identity);
            bloodSplatter.SetActive(false);
            bloodSplatterPool.Add(bloodSplatter);
        }
    }

    public GameObject GetBloodSplatter()
    {
        if (bloodSplatterPool.Count > 0)
        {
            GameObject bloodSplatter = bloodSplatterPool[0];
            bloodSplatterPool.RemoveAt(0);
            return bloodSplatter;
        }
        Debug.Log("ERROR: Out of blood splatters");
        return null;
    }

    public void ReturnBloodSplatter(GameObject bloodSplatter)
    {
        bloodSplatter.SetActive(false);
        bloodSplatterPool.Add(bloodSplatter);
    }
}