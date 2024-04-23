using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotMachine : MonoBehaviour
{
   public Sprite[] sprites;
   //public PlayerStats player;
   public float time;
   public bool play = false;
   public CheckWin check;

   void Update()
   {  
      RandomImage();
      //CheckImages();
   }

   public void RandomImage()
   {
      gameObject.GetComponent<UnityEngine.UI.Image>().sprite = sprites[Random.Range(0, sprites.Length)];
   }

   public void EnableOff()
   {
      Invoke("DelayNum", time);
      StartCoroutine(CheckSprite());
   }

    public void EnableOn()
    {
      enabled = true;
    }

   void DelayNum()
   {
      enabled = false;
   }

   IEnumerator CheckSprite()
   {
      yield return new WaitForSeconds(time);
      {
         SlotManager.Instance.spriteCheck.Add(gameObject.GetComponent<UnityEngine.UI.Image>().sprite); 
         SlotManager.Instance.Count += 1;

         if (SlotManager.Instance.Count >= 4) // 0.08% chance of 4 in a row!
         {
            check.CheckForWin();

            if (check.win != true)
            {
               check.ThreeInARow();
               check.CheckDoublePairs();
               check.CheckForPattern();
              // check.StartCoroutine(check.LoseGame()); // TEST
            }

            SlotManager.Instance.Restart(); // Displays replay and quit buttons!
         }
      }
   }



}
