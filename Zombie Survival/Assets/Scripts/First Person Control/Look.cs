using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(99)]
public class Look : MonoBehaviour // More complex then then other scripts
{
    public float sensitivity = 1;
    public float smoothing = 2;

    private Transform charCamera;
    //[SerializeField] Transform gunCamera; // TEST
    private Vector2 currentMouseLook;
    private Vector2 appliedMouseDelta;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Used to make it so the cursor wont move around on the screen or outside of the application
        Cursor.visible = false; // Used to make it so the cursor is not visible
        charCamera = Camera.main.transform; // Set the transform to be the main cameras transform
    }

    void Update()
    {
        Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing); // Calculate the smoothed mouse movement in a 2D space
        appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing); // Linearly interpolates between the appliedMouseDelta and the smoothMouseDelta by 1 divided by the smoothing
        currentMouseLook += appliedMouseDelta; // Add the appliedMouseDelta to the currentMouseLook
        currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90); // This will clamp the mouse y axis betweem -90 and 90

        charCamera.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right); // This will cause the charCamera to rotate around its local right axis by -currentMouseLook.y
        //gunCamera.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right); // TEST
        transform.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up); // This will cause the transform to rotate around its local up axis by currentMouseLook.x
    }
}
