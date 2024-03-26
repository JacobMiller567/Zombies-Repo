using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScope : MonoBehaviour
{
    public GameObject sniper;
     [SerializeField] private GameObject sniperScope;
    [SerializeField] private MeshRenderer sniperMesh;
    [SerializeField] private GameObject handObject;
    [SerializeField] private Animator scopeAnimator;
    [SerializeField] private Animation sniperAnimation;
    private bool currentlyZoomed = false;

    void OnDisable()
    {
        sniperScope.SetActive(false);
        sniperMesh.enabled = true;
        handObject.SetActive(true);
        currentlyZoomed = false;
    }


    public void ZoomAnimation()
    {
        if (sniper.activeSelf && !currentlyZoomed)
        {
            sniperAnimation.Play();
        }
    }

    public void HideScope()
    {
      //  scopeAnimator.Rebind(); // TEST
        //scopeAnimator.SetBool("isZoom", false);
        sniperScope.SetActive(false);
        sniperMesh.enabled = true;
        handObject.SetActive(true);
        currentlyZoomed = false;
    }

    public void ScopeView() // Sniper animation event
    {
        if (sniper.activeSelf)
        {
            sniperMesh.enabled = false;
            handObject.SetActive(false);
            currentlyZoomed = true;
            //scopeAnimator.SetBool("isZoom", true);
            sniperScope.SetActive(true);
        }
        else
        {
           // scopeAnimator.SetBool("isZoom", false);
            sniperScope.SetActive(false);
        }
    }






}
