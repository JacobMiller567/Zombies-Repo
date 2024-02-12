using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    public static AmmoDisplay instance;
    public GunData weapon;
    public GunData[] weaponData;
    public TextMeshProUGUI ammoText;
    public int maxAmmo;
    public int getAmmo;
    private int index;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start() // ADDED
    {
        maxAmmo = weapon.RuntimeMagazine;
        getAmmo = weapon.RuntimeAmmo;
        ammoText.text = getAmmo + "/" + maxAmmo;
    }

    void Update()
    {
        //DisplayAmmo();
    }

    public void WeaponChanged(GunData newWeapon) 
    {
        weapon = newWeapon;
        Debug.Log(weapon.name);
        UpdateAmmo();
    }
    
    /*
    public void DisplayAmmo()
    {
        //maxAmmo = weapon.magazineSize;
        maxAmmo = weapon.RuntimeMagazine;
        getAmmo = weapon.RuntimeAmmo;
        ammoText.text = getAmmo + "/" + maxAmmo;
    }
    */

    public void UpdateAmmo() // ADDED
    {
        maxAmmo = weapon.RuntimeMagazine;
        getAmmo = weapon.RuntimeAmmo;
        ammoText.text = getAmmo + "/" + maxAmmo;
    }
    
}
