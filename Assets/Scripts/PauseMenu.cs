using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class PauseMenu : MonoBehaviour
{
    
    public static bool gameispaused=false;
    public static bool gameisover=false;
    public GameObject PauseUI;
    public GameObject GameOverUI;
    public GameObject PauseButton;
    public GameObject LeaderboardMenu;
    public AudioSource PauseSound;
    public AudioSource ButtonClick;

    
    public void Resume()
    {
        ButtonClick.Play();
        PauseUI.SetActive(false);
        PauseButton.SetActive(true);
        LeaderboardMenu.SetActive(false);
        Time.timeScale=1;
        gameispaused=false;
        
    }

    public void Replay()
    {
        ButtonClick.Play();
        GameOverUI.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale=1;
        GameManager.score=0;
        SceneManager.LoadScene("Game");
        gameisover=false;
        GameManager.multiplier=1;
    }

    public void Pause()
    {
        PauseSound.Play();
        PauseUI.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale=0;
        gameispaused=true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("NeonLoginPage");

    }

    public void Quit()
    {
        ButtonClick.Play();
        Application.Quit();
        Debug.Log("Quit");
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().buildIndex==0)
            {
                Application.Quit();
            }
            else
            {
                if(LeaderboardMenu.activeSelf){
                    LeaderboardMenu.SetActive(false);
                }
                else if(PauseUI.activeSelf){
                    Resume();
                }
                else if(!PauseUI.activeSelf){
                    Replay();
                    SceneManager.LoadScene("NeonLoginPage");
                }
                
            }
        }

        if(gameisover)
        {
            GameOverUI.SetActive(true);
            PauseButton.SetActive(false);
            Time.timeScale=0;
            int scoreint=Convert.ToInt32(GameManager.score);
            PlayfabManager instance= gameObject.AddComponent<PlayfabManager>();
            instance.SendLeaderboard(scoreint);
                        
        }
        
    }
    
}
