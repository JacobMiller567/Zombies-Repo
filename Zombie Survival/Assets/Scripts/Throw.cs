using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public float throwForce = 10f; // Adjust this value to control the throw force

    public void ThrowGrenade()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Debug.Log("GRENADE OUT");
        if (rb != null)
        {
            // Calculate the throw direction based on the forward direction of the GameObject
            Vector3 throwDirection = transform.forward;

            // Apply force to throw the grenade
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Rigidbody component not found. Cannot throw grenade.");
        }
    }
}
