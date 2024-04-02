using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject crosshairManager;
    public bool isPaused = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

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

    public void HideCrosshairs(TMP_Dropdown option)
    {
        switch (option.value)
        {
            case 0:
                crosshairManager.SetActive(true);
                break;
            case 1:
                crosshairManager.SetActive(false);
                break;
        }
    }
}
