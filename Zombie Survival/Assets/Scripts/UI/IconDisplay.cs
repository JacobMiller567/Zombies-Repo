using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconDisplay : MonoBehaviour
{
    public static IconDisplay Instance;
    public List<GameObject> fullGrenades;
    public GameObject[] emptyGrenades;
    private int index = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void RemoveGrenade()
    {
        fullGrenades[index].SetActive(false);
        if (index >= 0 && index < emptyGrenades.Length) // Make sure index is in range!
        {
            emptyGrenades[index].SetActive(true);
            fullGrenades[index].SetActive(false);
            index++;
        }
        else
        {
            Debug.LogWarning("Index out of range in RemoveGrenade()");
        }
    }

    public void AddedGrenades()
    {
        foreach (GameObject grenade in fullGrenades)
        {
            grenade.SetActive(true);
        }
        foreach (GameObject grenade in emptyGrenades)
        {
            grenade.SetActive(false);
        }
        index = 0;
    }
}
