using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightBehaviour : MonoBehaviour
{
    public static Color purple= new Color(255,0,247,1.0f);
    public static Color[] colors= new Color[] {Color.red,Color.blue,Color.green,Color.yellow};
    public static Light l1;
    public static int randomcolor;
    // Start is called before the first frame update
    
    void Start()
    {
        SetColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColors()
    {
        randomcolor = Random.Range(0,4);
        l1=GetComponent<Light>();
        l1.color= Color.Lerp(colors[randomcolor],colors[randomcolor],0);   
    }
}
