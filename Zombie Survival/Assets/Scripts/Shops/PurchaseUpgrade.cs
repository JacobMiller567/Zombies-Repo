using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchaseUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject costPopup;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private UpgradeShop shop;
    private bool canBuy = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && shop.allowUpgrade)
        {   
            costPopup.SetActive(true);
            costText.text = "Press E to Upgrade: " + PlayerInventory.instance.activeGuns[PlayerInventory.instance.GetIndex()].GetComponentInChildren<Gun>().GetGunData().name + " [Cost: " + (PlayerInventory.instance.activeGuns[PlayerInventory.instance.GetIndex()].GetComponentInChildren<Gun>().GetGunData().price + shop.upgradeCost).ToString()+"]";
            canBuy = true;
            StartCoroutine(CheckForPurchase());  // Check first if player has money? // CHECK: See if this saves some fps
        }
        if (other.gameObject.CompareTag("Player") && !shop.allowUpgrade && shop.upgradeComplete)
        {
            costPopup.SetActive(true);
            costText.text = "Press E to Claim: " + shop.AllGuns[shop.currentIndex].GetComponentInChildren<Gun>().GetGunData().name;
            canBuy = true;
            StartCoroutine(CheckForClaim());
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
                shop.BuyUpgrade(PlayerInventory.instance.activeGuns[PlayerInventory.instance.GetIndex()].GetComponentInChildren<Gun>().GetGunData());
            }
            yield return null;
        }
    }
    IEnumerator CheckForClaim()
    {
        while (canBuy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                shop.isCollected = true;
                canBuy = false;
            }
            yield return null;          
        }
    }
}
