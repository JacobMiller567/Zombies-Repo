using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotManager : MonoBehaviour
{

    public static SlotManager Instance;
    public SlotMachine[] slotNumbers;
    public Sprite[] sprites;
    public List <Sprite> spriteCheck;
    public int Count;
    public CheckWin check;
   // public BettingSlider betSlider;
    public Slider slider;
   // public PlayerStats stats;

    public GameObject Replay;
    public GameObject Quit;

    //public TMP_Text WinText;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }



    public void NewGame() // Start spinning
    {
        if (spriteCheck != null)
        {
            spriteCheck.Clear();
            check.WinText.text = null;
            check.LoseText.text = null;
            Count = 0;
            check.win = false;
           // betSlider.slider.value = 1; 
            Replay.SetActive(false);
           // Quit.SetActive(false);

        }
    }

    public void Restart()
    {
        //slider.BetAmount = 0;
       // betSlider.slider.maxValue = stats.PlayerMoney;
        //betSlider.slider.value = 1; 
       // slider.value = 1; // FIXED?!
        Replay.SetActive(true);
       // Quit.SetActive(true);
    }



/*
    public void StartSlots()
    {
        for (int i = 0; i < slotNumbers.Length; i++)
        {
            //slotNumbers[i].GetComponent<UnityEngine.UI.Image>().sprite = sprites[Random.Range(0, sprites.Length)];
            slotNumbers[i].RandomImage();
        }

    }
*/

/*
    void Update()
    {
        for (int i = 0; i < slotNumbers.Length; i++)
        {
            if (slotNumbers[i].SlotNum.text == "2")
            {
                Debug.Log("BANNNNNNNNNNNG");

            }
        }
    }
*/


}
