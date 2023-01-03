using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider GeneralVolumeSlider;

    public void LoadVolume(){
        float volumeValue=PlayerPrefs.GetFloat("VolumeValue");
        GeneralVolumeSlider.value=volumeValue;
        AudioListener.volume=volumeValue;
    }

    public void SaveVolume(){
        float volumeValue=GeneralVolumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue",volumeValue);
        LoadVolume();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadVolume();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
