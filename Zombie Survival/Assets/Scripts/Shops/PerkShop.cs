using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkShop : MonoBehaviour
{
    public List<GameObject> AllPerks;
    public List<GameObject> ShowcasePerks;
    [SerializeField] private Material offMaterial;

    [Header("Types of Perks:")]
    [SerializeField] private PerkData MuscleJuice;
    [SerializeField] private PerkData SpeedJuice;

    private bool muscleJuiceBought = false;
    private bool speedJuiceBought = false;


    public void BuyPerk(PerkData type)
    {
        if (type == MuscleJuice) 
        {
            if (PlayerVitals.instance.money >= MuscleJuice.price && !muscleJuiceBought)
            {
                PlayerVitals.instance.money -= MuscleJuice.price;
                PlayerInventory.instance.ApplyPerk(0);
                muscleJuiceBought = true;
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
    }
}
