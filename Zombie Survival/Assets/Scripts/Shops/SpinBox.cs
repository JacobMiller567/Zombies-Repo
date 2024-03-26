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
    private bool spawnCoolDown = true;


    private void OnEnable() 
    {
        //StartCoroutine(BoxSpawned()); // TEST
        costPopup.SetActive(false);
        MysteryBox.instance.StopCoroutine(MysteryBox.instance.CloseBox()); // Reset Mystery Box coroutine
    }
    private void OnDisable() 
    {
        ResetDisplay(); // TEST
        animator.Rebind(); // TEST!
        //Debug.Log("Changed");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !MysteryBox.instance.isSpinning )//&& !spawnCoolDown)
        {   
            costPopup.SetActive(true);
            costText.text = "Press E to spin Mystery Box [Cost: " + price.ToString()+"]";
            canBuy = true;
            StartCoroutine(CheckForPurchase());
        }
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

    IEnumerator CheckForPurchase() // When MysterBox is pruchased
    {
        while (canBuy && !MysteryBox.instance.isSpinning)//&& MysteryBox.instance.currentSpins <= MysteryBox.instance.maxSpins)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (PlayerVitals.instance.money >= price)
                {
                    PlayerVitals.instance.money -= price;
                    MysteryBox.instance.StartSpin();
                    //animator.SetTrigger("Purchased");
                    animator.SetBool("isPurchased", true);
                    costPopup.SetActive(false);
                    canBuy = false;
                }
            }
            yield return null;
        }
    }

    IEnumerator CheckForClaim() // When gun is collected
    {
        while (canBuy && MysteryBox.instance.canCollect)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MysteryBox.instance.isCollected = true;
                canBuy = false;
                //animator.SetTrigger("Complete"); // TEST
                //animator.ResetTrigger("Purchased"); // TEST
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
            animator.SetBool("isPurchased", false);
          //  animator.SetTrigger("Complete");
           // animator.ResetTrigger("Complete"); // TEST
            //animator.SetTrigger("Reset");
            //StartCoroutine(BoxSpawned()); // TEST
        }
    }

    private IEnumerator BoxSpawned()
    {
        costPopup.SetActive(false);
        spawnCoolDown = true;
        yield return new WaitForSeconds(.5f);
        spawnCoolDown = false;

    }
}
