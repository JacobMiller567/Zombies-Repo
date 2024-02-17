using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkShop : MonoBehaviour
{
    public List<GameObject> AllPerks;
    public List<GameObject> ShowcasePerks;
    [SerializeField] private Material offMaterial;

    [Header("Types of Perks:")]
    [SerializeField] private PerkData IronSkin;
    [SerializeField] private PerkData SpeedJuice;
    [SerializeField] private PerkData MuscleJuice;


    private bool ironSkinBought = false;
    private bool speedJuiceBought = false;
    private bool muscleJuiceBought = false;


    public void BuyPerk(PerkData type)
    {
        if (type == MuscleJuice) 
        {
            if (PlayerVitals.instance.money >= IronSkin.price && !ironSkinBought)
            {
                PlayerVitals.instance.money -= IronSkin.price;
                PlayerInventory.instance.ApplyPerk(0);
                ironSkinBought = true;
                // Make screen turn black after purchase
                AllPerks[0].GetComponent<Renderer>().material = offMaterial;
                Debug.Log("Muscle Juice Bought!");
            }
        }
        if (type == SpeedJuice) 
        {
            if (PlayerVitals.instance.money >= SpeedJuice.price && !speedJuiceBought)
            {
                PlayerVitals.instance.money -= SpeedJuice.price;
                PlayerInventory.instance.ApplyPerk(1);
                speedJuiceBought = true;
                // Make screen turn black after purchase
                AllPerks[1].GetComponent<Renderer>().material = offMaterial;
                Debug.Log("Speed Juice Bought!");
            }
        }
        if (type == MuscleJuice) 
        {
            if (PlayerVitals.instance.money >= MuscleJuice.price && !muscleJuiceBought)
            {
                PlayerVitals.instance.money -= MuscleJuice.price;
                PlayerInventory.instance.ApplyPerk(2);
                muscleJuiceBought = true;
                // Make screen turn black after purchase
                AllPerks[2].GetComponent<Renderer>().material = offMaterial;
                Debug.Log("Speed Juice Bought!");
            }
        }
    }
}
