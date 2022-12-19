using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherLights : MonoBehaviour
{
    // Start is called before the first frame update
    public static Light lOthers;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        SetColors();
        lOthers.color= Color.Lerp(GameManager.before,GameManager.after,1);

    }
       
    public void SetColors()
    {
        lOthers=GetComponent<Light>();
        lOthers.color= Color.Lerp(LightBehaviour.colors[LightBehaviour.randomcolor],LightBehaviour.colors[LightBehaviour.randomcolor],0);
    }


}
