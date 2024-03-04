using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float destroyTime = 1.5f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
