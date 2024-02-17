using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipBoughtGuns : MonoBehaviour
{
    [SerializeField] private GameObject gunPopup;
    [SerializeField] private TextMeshProUGUI gunText;
    [SerializeField] private GunShop shop;
    [SerializeField] private GunData gunType;
    private bool inputReceived = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            gunPopup.SetActive(true);
            gunText.text = "Press E to equip "+ gunType.name;
            inputReceived = true;
            StartCoroutine(CheckForInput());  // Check first if player has money? // CHECK: See if this saves some fps
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gunPopup.SetActive(false);
            inputReceived = false;
        }
    }

    IEnumerator CheckForInput()
    {
        while (inputReceived)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                shop.AddGun(gunType);
            }
            yield return null;
        }

    }
}
