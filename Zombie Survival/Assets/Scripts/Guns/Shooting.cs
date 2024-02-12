using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shooting : MonoBehaviour
{
    public static Action inputShooting;
    public static Action inputReloading;

    public Camera cam;
    public Camera gunCam; // TEST
    public float normalFov = 60;
    public float multiplier = 2;
    public float zoomTime = 1;

    [SerializeField] private KeyCode reloadKey = KeyCode.R;
    // [SerializeField] private AudioSource reloadAudio;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            inputShooting?.Invoke(); 
        }
        if (Input.GetMouseButtonDown(0)) 
        {
            inputShooting?.Invoke();
        }
        if (Input.GetKeyDown(reloadKey))
        {
            inputReloading?.Invoke(); 
            //reloadAudio.Play();
        }
        AimSights();
    }

    private void AimSights()
    {
        if (Input.GetMouseButton(1))
        {
            ZoomCamera(normalFov / multiplier);
        }
        else if (cam.fieldOfView != normalFov)
        {
            ZoomCamera(normalFov);
        }
    }
    private void ZoomCamera(float target)
    {
        float angle = Mathf.Abs((normalFov / multiplier) - normalFov);
        cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, target, angle / zoomTime * Time.deltaTime);
        gunCam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, target, angle / zoomTime * Time.deltaTime);
        // Move the ZoomContainer closer to the player (or gun)
        // transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime);
    }

}
