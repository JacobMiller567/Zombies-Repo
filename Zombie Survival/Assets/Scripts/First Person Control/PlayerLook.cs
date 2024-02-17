using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float sensitivity = 1f;
    public float smoothing = 2f;

    [SerializeField] private Transform charCamera;
    private Vector2 currentMouseLook;
    private Vector2 appliedMouseDelta;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       // charCamera = Camera.main.transform;
    }

    void Update()
    {
        Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);
        appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1f / smoothing);
        currentMouseLook += appliedMouseDelta;

        currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90f, 90f);

        charCamera.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
    }
}
