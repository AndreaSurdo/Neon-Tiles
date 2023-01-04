using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public Slider GeneralVolumeSlider;

    public void LoadVolume(){
        float volumeValue=PlayerPrefs.GetFloat("VolumeValue", 1f);
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
        if(SceneManager.GetActiveScene().name=="NeonLoginPage")
        {
           AudioListener.volume=PlayerPrefs.GetFloat("VolumeValue", 1f);
        }
        else{LoadVolume();}
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name=="Game")
        {
           AudioListener.volume=GeneralVolumeSlider.value;
        }
}
}
