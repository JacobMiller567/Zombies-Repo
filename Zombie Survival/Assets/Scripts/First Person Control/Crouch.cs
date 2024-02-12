using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Crouch : MonoBehaviour
{
    public float crouchAmount = 0.25f; // Amount we will crouch
    public KeyCode crouchKey = KeyCode.LeftControl; // Left control

    private Rigidbody rb;
    private float normalYLocalPosition = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        normalYLocalPosition = rb.transform.localScale.y; // Set normalYLocalPosition to be the rigidbody transforms localScale y 
    }


    void Update()
    {
        float currentYVal = Input.GetKey(crouchKey) // If left control key is pressed
                                    ? normalYLocalPosition - crouchAmount // if pressed then subtract our normalYLocalPosition by the crouchAmount
                                    : normalYLocalPosition; // Else have our normalYLocalPosition stay the same

        rb.transform.localScale = new Vector3(rb.transform.localScale.x,
                                                  currentYVal,
                                                  rb.transform.localScale.z); // Set our rigibodies transforms local scale to be a new Vector3 based on our localScale x, 
                                                                                // the currentYVal we assigned above, and the localScale z
    }
}
