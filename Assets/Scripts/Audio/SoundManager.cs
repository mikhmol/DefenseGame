using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }

        LoadSetup();
    }

    public void ChangeMusicVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveSetup();
    }

    private void LoadSetup()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void SaveSetup()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
