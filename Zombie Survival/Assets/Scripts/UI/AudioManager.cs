using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //public Slider musicSlider;
    //public float musicSliderValue;
    //public Slider gunAudioSlider;
    //public float gunAudioSliderValue;
    public Slider gameSoundSlider;
    public float gameSoundSliderValue;


    [SerializeField] private AudioSource[] gunSounds;
    [SerializeField] private float[] gunVolumeDefault;

    [SerializeField] private AudioSource[] playerSounds;
    [SerializeField] private float[] playerVolumeDefault;

    [SerializeField] private AudioSource[] zombieSounds;
    [SerializeField] private float[] zombieVolumeDefault;
    
    [SerializeField] private AudioSource[] ambientSounds;
    [SerializeField] private float[] ambientVolumeDefault;

    [SerializeField] private AudioSource[] explosionSounds;
    [SerializeField] private float[] explosionVolumeDefault;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SetDefaultVolume();
    }

    public void SetDefaultVolume() // Set initial game volume to default
    {
        for (int i = 0; i < gunSounds.Length; i++)
        {
            gunVolumeDefault[i] = gunSounds[i].volume;
        }
        for (int i = 0; i < playerSounds.Length; i++)
        {
            playerVolumeDefault[i] = playerSounds[i].volume;
        }
        for (int i = 0; i < zombieSounds.Length; i++)
        {
            zombieVolumeDefault[i] = zombieSounds[i].volume;
        }
    }

    public void LoadDefaultVolume() // Reset game volume back to default
    {
        for (int i = 0; i < gunSounds.Length; i++)
        {
            gunSounds[i].volume = gunVolumeDefault[i];
        }
        for (int i = 0; i < playerSounds.Length; i++)
        {
            playerSounds[i].volume = playerVolumeDefault[i];
        }
        for (int i = 0; i < zombieSounds.Length; i++)
        {
            zombieSounds[i].volume = zombieVolumeDefault[i];
        }
    }

    public void AdjustGameVolume()
    {
        gameSoundSliderValue = gameSoundSlider.value;

        for (int i = 0; i < gunSounds.Length; i++)
        {
            gunSounds[i].volume = gameSoundSliderValue;
        }
        for (int i = 0; i < playerSounds.Length; i++)
        {
            playerSounds[i].volume = gameSoundSliderValue;
        }
        for (int i = 0; i < zombieSounds.Length; i++)
        {
            zombieSounds[i].volume = gameSoundSliderValue;
        }
    }



    public void GrenadeSound() // USE???
    {
        explosionSounds[0].Play();

    }

}
