using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public static MysteryBox instance;
    
    [SerializeField] private GameObject[] boxes;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        boxes[Random.Range(0, boxes.Length)].SetActive(true); // Spawn random mystery box
    }

    public void StartSpin()
    {
        
    }
}
