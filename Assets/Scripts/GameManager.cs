using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    
    public GameObject tile; 
    public GameObject ground; 
    public GameObject finishLine;  
    public int randTile=0;
    public Transform[] spawnPoints;
    public static float score=0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI NextColorIs;
    public TextMeshProUGUI ActualColorName;
    public Light[] lights;
    static Color32 red=new Color32(180,0,0,255);
    static Color32 blue=new Color32(0,0,173,255);
    static Color32 green=new Color32(0,176,0,255);
    static Color32 yellow=new Color32(255,214,0,255);
    static Color32 diprova=new Color32(0,217,197,255);
    public static Color32[] colors= new Color32[] {red,blue,green,yellow};
    public string[] nextcolor= new string[] {"red","blue","green","yellow"};
    public static int highscore;
    public float distance;
    public static bool colorChanged=false;
    public float timer = 0;
    public float waitTime;
    public static string displayName;
    public AudioSource Hitmark;
    
    //public GameObject playButton;
    //public Button pauseButton;
    //public GameObject player;

    public static float multiplier=1;
    public static Color before;
    public static Color after;
    public static string currentLightColor;
    public GameObject NextColorButton;
    public void DistanceCalculator(){
        distance = Vector3.Distance(tile.transform.position, finishLine.transform.position);
        Debug.Log("Distance between objects: " + distance);
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.gameisover=false;
        //UpdateHighScoreText();
        /*PlayfabManager instance= gameObject.AddComponent<PlayfabManager>();
        instance.GetDisplayName();*/
        GameStart(); 
    }  


    // Update is called once per frame
    void Update()
    {  
       //DistanceCalculator();
       ScoreUp();
       SpeedUp();
       timer += Time.deltaTime;
       if (timer >= 1f){
            colorChanged=false;
            timer = 0;                  
        }
    }


    IEnumerator SpawnObstacles()
    {
        
        while(true)
        {
            if(score<10){waitTime = Random.Range(0.5f,0.75f);}
            else if(score<25){waitTime = Random.Range(0.4f,0.65f);}
            else if(score<50){waitTime = Random.Range(0.3f,0.5f);}
            else if(score<75){waitTime = Random.Range(0.25f,0.4f);}
            else{waitTime = Random.Range(0.15f,0.25f);}
            yield return new WaitForSeconds(waitTime);
            randTile = Random.Range(0,4);            
            Instantiate(tile, spawnPoints[randTile].position, Quaternion.identity);
        }
    }

    IEnumerator ChangeGroundColor()
    {
        //Material method
        int randomcolor = Random.Range(0,4);
        ground.GetComponent<Renderer>().material.color=colors[randomcolor];
        before=ground.GetComponent<Renderer>().material.color;
        after=before;
        if(after==colors[0])
        {
            ground.tag="red";
            currentLightColor="red";
        }
        else if(after==colors[1])
        {
            ground.tag="blue";
            currentLightColor="blue";
        }
        else if(after==colors[2])
        {
            ground.tag="green";
            currentLightColor="green";
        }
        else if(after==colors[3])
        {
            ground.tag="yellow";
            currentLightColor="yellow";
        }
        while(true)
        {   
            before=after; 
            randomcolor = Random.Range(0,4);
            
            float waitTime = Random.Range(5f,10f);
            yield return new WaitForSeconds(waitTime-3);
            NextColorAnimation animatorController = NextColorButton.GetComponent<NextColorAnimation>();
            animatorController.SlideIn();
            ActualColorName.text=nextcolor[randomcolor].ToUpper();
            if(nextcolor[randomcolor]=="red"){ActualColorName.color=Color.red;}
            else if(nextcolor[randomcolor]=="blue"){ActualColorName.color=Color.blue;} 
            else if(nextcolor[randomcolor]=="green"){ActualColorName.color=Color.green;} 
            else if(nextcolor[randomcolor]=="yellow"){ActualColorName.color=Color.yellow;}  
            NextColorIs.text="Next color is: ";
            yield return new WaitForSeconds(3);
            animatorController.SlideOut();
            ground.GetComponent<Renderer>().material.color=colors[randomcolor];
            after=ground.GetComponent<Renderer>().material.color;
            if(after==colors[0])
                {
                    ground.tag="red";
                    currentLightColor="red";
                }
                else if(after==colors[1])
                {
                    ground.tag="blue";
                    currentLightColor="blue";
                }
                else if(after==colors[2])
                {
                    ground.tag="green";
                    currentLightColor="green";
                }
                else if(after==colors[3])
                {
                    ground.tag="yellow";
                    currentLightColor="yellow";
                }
            //colorChanged=true;
            colorChanged=true;    
            
        }




        //Lights method

        
        /*before=LightBehaviour.l1.color;
        //Debug.Log(LightBehaviour.randomcolor);
        int randomcolor = Random.Range(0,4);
        LightBehaviour.l1.color=Color.Lerp(before,LightBehaviour.colors[randomcolor],1);
        after=LightBehaviour.l1.color;   
        if(after==LightBehaviour.colors[0])
        {
            ground.tag="red";
            currentLightColor="red";
        }
        else if(after==LightBehaviour.colors[1])
        {
            ground.tag="blue";
            currentLightColor="blue";
        }
        else if(after==LightBehaviour.colors[2])
        {
            ground.tag="green";
            currentLightColor="green";
        }
        else if(after==LightBehaviour.colors[3])
        {
            ground.tag="yellow";
            currentLightColor="yellow";
        }
        while(true)
        {   
            before=after; 
            randomcolor = Random.Range(0,4);
            Debug.Log("Next color is:"+nextcolor[randomcolor]);    
            float waitTime = Random.Range(5f,10f);
            yield return new WaitForSeconds(waitTime);            
            LightBehaviour.l1.color=Color.Lerp(before,LightBehaviour.colors[randomcolor],1);
            after=LightBehaviour.l1.color;   
            if(after==LightBehaviour.colors[0])
            {
                ground.tag="red";
                currentLightColor="red";
            }
            else if(after==LightBehaviour.colors[1])
            {
                ground.tag="blue";
                currentLightColor="blue";
            }
            else if(after==LightBehaviour.colors[2])
            {
                ground.tag="green";
                currentLightColor="green";
            }
            else if(after==LightBehaviour.colors[3])
            {
                ground.tag="yellow";
                currentLightColor="yellow";
            }
            //colorChanged=true;
            colorChanged=true;
            if(before!=after){
                timer=0;
                Time.timeScale=0.5f;
            }
            
            
            
        }*/
    }
    
    /*public void PauseVisible(){
        pauseButton.gameObject.SetActive(true);
    }
     
    /*public void PauseGame()
    {
        SceneManager.LoadScene("Pause Menu", LoadSceneMode.Additive);
        Debug.Log("Button clicked");
        Time.timeScale=0;
        pauseButton.gameObject.SetActive(false);
        

    }*/
    public void GameStart()
    {    
        StartCoroutine("SpawnObstacles");
        StartCoroutine("ChangeGroundColor");
    }

    

    public void SpeedUp(){
        if(score>=10){
            multiplier=1.5f;
                if(score>=25){
                    multiplier=2f;
                    if(score>=50){
                        multiplier=2.5f;
                        if(score>=75){
                            multiplier=3f;
                            if(score>=100){
                                multiplier=3.5f;
                        }
                     }
                }
        
             }

    }
    }

       public void SlowDownTime()
    {
        Time.timeScale = 0.5f;
        Invoke("ResetTimeScale", 0.5f);
    }

    void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
    public void ScoreUp()
    {
        if(Tile.isDead){
            Hitmark.Play();
            Tile.isDead=false;
            score++;
            UpdateHighScoreText();
            scoreText.text=score.ToString();
        }
                    
    }

    //works only without the global leaderboard
    public void CheckHighScore()
    {
        if(score>PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetFloat("HighScore",score);
            UpdateHighScoreText();
        }
    }

    public void UpdateHighScoreText()
    {
        highScoreText.text=$"{PlayerPrefs.GetInt("HighScore",0)}"; 
    }

    /*public void UpdateHighScoreTextPlayFab(){
        PlayfabManager instance2= gameObject.AddComponent<PlayfabManager>();
        instance2.GetHighScore(displayName);
        highScoreText.text=highscoreBELLO.ToString();

    }*/
    
}
