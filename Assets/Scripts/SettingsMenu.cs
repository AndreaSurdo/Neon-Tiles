using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public Slider GeneralVolumeSlider;
    public GameObject MusicOn;
    public GameObject MusicOff;
    public GameObject SfxOn;
    public GameObject Sfxoff;
    public GameObject BGMusic;
    public GameObject canvas;
    public GameObject prefabTile;
    public GameObject gameManager;
    public AudioSource Music;
    public AudioSource[] canvasSfx;


    public void TurnMusicOff()
    {
        MusicOn.SetActive(false);
        MusicOff.SetActive(true);
        BGMusic=GameObject.Find("Backgroung Music");
        Music=BGMusic.GetComponent<AudioSource>();
        Music.Pause();
        PlayerPrefs.SetFloat("ActiveMusic", 0);
    }

    public void TurnSfxOff()
    {
        SfxOn.SetActive(false);
        Sfxoff.SetActive(true);
        // Find the AudioSource that you want to keep playing
        AudioSource audioSourceToKeepPlaying = Music;/* your code here to find the audio source */;

        // Find all the AudioSource components in the scene
        AudioSource[] audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        // Mute all the audio sources except for the one that you want to keep playing
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != audioSourceToKeepPlaying)
            {
                audioSource.mute=true;
            }
        }
        PlayerPrefs.SetFloat("ActiveSfx", 0);

    }

    public void TurnSfxOn()
    {
        SfxOn.SetActive(true);
        Sfxoff.SetActive(false);
        // Find the AudioSource that you want to keep playing
        AudioSource audioSourceToKeepPlaying = Music;/* your code here to find the audio source */;

        // Find all the AudioSource components in the scene
        AudioSource[] audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        // Mute all the audio sources except for the one that you want to keep playing
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != audioSourceToKeepPlaying)
            {
                audioSource.mute=false;
            }
        }
        PlayerPrefs.SetFloat("ActiveSfx", 1);

    }
    public void TurnMusicOn()
    {
        float MusicisActive=PlayerPrefs.GetFloat("ActiveMusic", 1);
        MusicOn.SetActive(true);
        MusicOff.SetActive(false);
        BGMusic=GameObject.Find("Backgroung Music");        
        Music=BGMusic.GetComponent<AudioSource>();
        if(MusicisActive==0){ Music.Play();}
        PlayerPrefs.SetFloat("ActiveMusic", 1);
    }

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
        float MusicisActive=PlayerPrefs.GetFloat("ActiveMusic", 1);
        if(MusicisActive==1){TurnMusicOn();}
        else{TurnMusicOff();}
        float SfxisActive=PlayerPrefs.GetFloat("ActiveSfx", 1);
        if(SfxisActive==1){TurnSfxOn();}      
        
        
        if(SceneManager.GetActiveScene().name=="NeonLoginPage")
        {
            if(MusicisActive==0){AudioListener.volume=0;Debug.Log("silent music");}
            else{AudioListener.volume=PlayerPrefs.GetFloat("VolumeValue", 1f);}
        }
        else{LoadVolume();}
        
    }

    // Update is called once per frame
    void Update()
    {
        float SfxisActive=PlayerPrefs.GetFloat("ActiveSfx", 1);
        if(SfxisActive==0){TurnSfxOff();}
        if(SceneManager.GetActiveScene().name=="Game")
        {
           AudioListener.volume=GeneralVolumeSlider.value;
        }
}
}
