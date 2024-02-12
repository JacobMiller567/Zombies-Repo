using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchaseWeapon : MonoBehaviour
{
    [SerializeField] private GameObject costPopup;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private GunShop shop;
    [SerializeField] private GunData gunType;
    private bool canBuy = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            costPopup.SetActive(true);
            costText.text = "Press E to buy " + gunType.name + " [Cost: " + gunType.price.ToString()+"]";
            canBuy = true;
            StartCoroutine(CheckForPurchase());  // Check first if player has money? // CHECK: See if this saves some fps

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            costPopup.SetActive(false);
            canBuy = false;
        }
    }

    IEnumerator CheckForPurchase()
    {
        while (canBuy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                shop.BuyGun(gunType);
            }
            yield return null;
        }

    }


}
