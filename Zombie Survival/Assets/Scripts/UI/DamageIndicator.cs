using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public static DamageIndicator instance;
    public Image screenImage;
    private Color colorHolder;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        colorHolder = screenImage.color;
    }

    public void DisplayScreenDamage(float health)
    {
        //if (health > 75)
        if (health > Mathf.RoundToInt(PlayerVitals.instance.maxHealth * .75f))
        {
            colorHolder.a = 0f;
            screenImage.color = colorHolder;
           // Debug.Log("More then 75%");
        }
        //if (health < 76 && health > 50)
        if (health < Mathf.RoundToInt(PlayerVitals.instance.maxHealth * .76f) && health > Mathf.RoundToInt(PlayerVitals.instance.maxHealth * .50f))
        {
            colorHolder.a = 0.3f;
            screenImage.color = colorHolder;
            Debug.Log("Less then 75%");
        }
        //if (health < 51 && health > 35)
        if (health < Mathf.RoundToInt(PlayerVitals.instance.maxHealth * .51f) && health > Mathf.RoundToInt(PlayerVitals.instance.maxHealth * .35f))
        {
            colorHolder.a = 0.5f;
            screenImage.color = colorHolder;
            Debug.Log("Less then 50%");
        }
        //if (health < 36 && health > 20)
        if (health < Mathf.RoundToInt(PlayerVitals.instance.maxHealth * .36f) && health > Mathf.RoundToInt(PlayerVitals.instance.maxHealth * .20f))
        {
            colorHolder.a = 0.75f;
            screenImage.color = colorHolder;
            Debug.Log("Less then 35%");
        }
        //if (health < 21)
        if (health < Mathf.RoundToInt(PlayerVitals.instance.maxHealth * .21f) && health > Mathf.RoundToInt(PlayerVitals.instance.maxHealth * .1f))
        {
            colorHolder.a = .9f;
            screenImage.color = colorHolder;
            Debug.Log("Less then 20%");
        }
        if (health < Mathf.RoundToInt(PlayerVitals.instance.maxHealth * .11f))
        {
            colorHolder.a = 1f;
            screenImage.color = colorHolder;
            Debug.Log("Less then 10%");
        }
    }
}
