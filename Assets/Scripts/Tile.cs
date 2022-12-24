using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{   public float speed;
    Rigidbody rb;
    public GameObject ground;     
    public Material redmat;
    public Material greenmat;
    public Material yellowmat;
    public Material bluemat;
    public static bool isDead=false;
    public static float distance;
    public GameObject finishLine;
    int pick;
    public static bool canBeImmortal=true;


    // Start is called before the first frame update
    void Start()
    {
        ground=GameObject.Find("Ground light");  //equivalente del spostare il ground nell'inspector
        finishLine=GameObject.Find("FinishLine"); 
        //GameOverUI=GameObject.Find("Game Over");
        //PauseButton=GameObject.Find("Pause");
        //Debug.Log(ground.tag);
        RandomColor();        
        
    }
    
      public void DistanceCalculator(){
        distance = Vector3.Distance(gameObject.transform.position, finishLine.transform.position);
        //Debug.Log("Distance between objects: " + distance);
    }

    public void RandomColor()
    {
        pick = Random.Range(0,4);
        if(pick==0)
        {
            GetComponent<Renderer>().material = redmat;
            gameObject.tag="red";
        }
        else if(pick==1)
        {
            GetComponent<Renderer>().material = greenmat;
            gameObject.tag="green";
        }
        else if(pick==2)
        {
            GetComponent<Renderer>().material = yellowmat;
            gameObject.tag="yellow";
        }
        else if(pick==3)
        {
            GetComponent<Renderer>().material = bluemat;
            gameObject.tag="blue";
        }
        
    }

    // Update is called once per frame
    void Update()
    {   
        DistanceCalculator();
        if(distance<3)
        {
            if(GameManager.colorChanged==true)
            {
                Collider collider = gameObject.GetComponent<Collider>();      
                Destroy(collider);
            }
            
        }
        
            
        transform.Translate(Vector3.forward*speed*Time.deltaTime*GameManager.multiplier);
        
        if( Input.GetMouseButtonDown(0) )
     {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
             RaycastHit hit;
         if( Physics.Raycast(ray, out hit, 100.0f) && hit.transform.gameObject != null )
        {
            if(hit.transform.tag!=ground.tag && hit.transform.tag!="finishline" && PauseMenu.gameisover==false && PauseMenu.gameispaused==false){
                isDead=true;
                GameObject.Destroy(hit.transform.gameObject);          
                }

            else if(hit.transform.tag==ground.tag && hit.transform.gameObject != ground && PauseMenu.gameispaused==false)
            {
                //Debug.Log("Stesso colore");
                //GameOverUI.SetActive(true);
                //PauseButton.SetActive(false);
                PauseMenu.gameisover=true;
            }

        }
   }
    }
    private void Awake()
    {
        rb= GetComponent<Rigidbody>();
    }
    

     private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="finishline" && gameObject.tag==ground.tag)
        {
            Collider collider = gameObject.GetComponent<Collider>();      
            Destroy(collider);
        }
        else if(other.gameObject.tag=="finishline" && gameObject.tag!=ground.tag && distance<3)
        {
            PauseMenu.gameisover=true;
        }
    }

    private void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }
}

    