using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkShop : MonoBehaviour
{
    public List<GameObject> AllPerks;
    public List<GameObject> ShowcasePerks;
    [SerializeField] private Material offMaterial;
    [SerializeField] private AudioSource purchaseSound;

    [Header("Types of Perks:")]
    [SerializeField] private PerkData IronSkin;
    [SerializeField] private PerkData SpeedJuice;
    [SerializeField] private PerkData MuscleJuice;

    private bool ironSkinBought = false;
    private bool speedJuiceBought = false;
    private bool muscleJuiceBought = false;


    public void BuyPerk(PerkData type)
    {
        if (type == IronSkin) // Increase Health
        {
            if (PlayerVitals.instance.money >= IronSkin.price && !ironSkinBought)
            {
                PlayerVitals.instance.money -= IronSkin.price;
                PlayerInventory.instance.ApplyPerk(0);
                ironSkinBought = true;
                AllPerks[0].GetComponent<Renderer>().material = offMaterial;
                ShowcasePerks[0].GetComponent<PurchasePerk>().PerkBought();
                purchaseSound.Play();
            }
        }
        if (type == SpeedJuice) // Increase Speed and Stamina
        {
            if (PlayerVitals.instance.money >= SpeedJuice.price && !speedJuiceBought)
            {
                PlayerVitals.instance.money -= SpeedJuice.price;
                PlayerInventory.instance.ApplyPerk(1);
                speedJuiceBought = true;
                AllPerks[1].GetComponent<Renderer>().material = offMaterial;
                ShowcasePerks[1].GetComponent<PurchasePerk>().PerkBought();
                purchaseSound.Play();
            }
        }
        if (type == MuscleJuice) // Increase Gun carry amount
        {
            if (PlayerVitals.instance.money >= MuscleJuice.price && !muscleJuiceBought)
            {
                PlayerVitals.instance.money -= MuscleJuice.price;
                PlayerInventory.instance.ApplyPerk(2);
                muscleJuiceBought = true;
                AllPerks[2].GetComponent<Renderer>().material = offMaterial;
                ShowcasePerks[2].GetComponent<PurchasePerk>().PerkBought();
                purchaseSound.Play();
            }
        }
    }
}
