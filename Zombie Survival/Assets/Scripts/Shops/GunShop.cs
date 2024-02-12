using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShop : MonoBehaviour
{
    
    public List<GameObject> AllGuns;
    public List<GameObject> ShowcaseGuns;
    public List<GameObject> ShowcaseAmmo;

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
               // PlayerInventory.instance.ChangeWeapon(1); // TEST
                ShowcaseAmmo[0].SetActive(true);
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
                ShowcaseGuns[2].SetActive(false);
                Debug.Log("DoubleBarrel Bought!");
            }
        }
    }

    public void BuyAmmo(GunData type)
    {
        if (type == HeavyPistol) 
        {
            Mathf.RoundToInt(PlayerVitals.instance.money -= HeavyPistol.price / 2);
            PlayerInventory.instance.AddAmmo();//(1); // Passing 1 since heavy pistol is the second gun in the list
        }
        if (type == AK47) 
        {
            Mathf.RoundToInt(PlayerVitals.instance.money -= HeavyPistol.price / 2);
            PlayerInventory.instance.AddAmmo();//(2);
        }
        if (type == DoubleBarrel) 
        {
            Mathf.RoundToInt(PlayerVitals.instance.money -= DoubleBarrel.price / 2);
            PlayerInventory.instance.AddAmmo();//(3);
        }
    }

}
