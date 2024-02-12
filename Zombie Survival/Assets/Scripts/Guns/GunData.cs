using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject, ISerializationCallbackReceiver
{
    public new string name;
    public int price;
    public float damage;
    public float maxRange;
    public int ammo;
    public int magazineSize;
    public float fireRate;
    public float reloadSpeed;
    public bool reloading;

    // These values get changed throughout the game but won't save
    [NonSerialized]
    public int RuntimeAmmo;
    [NonSerialized]
    public int RuntimeMagazine;
    [NonSerialized]
    public float RuntimeDamage;


    public void OnAfterDeserialize()
    {
        RuntimeAmmo = ammo;
        RuntimeMagazine = magazineSize;
        RuntimeDamage = damage;
    }

    public void OnBeforeSerialize() {}
}
