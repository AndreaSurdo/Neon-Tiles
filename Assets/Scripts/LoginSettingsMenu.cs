using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSettingsMenu : MonoBehaviour
{

    public GameObject BGMusic;
    public AudioSource Music;
    
    // Start is called before the first frame update
    void Start()
    {    Music=BGMusic.GetComponent<AudioSource>();    
        float MusicisActive=PlayerPrefs.GetFloat("ActiveMusic", 1);       
        
            if(MusicisActive==0){AudioListener.volume=0;Debug.Log("silent music");}
            else{AudioListener.volume=PlayerPrefs.GetFloat("VolumeValue", 1f); Music.Play();}
        
    }

    // Update is called once per frame
    void Update()
    {
}
}
