using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) 
        {
            settingsMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) 
        {
            settingsMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
