using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BettingSlider : MonoBehaviour
{
    public Slider slider;
    public int BetAmount;
    public TMP_Text BetText;


    void Start()
    {
        //slider.maxValue = player.PlayerMoney; // TEST
        slider.value = 0;
        BetText.text = "Bet Amount: $" + BetAmount.ToString();
    }


    void Update()
    {
        //slider.maxValue = player.PlayerMoney;
    }

    public void RefreshBet()
    {
        slider.value = 1;
    }


    public void PlaceBet()
    {
        if (PlayerVitals.instance.money > 0) //&& player.PlayerMoney >= BetAmount)
        {
            slider.maxValue = PlayerVitals.instance.money; // TEST
            BetAmount = Mathf.RoundToInt(slider.value);
            BetText.text = "Bet Amount: $" + BetAmount.ToString();
            //Debug.Log("Placing Bet");
        }
        else
        {
            return;
        }
    }

    public void ConfirmBet()
    {
        if (PlayerVitals.instance.money >= BetAmount)
        PlayerVitals.instance.money -= BetAmount;
        Debug.Log("Bet Confirmed: + $" + BetAmount.ToString());
    }


    public void BetButton(int index)
    {
        if (PlayerVitals.instance.money > 0) //&& player.PlayerMoney >= BetAmount)
        {
            switch (index)
            {
                case 0:
                Debug.Log("5%");
                BetAmount = Mathf.RoundToInt(PlayerVitals.instance.money * 0.05f); 
                break;
                case 1:
                Debug.Log("10%");
                BetAmount = Mathf.RoundToInt(PlayerVitals.instance.money * 0.1f); 
                break;
                case 2:
                Debug.Log("20%");
                BetAmount = Mathf.RoundToInt(PlayerVitals.instance.money * 0.2f);  
                break;
                case 3:
                Debug.Log("35%"); 
                BetAmount = Mathf.RoundToInt(PlayerVitals.instance.money * 0.35f); 
                break;
                case 4:
                Debug.Log("50%"); 
                BetAmount = Mathf.RoundToInt(PlayerVitals.instance.money * 0.5f); 
                break;
                case 5:
                Debug.Log("75%"); 
                BetAmount = Mathf.RoundToInt(PlayerVitals.instance.money * 0.75f); 
                break;

            }
            BetText.text = "Bet Amount: $" + BetAmount.ToString();
            //ConfirmBet();
        }
    }

}
