using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShop : MonoBehaviour
{ 
    public List<GameObject> AllGuns;
    public List<GameObject> ShowcaseGuns;
    public List<GameObject> ShowcaseAmmo;
    public List<GameObject> ShowcaseOwnedGuns;

    [Header("Types of Guns:")]
    [SerializeField] private GunData HeavyPistol;
    [SerializeField] private GunData AK47;
    [SerializeField] private GunData DoubleBarrel;

   // [SerializeField] private GunData DoubleBarrelShotgun;
   // [SerializeField] private GunData HeavyPistol;
   // [SerializeField] private GunData HeavyPistol;


    public void BuyGun(GunData type)
    {
        if (type == HeavyPistol) // Gun 0
        {
            if (PlayerVitals.instance.money >= HeavyPistol.price)
            {
                PlayerVitals.instance.money -= HeavyPistol.price;
                PlayerInventory.instance.UnlockGun(AllGuns[0]);
                ShowcaseAmmo[0].SetActive(true);
                //ShowcaseOwnedGuns[0].SetActive(true);
                ShowcaseGuns[0].SetActive(false);
                Debug.Log("Heavy Pistol Bought!");
            }
        }
        if (type == AK47) // Gun 1
        {
            if (PlayerVitals.instance.money >= AK47.price)
            {
                PlayerVitals.instance.money -= AK47.price;
                PlayerInventory.instance.UnlockGun(AllGuns[1]);
                ShowcaseAmmo[1].SetActive(true);
               // ShowcaseOwnedGuns[1].SetActive(true);
                ShowcaseGuns[1].SetActive(false);
                Debug.Log("AK47 Bought!");
            }
        }
        if (type == DoubleBarrel) // Gun 2
        {
            if (PlayerVitals.instance.money >= DoubleBarrel.price)
            {
                PlayerVitals.instance.money -= DoubleBarrel.price;
                PlayerInventory.instance.UnlockGun(AllGuns[2]);
                ShowcaseAmmo[2].SetActive(true);
               // ShowcaseOwnedGuns[2].SetActive(true);
                ShowcaseGuns[2].SetActive(false);
                Debug.Log("DoubleBarrel Bought!");
            }
        }
    }

    public void BuyAmmo(GunData type)
    {
        if (type == HeavyPistol && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "Heavy_Pistol") 
        {
            int amount = Mathf.RoundToInt(HeavyPistol.price / 2);
            if (PlayerVitals.instance.money >= amount)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.AddAmmo();
            }
        }
        if (type == AK47 && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "AK47_AR") 
        {
            int amount = Mathf.RoundToInt(AK47.price / 2);
            if (PlayerVitals.instance.money >= amount)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.AddAmmo();
            }
        }
        if (type == DoubleBarrel && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "DoubleBarrel_SG") 
        {
            int amount = Mathf.RoundToInt(DoubleBarrel.price / 2);
            if (PlayerVitals.instance.money >= amount)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.AddAmmo();
            }
        }
    }

    public void AddGun(GunData type)
    {
        if (type == HeavyPistol) // Gun 0
        {
            PlayerInventory.instance.EquipGun(AllGuns[0]);
            ShowcaseAmmo[0].SetActive(true);
            ShowcaseOwnedGuns[0].SetActive(false);
        }
        if (type == AK47) // Gun 1
        {
            PlayerInventory.instance.EquipGun(AllGuns[1]);
            ShowcaseAmmo[1].SetActive(true);
            ShowcaseOwnedGuns[1].SetActive(false);
        }
        if (type == DoubleBarrel) // Gun 2
        {
            PlayerInventory.instance.EquipGun(AllGuns[2]);
            ShowcaseAmmo[2].SetActive(true);
            ShowcaseOwnedGuns[2].SetActive(false);
        }
    }

    public void OnGunsChanged()
    {
        if (PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "Heavy_Pistol")
        {
            ShowcaseOwnedGuns[0].SetActive(true);
            ShowcaseAmmo[0].SetActive(false);
        }
        if (PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "AK47_AR")
        {
            ShowcaseOwnedGuns[1].SetActive(true);
            ShowcaseAmmo[1].SetActive(false);           
        }
        if (PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "DoubleBarrel_SG")
        {
            ShowcaseOwnedGuns[2].SetActive(true);
            ShowcaseAmmo[2].SetActive(false);     
        }
    }

}
