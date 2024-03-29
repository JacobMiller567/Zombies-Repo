﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Jump : MonoBehaviour
{
    public float jumpStrength = 5;
    public float jumpCooldown = 0.5f; // Cooldown period between jumps
     private float lastJumpTime; // Time when the last jump occurred
    public KeyCode jumpKey = KeyCode.Space; // Spacebar

    private Rigidbody rb;
    private bool isGrounded = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the rigidbody component from our player
    }

    void Update() //FixedUpdate()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded && Time.time - lastJumpTime >= jumpCooldown) // If spacebar is pressed and player is touching the ground
        {
            rb.AddForce(rb.transform.up * jumpStrength, ForceMode.Impulse); // add upwards force based on our jumpStrength instantly.
            lastJumpTime = Time.time; 
        }
    }

    private void OnCollisionEnter(Collision collision) // When player first collides
    {
        if (collision.gameObject.CompareTag("Ground")) // If the object collided with is the ground
        {
            isGrounded = true; // Set the isGrounded bool to true
        }
    }

    private void OnCollisionExit(Collision collision) // When player is leaving the collision
    {
        if (collision.gameObject.CompareTag("Ground")) // If that object was the ground
        {
            isGrounded = false; // Set the isGrounded bool to false
        }
    }
}
