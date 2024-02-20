using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Find the main camera in the scene
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Ensure we have a valid reference to the main camera
        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera reference not found!");
            return;
        }

        // Update gun position and rotation to match the main camera
        transform.position = mainCamera.transform.position;
        transform.rotation = mainCamera.transform.rotation;
    }
}
