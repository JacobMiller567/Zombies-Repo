using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    public static CrosshairManager Instance;
    public GameObject[] crosshairTypes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ChangeCrosshair(string type)
    {
        if (type == "Pistol" || type == "HeavyPistol" || type == "Revolver")
        {
            UsingPistol();
        }
        if (type == "MP7" || type == "MAC11")
        {
            UsingSMG();
        }
        if (type == "DoubleBarrel" || type == "PumpAction" )
        {
            UsingShotgun();
        }
        if (type == "AK47" || type == "AR15" || type == "FAMAS")
        {
            UsingAssaultRifle();
        }
        if (type == "Ballista")
        {
            UsingSniper();
        }

    }

    public void UsingSniper()
    {
        foreach (GameObject type in crosshairTypes)
        {
            type.SetActive(false);
        }
        crosshairTypes[4].SetActive(true);
    }
    public void UsingShotgun()
    {
        foreach (GameObject type in crosshairTypes)
        {
            type.SetActive(false);
        }
        crosshairTypes[3].SetActive(true);
    }
    public void UsingAssaultRifle()
    {
        foreach (GameObject type in crosshairTypes)
        {
            type.SetActive(false);
        }
        crosshairTypes[2].SetActive(true);
    }
    public void UsingSMG()
    {
        foreach (GameObject type in crosshairTypes)
        {
            type.SetActive(false);
        }
        crosshairTypes[1].SetActive(true);
    }
    public void UsingPistol()
    {
        foreach (GameObject type in crosshairTypes)
        {
            type.SetActive(false);
        }
        crosshairTypes[0].SetActive(true);
    }
}
