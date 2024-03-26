using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    public static UpgradeShop Instance;
    public List<GameObject> AllGuns;
    public List<GameObject> ShowcaseGuns;

    [SerializeField] private Material decal;

    [Header("Shop Info:")]
    [SerializeField] private float upgradeTime = 180f;
    public int upgradeCost = 500;
    public bool changeToNextGun = false;

    [Header("Types of Guns:")]
    [SerializeField] private GunData Pistol;
    [SerializeField] private GunData HeavyPistol;
    [SerializeField] private GunData AK47;
    [SerializeField] private GunData DoubleBarrel;
    [SerializeField] private GunData PumpAction;
    [SerializeField] private GunData MP7;
    [SerializeField] private GunData MAC11;
    [SerializeField] private GunData Ballista;
    [SerializeField] private GunData AUG;
    [SerializeField] private GunData AR15;
    [SerializeField] private GunData FAMAS;
    [SerializeField] private GunData Revolver;

    public bool allowUpgrade = true;
    public bool upgradeComplete = false;
    public bool isCollected = false;
    public int currentIndex = 0;

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

        if (data == PumpAction && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "PumpAction_SG" && allowUpgrade) 
        {
            int amount = Mathf.RoundToInt(PumpAction.price + upgradeCost);
            if (PlayerVitals.instance.money >= amount && !data.RuntimeUpgraded)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.RemoveGun(AllGuns[4]);
                ShowUpgrading(4);
                StartCoroutine(StartUpgrading(AllGuns[4], data));
            }
        }
        if (data == MP7 && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "MP7_SMG" && allowUpgrade) 
        {
            int amount = Mathf.RoundToInt(MP7.price + upgradeCost);
            if (PlayerVitals.instance.money >= amount && !data.RuntimeUpgraded)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.RemoveGun(AllGuns[5]);
                ShowUpgrading(5);
                StartCoroutine(StartUpgrading(AllGuns[5], data));
            }
        }
        if (data == MAC11 && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "MAC11_SMG" && allowUpgrade) 
        {
            int amount = Mathf.RoundToInt(MAC11.price + upgradeCost);
            if (PlayerVitals.instance.money >= amount && !data.RuntimeUpgraded)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.RemoveGun(AllGuns[6]);
                ShowUpgrading(6);
                StartCoroutine(StartUpgrading(AllGuns[6], data));
            }
        }
        if (data == Ballista && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "Ballista_Sniper" && allowUpgrade) 
        {
            int amount = Mathf.RoundToInt(Ballista.price + upgradeCost);
            if (PlayerVitals.instance.money >= amount && !data.RuntimeUpgraded)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.RemoveGun(AllGuns[7]);
                ShowUpgrading(7);
                StartCoroutine(StartUpgrading(AllGuns[7], data));
            }
        }
        if (data == AUG && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "AUG_AR" && allowUpgrade) 
        {
            int amount = Mathf.RoundToInt(AUG.price + upgradeCost);
            if (PlayerVitals.instance.money >= amount && !data.RuntimeUpgraded)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.RemoveGun(AllGuns[8]);
                ShowUpgrading(8);
                StartCoroutine(StartUpgrading(AllGuns[8], data));
            }
        }
        if (data == AR15 && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "AR15_AR" && allowUpgrade) 
        {
            int amount = Mathf.RoundToInt(AR15.price + upgradeCost);
            if (PlayerVitals.instance.money >= amount && !data.RuntimeUpgraded)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.RemoveGun(AllGuns[9]);
                ShowUpgrading(9);
                StartCoroutine(StartUpgrading(AllGuns[9], data));
            }
        }
        if (data == FAMAS && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "FAMAS_AR" && allowUpgrade) 
        {
            int amount = Mathf.RoundToInt(FAMAS.price + upgradeCost);
            if (PlayerVitals.instance.money >= amount && !data.RuntimeUpgraded)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.RemoveGun(AllGuns[10]);
                ShowUpgrading(10);
                StartCoroutine(StartUpgrading(AllGuns[10], data));
            }
        }
        if (data == Revolver && PlayerInventory.instance.activeGuns[PlayerInventory.instance.gunIndex].name == "Revolver_Pistol" && allowUpgrade) 
        {
            int amount = Mathf.RoundToInt(Revolver.price + upgradeCost);
            if (PlayerVitals.instance.money >= amount && !data.RuntimeUpgraded)
            {
                Mathf.RoundToInt(PlayerVitals.instance.money -= amount);
                PlayerInventory.instance.RemoveGun(AllGuns[11]);
                ShowUpgrading(11);
                StartCoroutine(StartUpgrading(AllGuns[11], data));
            }
        }
    }

    private IEnumerator StartUpgrading(GameObject type, GunData data)
    {
        changeToNextGun = true;
        allowUpgrade = false;
        PlayerInventory.instance.ChangeWeapon(PlayerInventory.instance.GetIndex()); // TEST
        yield return new WaitForSeconds(upgradeTime);
        // ADD finish sound!
        upgradeComplete = true;
        yield return new WaitUntil(() => isCollected); // Wait till player claims gun
        AddUpgradedWeapon(type, data);
    }
    public void ShowUpgrading(int index)
    {
        currentIndex = index;
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
        AllGuns[currentIndex].GetComponentInChildren<Renderer>().material = decal;

        foreach(GameObject gun in ShowcaseGuns)
        {
            gun.SetActive(false);
        }

        upgradeComplete = false;
        isCollected = false;
        allowUpgrade = true;
        changeToNextGun = false;
    }
}
