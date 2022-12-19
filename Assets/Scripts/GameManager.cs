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
    public Light[] lights;
    //public Color[] colors= new Color[] {Color.red,Color.blue,Color.green,Color.yellow};
    public int highscore;
    public float distance;
    public static bool colorChanged=false;
    public float timer = 0;
    public float waitTime;
    
    //public GameObject playButton;
    //public Button pauseButton;
    //public GameObject player;

    public static float multiplier=1;
    public static Color before;
    public static Color after;
    public static string currentLightColor;
    public void DistanceCalculator(){
        distance = Vector3.Distance(tile.transform.position, finishLine.transform.position);
        Debug.Log("Distance between objects: " + distance);
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.gameisover=false;
        UpdateHighScoreText();
        GameStart();
 
    }  


    // Update is called once per frame
    void Update()
    {  
       //DistanceCalculator();
       ScoreUp();
       SpeedUp();
       timer += Time.deltaTime;
                if (timer >= 1.5f)
                {
                    Time.timeScale = 1;
                    timer = 0;
                    Tile.colorhasChanged=false;
                }
    }


    IEnumerator SpawnObstacles()
    {
        
        while(true)
        {
            if(score<10){waitTime = Random.Range(0.5f,0.75f);}
            else if(score<25){waitTime = Random.Range(0.4f,0.65f);}
            else if(score<50){waitTime = Random.Range(0.3f,0.5f);}
            else{waitTime = Random.Range(0.25f,0.4f);}
            yield return new WaitForSeconds(waitTime);
            randTile = Random.Range(0,4);            
            Instantiate(tile, spawnPoints[randTile].position, Quaternion.identity);
        }
    }

    IEnumerator ChangeGroundColor()
    {
        before=LightBehaviour.l1.color;
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
            float waitTime = Random.Range(5f,10f);
            yield return new WaitForSeconds(waitTime);  
            randomcolor = Random.Range(0,4);
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
            Tile.colorhasChanged=true;
            if(before!=after){
            Time.timeScale=0.5f;
            }
            
        }
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
            Tile.isDead=false;
            score++;
            CheckHighScore();
            scoreText.text=score.ToString();
        }
                    
    }

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

    
}
