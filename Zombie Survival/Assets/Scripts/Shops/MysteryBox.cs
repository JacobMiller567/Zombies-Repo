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
    public int currentBoxIndex = 0;
    public List<GameObject> availableGuns;
    public List<GameObject> displayGuns;
    //public GameObject displayLocation;
    [SerializeField] private List<GameObject> availableBoxes;
    public bool isSpinning = false;
    public int maxSpins = 2;
    public int currentSpins = 0;
    private int lastLocation;
    public bool isCollected = false;
    public bool canCollect = false;
    private bool canceled = false;

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
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i].activeSelf)
            {
                currentBoxIndex = i;
            }
        }
    }

    public void StartSpin()
    {
        isSpinning = true;
        canceled = false;
        isCollected = false;
        int item = Random.Range(0, 23);
        if (item == 0) // 5%
        {
            rewardText.text = "Thunder Gun";
            canceled = true;
            StartCoroutine(HideText(0));
        }
        if (item > 0 && item <= 5) // 25%
        {
            rewardText.text = "Heavy Pistol";
            boxes[currentBoxIndex].GetComponent<SpinBox>().displayGuns[0].SetActive(true);
            displayGuns[0].SetActive(true);
            StartCoroutine(HideText(0));
        }
        if (item > 5 && item <= 10) // 20%
        {
            rewardText.text = "Revolver";
            boxes[currentBoxIndex].GetComponent<SpinBox>().displayGuns[1].SetActive(true);
            displayGuns[1].SetActive(true);
            StartCoroutine(HideText(1));
        }
        if (item > 10 && item <= 12) // 10%
        {
            rewardText.text = "AR15";
            boxes[currentBoxIndex].GetComponent<SpinBox>().displayGuns[4].SetActive(true);
            displayGuns[4].SetActive(true);
            StartCoroutine(HideText(4));
        }
        if (item > 12 && item <= 14) // 10%
        {
            rewardText.text = "AUG";
            boxes[currentBoxIndex].GetComponent<SpinBox>().displayGuns[3].SetActive(true);
            displayGuns[3].SetActive(true);
            StartCoroutine(HideText(3));
        }
        if (item > 14 & item <= 17) // 15%
        {
            rewardText.text = "MAC11";
            boxes[currentBoxIndex].GetComponent<SpinBox>().displayGuns[5].SetActive(true);
            displayGuns[5].SetActive(true);
            StartCoroutine(HideText(5));
        }
        if (item > 17 && item <= 19) // 10%
        {
            rewardText.text = "Pump-Action Shotgun";
            boxes[currentBoxIndex].GetComponent<SpinBox>().displayGuns[6].SetActive(true);
            displayGuns[6].SetActive(true);
            StartCoroutine(HideText(6));
        }
        if (item > 19) // 15%
        {
            rewardText.text = "FAMAS";
            boxes[currentBoxIndex].GetComponent<SpinBox>().displayGuns[2].SetActive(true);
            displayGuns[2].SetActive(true);
            StartCoroutine(HideText(2));
        }
        currentSpins += 1;
        StartCoroutine(CloseBox());
        //StartCoroutine(HideText());
    }

    IEnumerator HideText(int index)
    {
        // ADD: Make it so weapon is displayed in the box
        // ADD: Make it so player has a choice to collect weapon
        yield return new WaitForSeconds(1f);
        reward.SetActive(true);
        canCollect = true;
        //StartCoroutine(CloseBox());
        yield return new WaitUntil(() => isCollected || canceled); // Wait till player claims gun

        if (isCollected)
        {
            PlayerInventory.instance.UnlockGun(availableGuns[index]);
            StopCoroutine(CloseBox());
        }
        canCollect = false;
        reward.SetActive(false);
        isSpinning = false;
        if (currentSpins > maxSpins)
        {
            foreach (GameObject box in boxes)
            {
                Debug.Log("Changing Locations");
                box.SetActive(false);
                boxes[Random.Range(0, boxes.Length)].SetActive(true); // Spawn random mystery box
                //availableBoxes.
                currentSpins = 0;
                BoxChanged();
            }
        }
        boxes[currentBoxIndex].GetComponent<SpinBox>().ResetDisplay();
    }

    private void BoxChanged()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i].activeSelf)
            {
                currentBoxIndex = i;
            }
        }
    }

    IEnumerator CloseBox() // Glitched
    {
        if (isCollected)
        {
            Debug.Log("Broke out of ClosedBox 1");
            yield break;
        }
        if (!isCollected)
        {
            if (isCollected)
            {
                Debug.Log("Broke out of ClosedBox 2");
                yield break;
            }
            yield return new WaitForSeconds(6f);
            canceled = true;
            Debug.Log("Did not collect gun!");
        }

    }

    
}
