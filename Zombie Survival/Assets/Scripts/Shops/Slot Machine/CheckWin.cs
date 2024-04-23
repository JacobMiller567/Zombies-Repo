using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckWin : MonoBehaviour
{


    //public Slots[] slotNumbers;
    public BettingSlider bet;
    //public PlayerStats player;
    public TMP_Text WinText;
    public GameObject WinHold;
    public GameObject LoseHold;
    public TMP_Text LoseText;
    public bool win = false;

    // 1x win if 2 in a row?
    // 2x win if double 2 in a row?
    // 3x win if 3 in a row
    // 5x win if 4 in a row 
    public void CheckForWin() // Check for 4 in a row 
    {
     
        bool isSame = true;
        //bool three = true;
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                isSame = transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite == transform.GetChild(i + 1).GetComponent<UnityEngine.UI.Image>().sprite;
            }
            else
            {
                isSame = isSame && transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite == transform.GetChild(i + 1).GetComponent<UnityEngine.UI.Image>().sprite;
            }

            
        }

        if (isSame)
        {
            Debug.Log("FOUR IN A ROW!");
            PlayerVitals.instance.money += (5 * bet.BetAmount); // 5x wins
            win = true;
            WinHold.SetActive(true);
            WinText.text = "Winnings: $" + (5 * bet.BetAmount); 

        }
        else
        {
          Debug.Log("Not four in a row!");
        }
    }

    public void ThreeInARow() // Check for 3 in a row out of 4 given slots
    {

        // If not already 4 in a row...
        if (win != true)
        {

            // Check if images: 0, 1, 2 are the same
            bool frontSame = true;
            bool backSame = true;
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    frontSame = transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite == transform.GetChild(i + 1).GetComponent<UnityEngine.UI.Image>().sprite;
                }
                else
                {
                    frontSame = frontSame && transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite == transform.GetChild(i + 1).GetComponent<UnityEngine.UI.Image>().sprite;
                }
            }
            if (frontSame)
            {
                Debug.Log("Three in a row!");
                PlayerVitals.instance.money += (3 * bet.BetAmount); // 3x wins
                WinHold.SetActive(true);
                WinText.text = "Winnings: $" + (3 * bet.BetAmount);
                win = true;
                return;
            }


            if (!frontSame)
            {
                // Check if images: 1, 2, 3 are the same
                for (int i = 1; i < 3; i++)
                {
                    if (i == 1)
                    {
                        backSame = transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite == transform.GetChild(i + 1).GetComponent<UnityEngine.UI.Image>().sprite;
                    }
                    else
                    {
                        backSame = backSame && transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite == transform.GetChild(i + 1).GetComponent<UnityEngine.UI.Image>().sprite;
                    }
                }
                if (backSame)
                {
                    Debug.Log("Three in a row!");
                    PlayerVitals.instance.money += (3 * bet.BetAmount); // 3x wins
                    WinHold.SetActive(true);
                    WinText.text = "Winnings: $" + (3 * bet.BetAmount);
                    win = true;
                    return;
                }
            }


            // Else: Not 3 in a row
            else
            {
                Debug.Log("Not three in a row!");
            }

        }
    }

    public void CheckDoublePairs()
    {
        bool firstPair = false;
        bool secondPair = false;

        if (win != true)
        {
            firstPair = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite == transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite;
            secondPair = transform.GetChild(2).GetComponent<UnityEngine.UI.Image>().sprite == transform.GetChild(3).GetComponent<UnityEngine.UI.Image>().sprite;


            if (firstPair && secondPair)
            {
                Debug.Log("TWO PAIRS IN A ROW");
                PlayerVitals.instance.money += (2 * bet.BetAmount); // 2x wins
                win = true;
                WinHold.SetActive(true);
                WinText.text = "Winnings: $" + (2 * bet.BetAmount);
            }
            else
            {
                Debug.Log("No Double Pairs");
            }

        }
    }

    public void CheckForPattern()
    {
        if (win != true)
        {
            bool firstPattern = false;
            bool secondPattern = false;

            firstPattern = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite == transform.GetChild(2).GetComponent<UnityEngine.UI.Image>().sprite;
            secondPattern = transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite == transform.GetChild(3).GetComponent<UnityEngine.UI.Image>().sprite;

            if (firstPattern && secondPattern)
            {
                Debug.Log("PATTERN");
                PlayerVitals.instance.money += (1 * bet.BetAmount); // 2x wins
                win = true;
                WinHold.SetActive(true);
                WinText.text = "Winnings: $" + (1 * bet.BetAmount); 
            }

        }

    }

/*
    public IEnumerator LoseGame() // TEST
    {
        yield return new WaitForSeconds(0.1f);
        if (win != true)
        {
            LoseHold.SetActive(true);
            LoseText.text = "Lose: - $" + bet.BetAmount; 
        }
    }
*/



}
