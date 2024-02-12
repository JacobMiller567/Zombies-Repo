using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider),typeof(Rigidbody))]
public class Move : MonoBehaviour
{
    public static Move instance;
    public float walkSpeed = 5;
    public float runSpeed = 10;
    public KeyCode runKey = KeyCode.LeftShift; 

    private Rigidbody rb;
    private float pushForce;
	private Vector3 pushDirection;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update() //FixedUpdate()
    {
        float speed = Input.GetKey(runKey) ? runSpeed : walkSpeed; 
        float inputX = Input.GetAxis("Horizontal") * speed * Time.deltaTime; 
        float inputZ = Input.GetAxis("Vertical") * speed * Time.deltaTime; 
        rb.transform.Translate(inputX, 0, inputZ);
    }

    public void IncreaseSpeed(float spd)
    {
        walkSpeed += spd;
        runSpeed += spd;
    }
}
