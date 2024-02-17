using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Gun Stats")]
    public new string name;
    public int price;
    public float damage;
    public float maxRange;
    public int ammo;
    public int magazineSize;
    public float fireRate;
    public float reloadSpeed;
    public bool reloading;

    [Header("Upgraded Stats")]
    public bool upgraded;
    public float upgradedDamage;
    public float upgradedMaxRange;
    public int upgradedAmmo;
    public int upgradedMagazineSize;
    public float upgradedFireRate;
    public float upgradedReloadSpeed;


    // These values get changed throughout the game but won't save
    [NonSerialized] public int RuntimeAmmo;
    [NonSerialized] public int RuntimeMagazine;
    [NonSerialized] public float RuntimeDamage;
    [NonSerialized] public float RuntimeFireRate;
    [NonSerialized] public float RuntimeMaxRange;
    [NonSerialized] public float RuntimeReloadSpeed;
    [NonSerialized] public bool RuntimeUpgraded;

    public void OnAfterDeserialize()
    {
        RuntimeAmmo = ammo;
        RuntimeMagazine = magazineSize;
        RuntimeDamage = damage;
        RuntimeFireRate = fireRate;
        RuntimeMaxRange = maxRange;
        RuntimeReloadSpeed = reloadSpeed;
        RuntimeUpgraded = upgraded;
    }

    public void OnBeforeSerialize() {}
}
