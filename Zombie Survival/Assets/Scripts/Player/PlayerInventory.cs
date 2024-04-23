using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    public List<GameObject> guns; // All guns unlocked
    public List<GameObject> activeGuns; // Current guns that can be used
    public GameObject grenades;
    public int maxGunSlots = 2; // Max number of guns used at a time
    public int maxGrenades = 4;
    public int currentGrenades = 4;
    public int gunIndex = 0;
    [SerializeField] private Transform throwZone;
    [SerializeField] private GunShop shop;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        activeGuns[gunIndex].SetActive(true);
        currentGrenades = maxGrenades;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !SettingsManager.Instance.isPaused)
        {
            gunIndex = (gunIndex + 1) % activeGuns.Count;
            ChangeWeapon(gunIndex);
        }

        if (Input.GetKeyDown(KeyCode.Q)) // FIX: Move to new script!
        {
            if (currentGrenades > 0 && !SettingsManager.Instance.isPaused)
            {
                AudioManager.Instance.GrenadeSound();// TEST
                ThrowGrenade();

            }
        }
    }

    public void ChangeWeapon(int index) // MAKE IT SO RELOADING WILL CONTINUE AFTER SWITCHING BACK WEAPONS??
    {
        if (activeGuns.Count > 1)
        {
            foreach (var gun in activeGuns)
            {
                gun.SetActive(false);
            }
            activeGuns[index].SetActive(true);
            AmmoDisplay.instance?.WeaponChanged(activeGuns[index].GetComponentInChildren<Gun>().GetGunData());
        }
        else if (UpgradeShop.Instance.changeToNextGun && activeGuns.Count > 0 && activeGuns.Count < 3)
        {
            index = (gunIndex + 1) % activeGuns.Count;
            activeGuns[index].SetActive(true);
            AmmoDisplay.instance?.WeaponChanged(activeGuns[index].GetComponentInChildren<Gun>().GetGunData());
            UpgradeShop.Instance.changeToNextGun = false;
        }   
        else
        {
            Debug.Log("Only 1 gun");
        }
    }

    public void UnlockGun(GameObject newGun)  
    {
        if (activeGuns.Contains(newGun))
        {
          int i = 0;
            
            foreach (GameObject gun in activeGuns)
            {
                if (gun == newGun)
                {
                   activeGuns[i].GetComponentInChildren<Gun>().RefillAmmo();
                   Debug.Log("Adding ammo for: " + newGun.name);
                   break;
                }
                i++;
            }
            //GameObject testGun = Instantiate(newGun); // TEST
           // guns.Add(testGun); // TEST
           // newGun = testGun;
        }

        if (!guns.Contains(newGun)) // If player has not unlocked this gun
        {
            guns.Add(newGun);
        }

        if (!activeGuns.Contains(newGun)) // If player is not currently using this gun
        {
            if (activeGuns.Count < maxGunSlots)
            {
                activeGuns.Insert(gunIndex, newGun); // insert gun to current slot and shift other gun to next slot
            }
            else 
            {
                activeGuns[gunIndex].SetActive(false);
                shop.OnGunsChanged();
                ResetGunValues(activeGuns[gunIndex].GetComponentInChildren<Gun>().GetGunData()); // reset guns values
                activeGuns.RemoveAt(gunIndex); // remove current gun
                activeGuns.Insert(gunIndex, newGun); // add gun to current slot
            }
            ChangeWeapon(gunIndex);
        }
    }

    public void EquipGun(GameObject newGun)
    {
        if (activeGuns.Contains(newGun))
        {
            Debug.Log("Duplicate: " + newGun.name);
        }

        if (activeGuns.Count < maxGunSlots) // if slots are not full
        {
            activeGuns.Insert(gunIndex, newGun); // insert gun to current slot and shift other gun to next slot
        }
        else 
        {
            activeGuns[gunIndex].SetActive(false);
            shop.OnGunsChanged();
            ResetGunValues(activeGuns[gunIndex].GetComponentInChildren<Gun>().GetGunData()); // reset guns values
            activeGuns.RemoveAt(gunIndex); // remove current gun
            activeGuns.Insert(gunIndex, newGun); // add gun to current slot
        }
        ChangeWeapon(gunIndex);
    }

    public void RemoveGun(GameObject currentGun)
    {
        ResetGunValues(activeGuns[gunIndex].GetComponentInChildren<Gun>().GetGunData()); // reset guns values
        activeGuns[gunIndex].SetActive(false);
        activeGuns.RemoveAt(gunIndex); // remove current gun
        gunIndex = (gunIndex + 1) % activeGuns.Count;
        ChangeWeapon(gunIndex);
    }

    public void AddAmmo() 
    {
       activeGuns[gunIndex].GetComponentInChildren<Gun>().RefillAmmo();
    }

    public void ApplyPerk(int index)
    {
        if (index == 0)
        {
            PlayerVitals.instance.IncreaseHealth(100);
        }
        if (index == 1)
        {
            PlayerMovement.instance.IncreaseSpeed(1f, 50f);
        }
        if (index == 2)
        {
            maxGunSlots += 1;
        }
    }

    public void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenades, throwZone.position, Quaternion.identity);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        Vector3 throwDirection = throwZone.forward;
        rb.AddForce(throwDirection * 12f, ForceMode.Impulse);
        Vector3 upwardsDirection = throwZone.up;
        rb.AddForce(upwardsDirection * 2f, ForceMode.Impulse);
        currentGrenades -= 1;
        IconDisplay.Instance.RemoveGrenade();
    }

    public void AddGrenades()
    {
        currentGrenades = maxGrenades;
        IconDisplay.Instance.AddedGrenades();
    }

    public int GetIndex()
    {
        return gunIndex;
    }


    public void ResetGunValues(GunData data)
    {
        data.RuntimeUpgraded = data.upgraded;
        data.RuntimeDamage = data.damage;
        data.RuntimeAmmo = data.ammo;
        data.RuntimeMagazine = data.magazineSize;
        data.RuntimeFireRate = data.fireRate;
        data.RuntimeMaxRange = data.maxRange;
        data.RuntimeReloadSpeed = data.reloadSpeed;
        activeGuns[gunIndex].GetComponentInChildren<Renderer>().material = activeGuns[gunIndex].GetComponentInChildren<Gun>().gunDecal;
    }

    /*
     void SwitchGunByMouse(float direction)
    {
        if (direction == 0f)
            return;

        int nextGun = -1;

        // Ensure there are available guns
        if (gunsAvailable.Count > 0)
        {
            if (direction > 0f) // Scrolling up
            {
                for (int i = 0; i < gunsAvailable.Count; i++)
                {
                    if (currentGun == gunsAvailable[i].idNumber)
                        continue;

                    if (currentGun < gunsAvailable[i].idNumber)
                        nextGun = gunsAvailable[i].idNumber;
                }

                if (nextGun == -1)
                    nextGun = 0;
            }
            else if (direction < 0f) // Scrolling down
            {
                nextGun = gunsAvailable[currentGun].idNumber - 1;
                List<Gun> reversedGuns = gunsAvailable;
                reversedGuns.Reverse();

                for (int i = 0; i < reversedGuns.Count; i++)
                {
                    if (currentGun == reversedGuns[i].idNumber)
                        continue;

                    if (currentGun < reversedGuns[i].idNumber)
                        nextGun = reversedGuns[i].idNumber;
                }

                if (nextGun == -1)
                    nextGun = 0;
            }

            // Switch to the next gun
            SwitchGun(nextGun);
        }
    }

    void SwitchGun(int gunNumber)
    {
        // Implement your gun switching logic here
        // Set the active gun, update UI, etc.
    }
    */

}
