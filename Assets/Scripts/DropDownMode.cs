using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownMode : MonoBehaviour
{
    TMPro.TMP_Dropdown dropdownMenu;
    

    public void ModeSelector(int index)
    {
        switch(index)
        {
            case 0: PlayerPrefs.SetFloat("NormalMode",0); break;
            case 1: PlayerPrefs.SetFloat("NormalMode",1); break;
        }

    }

    public void CheckMode()
    {
        float gameMode=PlayerPrefs.GetFloat("NormalMode",0);
        if(gameMode==0)
        {
            dropdownMenu.value=0;
            
        }
        else if(gameMode==1)
        {
            dropdownMenu.value=1;
            
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        dropdownMenu=FindObjectOfType<TMPro.TMP_Dropdown>();
        CheckMode();
    }

    // Update is called once per frame
    void Update()
    {       
        
        
    }
}
