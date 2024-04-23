using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    public static CrosshairManager Instance;
    public GameObject[] crosshairTypes;
    [SerializeField] private RectTransform crosshairParent; // Reference to the parent RectTransform of the crosshairs
    private int crosshairIndex;

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
                crosshairIndex = 0;
                break;
            case "MP7":
            case "MAC11":
                crosshairTypes[1].SetActive(true);
                crosshairIndex = 1;
                break;
            case "DoubleBarrel":
            case "PumpAction":
                crosshairTypes[3].SetActive(true);
                crosshairIndex = 3;
                break;
            case "AK47":
            case "M16":
            case "FAMAS":
            case "AUG":
                crosshairTypes[2].SetActive(true);
                crosshairIndex = 2;
                break;
            case "Ballista":
                crosshairTypes[4].SetActive(true);
                crosshairIndex = 4;
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
        if (type == "AK47" || type == "AR15" || type == "FAMAS" || type = "AUG")
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


/*
    public void SetCrosshairScale(float scaleFactor) // TEST
    {
        crosshairParent.localScale = Vector3.one * scaleFactor;
       // crosshairTypes[crosshairIndex].transform.localScale = Vector3.one * scaleFactor;

    }
*/
    public void SetCrosshairScale(bool isZoomedIn)
    {
       // normalCrosshairImage.SetActive(!isZoomedIn);
       // zoomedInCrosshairImage.SetActive(isZoomedIn);
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
