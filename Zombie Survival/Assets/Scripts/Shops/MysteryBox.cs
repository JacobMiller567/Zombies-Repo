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
    [SerializeField] private List<GameObject> availableBoxes;
    [SerializeField] private int[] mysteryboxWeight = {2, 23, 18, 10, 10, 15, 10, 12}; // Thunder Gun, Heavy Pistol, Revolver, AR15, AUG, MAC11, Pump-Action Shotgun, FAMAS 
    public bool isSpinning = false;
    public int maxSpins = 2;
    public int currentSpins = 0;
    public float removeGunTime = 10f;
    private Coroutine boxCoroutine;
    private int lastLocation;
    private float gunTimeHolder;
    private int gunIndex;
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
        gunTimeHolder = removeGunTime;
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
            gunIndex = 0;
        }
        if (item > 0 && item <= 5) // 25%
        {
            rewardText.text = "Heavy Pistol";
            gunIndex = 0;
        }
        if (item > 5 && item <= 10) // 20%
        {
            rewardText.text = "Revolver";
            gunIndex = 1;
        }
        if (item > 10 && item <= 12) // 10%
        {
            rewardText.text = "AR15";
            gunIndex = 4;
        }
        if (item > 12 && item <= 14) // 10%
        {
            rewardText.text = "AUG";
            gunIndex = 3;
        }
        if (item > 14 & item <= 17) // 15%
        {
            rewardText.text = "MAC11";
            gunIndex = 5;
        }
        if (item > 17 && item <= 19) // 10%
        {
            rewardText.text = "Pump-Action Shotgun";
            gunIndex = 6;
        }
        if (item > 19) // 15%
        {
            rewardText.text = "FAMAS";
            gunIndex = 2;
        }
        currentSpins += 1;
        StartCoroutine(HideText(gunIndex));
       // boxCoroutine = StartCoroutine(HideText(gunIndex));

    /* // TEST: Weighted Mystery Box
        int totalWeight = 100; // Total sum of weights
        int randomNumber = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;
        int selectedGun = 0;
        for (int i = 0; i < mysteryboxWeight.Length; i++)
        {
            cumulativeWeight += mysteryboxWeight[i];
            if (randomNumber < cumulativeWeight)
            {
                selectedGun = i;
                break;
            }
        }
    */
    }

    IEnumerator HideText(int index)
    {
        yield return new WaitForSeconds(0.35f);
        //displayGuns[index].SetActive(true);
        boxes[currentBoxIndex].GetComponent<SpinBox>().displayGuns[index].SetActive(true);
        reward.SetActive(true);
        canCollect = true;
        StartCoroutine(CloseBox());
        yield return new WaitUntil(() => isCollected || canceled); // Wait till player claims gun or runs out of time

        if (isCollected) // If gun is collected
        {
            PlayerInventory.instance.UnlockGun(availableGuns[index]);
            StopCoroutine(CloseBox());
        }
        canCollect = false;
        reward.SetActive(false);
        isSpinning = false;
        if (currentSpins > maxSpins)
        {
            foreach (GameObject box in boxes) // FIX: Changing boxes causing glitch where box stays open
            {
              //  Debug.Log("Changing Locations");
                box.SetActive(false);
                boxes[Random.Range(0, boxes.Length)].SetActive(true); // Spawn random mystery box
                currentSpins = 0;
                BoxChanged();
            }
        }
        boxes[currentBoxIndex].GetComponent<SpinBox>().ResetDisplay();
        removeGunTime = gunTimeHolder; // TEST
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

    public IEnumerator CloseBox()
    {
        removeGunTime = gunTimeHolder; // TEST
        if (!isCollected)
        {
            removeGunTime = gunTimeHolder; // TEST
            yield return new WaitForSeconds(removeGunTime);
            if (!isCollected)
            {
                canceled = true;
                Debug.Log("Did not collect gun!");
            }
        }
        else
        {
            Debug.Log("Gun Collected!");
            yield return true; // TEST?
        }
    }

    
}
