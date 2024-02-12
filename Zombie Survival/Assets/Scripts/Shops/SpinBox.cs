using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpinBox : MonoBehaviour
{
    [SerializeField] private GameObject costPopup;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private int price;

    [SerializeField] private Animator animator;
    private bool canBuy = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            costPopup.SetActive(true);
            costText.text = "Press E to spin Mystery Box [Cost: " + price.ToString()+"]";
            canBuy = true;
            StartCoroutine(CheckForPurchase());
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
                if (PlayerVitals.instance.money >= price)
                {
                    PlayerVitals.instance.money -= price;
                    MysteryBox.instance.StartSpin();
                    animator.SetTrigger("Purchased");
                    canBuy = false;
                }
            }
            yield return null;
        }
    }
}
