using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LockedDoors : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private GameObject costPopup;
    [SerializeField] private TextMeshProUGUI costText;
    private bool canBuy = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            costPopup.SetActive(true);
            costText.text = "Press E to unlock area "  + "[Cost: " + price+"]";
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
                if (PlayerVitals.instance?.money >= price)
                {
                    PlayerVitals.instance.money -= price;
                    costPopup.SetActive(false);
                    Destroy(gameObject);
                }
            }
            yield return null;
        }

    }


    
}
