using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    public List<GameObject> guns; // All guns unlocked
    public List<GameObject> activeGuns; // Current guns that can be used
    public int maxGunSlots = 2; // Max number of guns used at a time
    public int gunIndex = 0;
    [SerializeField] private GunShop shop;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            gunIndex = (gunIndex + 1) % activeGuns.Count;
            ChangeWeapon(gunIndex);
        }
    }

    public void ChangeWeapon(int index) // MAKE IT SO RELOADING WILL CONTINUE AFTER SWITCHING BACK WEAPONS??
    {    
        foreach (var gun in activeGuns)
        {
            gun.SetActive(false);
        }
        activeGuns[index].SetActive(true);
        AmmoDisplay.instance?.WeaponChanged(activeGuns[index].GetComponentInChildren<Gun>().GetGunData());    
    }

    public void UnlockGun(GameObject newGun)  
    {
        guns.Add(newGun);

        if (activeGuns.Count < maxGunSlots) // if slots are not full
        {
            activeGuns.Insert(gunIndex, newGun); // insert gun to current slot and shift other gun to next slot
        }
        else // if slots are full
        {
            activeGuns[gunIndex].SetActive(false);
            shop.OnGunsChanged();
            activeGuns.RemoveAt(gunIndex); // remove current gun
            activeGuns.Insert(gunIndex, newGun); // add gun to current slot
        }
        ChangeWeapon(gunIndex);
    }

    public void EquipGun(GameObject newGun)
    {
        if (activeGuns.Count < maxGunSlots) // if slots are not full
        {
            activeGuns.Insert(gunIndex, newGun); // insert gun to current slot and shift other gun to next slot
        }
        else // if slots are full
        {
            activeGuns[gunIndex].SetActive(false);
            shop.OnGunsChanged();
            activeGuns.RemoveAt(gunIndex); // remove current gun
            activeGuns.Insert(gunIndex, newGun); // add gun to current slot
        }
        ChangeWeapon(gunIndex);
    }

    public void RemoveGun(GameObject currentGun)
    {
        activeGuns[gunIndex].SetActive(false);
        activeGuns.RemoveAt(gunIndex); // remove current gun
        gunIndex = (gunIndex + 1) % activeGuns.Count;
        ChangeWeapon(gunIndex);
    }

    public void AddAmmo()
    {
       // guns[gunIndex].GetComponentInChildren<Gun>().RefillAmmo();
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

    public int GetIndex()
    {
        return gunIndex;
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
