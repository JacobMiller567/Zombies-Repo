using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadePlay : MonoBehaviour
{
    //public Camera PlayerCamera;
    //public Camera ArcadeCamera;
    //public GameObject slotManage;

    void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            //ArcadeCamera.enabled = true;
            //PlayerCamera.enabled = false;
        }
    }
    void OnTriggerExit(Collider collide)
    {
        if (collide.gameObject.tag == "Player")
        {
           // PlayerCamera.enabled = true;
           // ArcadeCamera.enabled = false;
            //slotManage.SetActive(false);
        }
    }
}
