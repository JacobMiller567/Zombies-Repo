using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameQuality : MonoBehaviour
{
    public void ChangeGameQuality(TMP_Dropdown option)
    {
        switch (option.value)
        {
            case 0:
                UltraQuality();
                break;
            case 1:
                HighQuality();
                break;
            case 2:
                MediumQuality();
                break;
            case 3:
                LowQuality();
                break;
        }
    }
    private void UltraQuality()
    {
        QualitySettings.SetQualityLevel(6);
    }
    private void HighQuality()
    {
        QualitySettings.SetQualityLevel(5);
    }
    private void MediumQuality()
    {
        QualitySettings.SetQualityLevel(3);
    }
    private void LowQuality()
    {
        QualitySettings.SetQualityLevel(2);
    }
}
