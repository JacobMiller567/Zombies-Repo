using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverChamber : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animator revolverAnimator;
    private float animSpeed;
    void OnDisable()
    {
        animator.Rebind();
        animator.ResetTrigger("Spin");
    }
    void Start()
    {
       animSpeed = revolverAnimator.speed; 
    }
    public void SpinChamber()
    {
        animator.SetTrigger("Spin");
        revolverAnimator.speed = 0;
    }

    public void ResumeReload()
    {
       revolverAnimator.speed = animSpeed; 
    }
}
