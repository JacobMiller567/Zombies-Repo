using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    public List<GameObject> guns;
    public int gunIndex = 0;

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
            gunIndex = (gunIndex + 1) % guns.Count;
            ChangeWeapon(gunIndex);
        }
     }

    public void ChangeWeapon(int index) // MAKE IT SO RELOADING WILL CONTINUE AFTER SWITCHING BACK WEAPONS??
    {
        foreach (var gun in guns)
        {
            gun.SetActive(false);
        }
        guns[index].SetActive(true);
        AmmoDisplay.instance?.WeaponChanged(guns[index].GetComponentInChildren<Gun>().GetGunData());
    }

    public void UnlockGun(GameObject newGun)  
    {
        guns.Add(newGun);
    }

    public void AddAmmo()//(int index)
    {
        //guns[index].GetComponentInChildren<Gun>().RefillAmmo();
        guns[gunIndex].GetComponentInChildren<Gun>().RefillAmmo();
    }

    public void ApplyPerk(int index)
    {
        if (index == 0)
        {
            PlayerVitals.instance.IncreaseHealth(100);
        }
        if (index == 1)
        {
            Move.instance.IncreaseSpeed(1);
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
