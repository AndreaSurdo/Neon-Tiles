using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    
    public static bool gameispaused=false;
    public static bool gameisover=false;
    public GameObject PauseUI;
    public GameObject GameOverUI;
    public GameObject PauseButton;
    public void Resume()
    {
       PauseUI.SetActive(false);
       PauseButton.SetActive(true);
       Time.timeScale=1;
       gameispaused=false;
        
    }

    public void Replay()
    {
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
                if(!PauseUI.activeSelf){
                SceneManager.LoadScene("NeonLoginPage");
                }
                else
                {
                    Resume();
                }
            }
        }

        if(gameisover)
        {
            GameOverUI.SetActive(true);
            PauseButton.SetActive(false);
            Time.timeScale=0;
        }
    }
}
