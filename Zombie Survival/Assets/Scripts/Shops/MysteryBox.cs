using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MysteryBox : MonoBehaviour
{
    public static MysteryBox instance;
    [SerializeField] private GameObject reward;
    [SerializeField] private TextMeshProUGUI rewardText;
    
    [SerializeField] private GameObject[] boxes;
    public bool isSpinning = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        boxes[Random.Range(0, boxes.Length)].SetActive(true); // Spawn random mystery box
    }

    public void StartSpin()
    {
        isSpinning = true;
        int item = Random.Range(0, 20);
        if (item == 0) // 5%
        {
            rewardText.text = "Thunder Gun";
        }
        if (item > 0 && item <= 5) // 25%
        {
            rewardText.text = "Heavy Pistol";
        }
        if (item > 5 && item <= 10) // 20%
        {
            rewardText.text = "Revolver";
        }
        if (item > 10 && item <= 12) // 10%
        {
            rewardText.text = "AR15";
        }
        if (item > 12 && item <= 14) // 10%
        {
            rewardText.text = "AUG";
        }
        if (item > 14 & item <= 17) // 15%
        {
            rewardText.text = "MAC11";
        }
        if (item > 17) // 10%
        {
            rewardText.text = "Pump-Action Shotgun";
        }

        StartCoroutine(HideText());
    }

    IEnumerator HideText()
    {
        reward.SetActive(true);
        yield return new WaitForSeconds(2f);
        reward.SetActive(false);
        isSpinning = false;
    }
}
