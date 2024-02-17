using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    public static UpgradeShop Instance;
    public List<GameObject> AllGuns;
    public List<GameObject> ShowcaseGuns;

    [Header("Shop Info:")]
    [SerializeField] private float upgradeTime = 180f;
    public int upgradeCost = 500;

    [Header("Types of Guns:")]
    [SerializeField] private GunData Pistol;
    [SerializeField] private GunData HeavyPistol;
    [SerializeField] private GunData AK47;
    [SerializeField] private GunData DoubleBarrel;

    public bool allowUpgrade = true;
    public bool upgradeComplete = false;
    public bool isCollected = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void BuyUpgrade(GunData data)
    {
        if (data == Pistol && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "Starter_Pistol" && allowUpgrade)
        {
            int amount = Mathf.RoundToInt(Pistol.price + upgradeCost);
            if (PlayerVitals.instance.money >= amount && !data.RuntimeUpgraded)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.RemoveGun(AllGuns[0]);
                ShowUpgrading(0);
                StartCoroutine(StartUpgrading(AllGuns[0], data));
            }
        }
        if (data == HeavyPistol && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "Heavy_Pistol" && allowUpgrade)
        {
            int amount = Mathf.RoundToInt(HeavyPistol.price + upgradeCost);
            if (PlayerVitals.instance.money >= amount && !data.RuntimeUpgraded)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.RemoveGun(AllGuns[1]);
                ShowUpgrading(1);
                StartCoroutine(StartUpgrading(AllGuns[1], data));
            }
        }
        if (data == AK47 && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "AK47_AR" && allowUpgrade) 
        {
            int amount = Mathf.RoundToInt(AK47.price + upgradeCost);
            if (PlayerVitals.instance.money >= amount && !data.RuntimeUpgraded)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.RemoveGun(AllGuns[2]);
                ShowUpgrading(2);
                StartCoroutine(StartUpgrading(AllGuns[2], data));
            }
        }
        if (data == DoubleBarrel && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "DoubleBarrel_SG" && allowUpgrade) 
        {
            int amount = Mathf.RoundToInt(DoubleBarrel.price + upgradeCost);
            if (PlayerVitals.instance.money >= amount && !data.RuntimeUpgraded)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.RemoveGun(AllGuns[3]);
                ShowUpgrading(3);
                StartCoroutine(StartUpgrading(AllGuns[3], data));
            }
        }
    }

    private IEnumerator StartUpgrading(GameObject type, GunData data)
    {
        allowUpgrade = false;
        yield return new WaitForSeconds(upgradeTime);
        // Add finish sounds!
        upgradeComplete = true;
        yield return new WaitUntil(() => isCollected); // Wait till player claims gun
        AddUpgradedWeapon(type, data);
    }
    public void ShowUpgrading(int index)
    {
        ShowcaseGuns[index].SetActive(true);
    }

    public void AddUpgradedWeapon(GameObject type, GunData data)
    {
        data.RuntimeUpgraded = true;
        data.RuntimeDamage = data.upgradedDamage;
        data.RuntimeAmmo = data.upgradedAmmo;
        data.RuntimeMagazine = data.upgradedMagazineSize;
        data.RuntimeFireRate = data.upgradedFireRate;
        data.RuntimeMaxRange = data.upgradedMaxRange;
        data.RuntimeReloadSpeed = data.upgradedReloadSpeed;
        PlayerInventory.instance.EquipGun(type);

        foreach(GameObject gun in ShowcaseGuns)
        {
            gun.SetActive(false);
        }

        upgradeComplete = false;
        isCollected = false;
        allowUpgrade = true;
    }

    public void RemoveUpgrade(GunData data)
    {
        data.RuntimeUpgraded = false;
        data.RuntimeDamage = data.damage;
        data.RuntimeAmmo = data.ammo;
        data.RuntimeMagazine = data.magazineSize;
        data.RuntimeFireRate = data.fireRate;
        data.RuntimeMaxRange = data.maxRange;
        data.RuntimeReloadSpeed = data.reloadSpeed;
    }
}
