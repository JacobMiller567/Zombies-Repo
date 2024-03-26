using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Color startColor = Color.white;
    public Color endColor = Color.black;
    [Range(0, 10)]
    public float speed = 1;
    private Material mat;
    private Renderer ren;
    private bool displayFlicker = false;
    private float delayTime;

    private void Awake()
    {
        ren = GetComponentInChildren<Renderer>();
        mat = ren.material;
        mat.EnableKeyword("_EMISSION");
    }
    private void OnEnable()
    {
        speed = 1;
        delayTime = 8f;
        //displayFlicker = false;
        //StartCoroutine(ShowHighlight());
    }
    private void OnDisable()
    {
        //displayFlicker = false;
        //StopCoroutine(ShowHighlight()); 
    }

    public void Disable()
    {
        speed = 0;
        StartCoroutine(DisableNow());
    }

    private IEnumerator DisableNow()
    {
        yield return new WaitForSeconds(0.5f);
        this.enabled = false;
    }

    private void Update()
    {
        delayTime -= Time.deltaTime;
        if (delayTime <= 0f)
        {
            speed = 3;
        }
        if (delayTime <= 4f)
        {
            //displayFlicker = true;
            mat.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1)));
        }
    /*
        if (displayFlicker)
        {
            mat.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1)));
        }
    */
    }

    public IEnumerator ShowHighlight()
    {
        yield return new WaitForSeconds(3f);
        displayFlicker = true;

    }
}
