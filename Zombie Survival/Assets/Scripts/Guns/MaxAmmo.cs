using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxAmmo : MonoBehaviour
{
    private bool isOpened = false;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && !isOpened)
        {
            animator.SetTrigger("Open");
            isOpened = true;
            PlayerInventory.instance.AddAmmo();//(PlayerInventory.instance.GetIndex()); // Add ammo to current gun
            PlayerInventory.instance.AddGrenades(); // Refill grenades
            Destroy(gameObject, .5f);
        }
    }
}
