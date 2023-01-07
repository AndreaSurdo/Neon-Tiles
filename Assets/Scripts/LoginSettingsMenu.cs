using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSettingsMenu : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {        
        float MusicisActive=PlayerPrefs.GetFloat("ActiveMusic", 1);       
        
            if(MusicisActive==0){AudioListener.volume=0;Debug.Log("silent music");}
            else{AudioListener.volume=PlayerPrefs.GetFloat("VolumeValue", 1f);}
        
    }

    // Update is called once per frame
    void Update()
    {
}
}
