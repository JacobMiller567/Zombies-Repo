using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float speed = 10.0f;

    /*
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    */
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
