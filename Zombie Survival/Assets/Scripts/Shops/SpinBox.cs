using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpinBox : MonoBehaviour
{
    [SerializeField] private GameObject costPopup;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private int price;
    public List<GameObject> displayGuns;
    [SerializeField] private Animator animator;

    private bool canBuy = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !MysteryBox.instance.isSpinning)
        {   
            costPopup.SetActive(true);
            costText.text = "Press E to spin Mystery Box [Cost: " + price.ToString()+"]";
            canBuy = true;
            StartCoroutine(CheckForPurchase());
        }
        /*
        if (other.gameObject.CompareTag("Player") && MysteryBox.instance.isSpinning && !MysteryBox.instance.isCollected)
        {
            costPopup.SetActive(true);
            costText.text = "Press E to Claim: ";
            canBuy = true;
            StartCoroutine(CheckForClaim());
        }
        */
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && MysteryBox.instance.isSpinning && !MysteryBox.instance.isCollected)
        {
            costPopup.SetActive(true);
            costText.text = "Press E to Claim: ";
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
        while (canBuy && !MysteryBox.instance.isSpinning)//&& MysteryBox.instance.currentSpins <= MysteryBox.instance.maxSpins)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (PlayerVitals.instance.money >= price)
                {
                    PlayerVitals.instance.money -= price;
                    MysteryBox.instance.StartSpin();
                    animator.SetTrigger("Purchased");
                    costPopup.SetActive(false);
                    canBuy = false;
                }
            }
            yield return null;
        }
    }

    IEnumerator CheckForClaim()
    {
        while (canBuy && MysteryBox.instance.canCollect)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MysteryBox.instance.isCollected = true;
                canBuy = false;
                // animator.SetTrigger("Complete");
                costPopup.SetActive(false);
            }
            yield return null;          
        }
    }

    public void ResetDisplay()
    {
        foreach(GameObject gun in displayGuns)
        {
            gun.SetActive(false);
            animator.SetTrigger("Complete");
        }
    }
}
