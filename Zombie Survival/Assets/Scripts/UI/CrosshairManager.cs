using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    public static CrosshairManager Instance;
    public GameObject[] crosshairTypes;
    [SerializeField] private RectTransform crosshairParent; // Reference to the parent RectTransform of the crosshairs

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ChangeCrosshair(string type)
    {
        foreach (GameObject crosshair in crosshairTypes)
        {
            crosshair.SetActive(false);
        }
        switch (type)
        {
            case "Pistol":
            case "HeavyPistol":
            case "Revolver":
                crosshairTypes[0].SetActive(true);
                break;
            case "MP7":
            case "MAC11":
                crosshairTypes[1].SetActive(true);
                break;
            case "DoubleBarrel":
            case "PumpAction":
                crosshairTypes[3].SetActive(true);
                break;
            case "AK47":
            case "AR15":
            case "FAMAS":
                crosshairTypes[2].SetActive(true);
                break;
            case "Ballista":
                crosshairTypes[4].SetActive(true);
                break;
            default:
                break;
        }

        /*
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
        */

    }

/*
    private void SetCrosshairPositions(Vector3 gunPosition)
    {
        for (int i = 0; i < crosshairTypes.Length; i++)
        {
            crosshairTypes[i].transform.position = gunPosition + crosshairOffsets[i];
        }
    }
*/

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
