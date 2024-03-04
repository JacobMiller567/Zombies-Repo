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

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start() 
    {
        maxAmmo = weapon.RuntimeMagazine;
        getAmmo = weapon.RuntimeAmmo;
        ammoText.text = getAmmo + "/" + maxAmmo;
    }

    public void WeaponChanged(GunData newWeapon) 
    {
        weapon = newWeapon;
        UpdateAmmo();
    }

    public void UpdateAmmo()
    {
        maxAmmo = weapon.RuntimeMagazine;
        getAmmo = weapon.RuntimeAmmo;
        ammoText.text = getAmmo + "/" + maxAmmo;
    }
}
