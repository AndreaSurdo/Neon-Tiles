using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public TMP_InputField emailinput;
    public TMP_InputField passwordinput;
    public TMP_InputField usernameinput;
    public GameObject usernameVisible;
    public GameObject passwordvisible;
    public GameObject Login;
    public GameObject HowToPlay;
    public GameObject RegisterLink;
    public GameObject RegisterTrue;
    public GameObject ResetLink;
    public GameObject ResetTrue;
    public GameObject rowPrefab;
    public Transform rowsParent;
    public GameObject LeaderboardMenu;
    public GameObject Pausemenu;
    public GameObject Back;
    public GameObject GameoverMenu;
    public AudioSource ButtonClick;
    public AudioSource ButtonClick2;


    public void LoginBackButton()
    {
            Back.SetActive(false);
            Login.SetActive(true);
            RegisterLink.SetActive(true);
            ResetLink.SetActive(true);
            RegisterTrue.SetActive(false);
            ResetTrue.SetActive(false);
            passwordvisible.SetActive(true);
            usernameVisible.SetActive(false);
            LeaderboardMenu.SetActive(false);
            Pausemenu.SetActive(true);
    }



    public void SendLeaderboard(int score){
        var request=new UpdatePlayerStatisticsRequest{ 
            Statistics=new List<StatisticUpdate>{
                new StatisticUpdate{
                StatisticName="Neon-Tiles-Leaderboard",
                Value=score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }   

    public void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result){
        Debug.Log("Leaderboard Updated");
    }

    //Leaderboard request for 5 elements
    public void GetLeaderboard(){
        ButtonClick.Play();
        LeaderboardMenu.SetActive(true);
        Pausemenu.SetActive(false);
        var request= new GetLeaderboardRequest{StatisticName="Neon-Tiles-Leaderboard", StartPosition=0, MaxResultsCount=5};
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet,OnError);
    }

    //Get display name to be used by gamemanager
    public void GetDisplayName()
{
    var request = new GetPlayerProfileRequest();
    PlayFabClientAPI.GetPlayerProfile(request, OnGetPlayerProfileSuccess, OnError);
}

public void OnGetPlayerProfileSuccess(GetPlayerProfileResult result)
{
    if(result==null){GameManager.displayName="NULL";}
    GameManager.displayName= result.PlayerProfile.DisplayName;
}

    //Get highscore from playfab
    public void GetHighScore(string userId){
     // Set up the request object
        GetLeaderboardAroundPlayerRequest request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Neon-Tiles-Leaderboard",
            MaxResultsCount = 1,
            PlayFabId = userId
        };

        // Send the request to PlayFab
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, (result) =>
        {
            // Check if the player is in the leaderboard
            if (result.Leaderboard.Count > 0)
            {
                // Get the player's score
                GameManager.highscore = result.Leaderboard[0].StatValue;
            }
        }, OnError);
    }
    

    public void ClearTable(){
        var clones=GameObject.FindGameObjectsWithTag ("clone");
         foreach (var clone in clones)
        {
            Destroy(clone);
        }

    }

    public void OnLeaderboardGet(GetLeaderboardResult result){
        ClearTable();
        foreach(var item in result.Leaderboard){
            GameObject newGo=Instantiate(rowPrefab, rowsParent);
            newGo.tag="clone";
            TextMeshProUGUI[] texts;
            texts= newGo.GetComponentsInChildren<TextMeshProUGUI>();
            int place=(item.Position)+1;
            texts[0].text= (place.ToString());
            if(item.DisplayName==null){texts[1].text="NULL";}
            else{texts[1].text= item.DisplayName;}
            texts[2].text= item.StatValue.ToString();
            Debug.Log(item.Position+""+item.PlayFabId+""+item.StatValue);
        }
    }



    public void RegisterButton()
    {
        if (passwordinput.text.Length<6){
            messageText.text="Password needs to be 6 characters long";
            return;
        }
        var request= new RegisterPlayFabUserRequest
        {
            Email=emailinput.text,
            Password= passwordinput.text,
            Username= usernameinput.text,
            RequireBothUsernameAndEmail=true        
        };
        PlayFabClientAPI.RegisterPlayFabUser(request,OnRregisterSuccess, OnError);
    }

    void OnUpdateSuccess(UpdateUserTitleDisplayNameResult result){
        messageText.text="Username Registered Correctly";
    }

    public void RegisterLinkMethod()
    {
        Back.SetActive(true);
        Login.SetActive(false);
        RegisterLink.SetActive(false);
        ResetLink.SetActive(false);
        RegisterTrue.SetActive(true);
        usernameVisible.SetActive(true);
    }

    public void ResetLinkMethod()
    {
        Back.SetActive(true);
        Login.SetActive(false);
        RegisterLink.SetActive(false);
        ResetLink.SetActive(false);
        passwordvisible.SetActive(false);
        ResetTrue.SetActive(true);
    }

    private void OnError(PlayFabError error)
    {
        messageText.text=error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    void OnRregisterSuccess (RegisterPlayFabUserResult result)
    {
        
        messageText.text="Registered and Logged in";
        UpdateUserTitleDisplayNameRequest request2 = new UpdateUserTitleDisplayNameRequest();
        request2.DisplayName = usernameinput.text;
        PlayFabClientAPI.UpdateUserTitleDisplayName(request2,OnUpdateSuccess, OnError);
        SceneManager.LoadScene("Game");
    }

    
    public void LoginButton()
    {
        var request=new LoginWithEmailAddressRequest
        {
            Email=emailinput.text,
            Password=passwordinput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams { GetPlayerProfile = true }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            GameManager.displayName = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        messageText.text="Logged in";
        SceneManager.LoadScene("Game");
    }

    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email=emailinput.text,
            TitleId="80BAA"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request,OnPassWordReset, OnError);

    }

    void OnPassWordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text="Password reset email sent";
    }

    public void BackButton(){
        ButtonClick2.Play();
        LeaderboardMenu.SetActive(false);
        if(PauseMenu.gameispaused==true){Pausemenu.SetActive(true); HowToPlay.SetActive(true);}
    }








    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name=="NeonLoginPage")
        {
            if(Input.GetKeyDown(KeyCode.Escape)){        
            Login.SetActive(true);
            RegisterLink.SetActive(true);
            ResetLink.SetActive(true);
            RegisterTrue.SetActive(false);
            ResetTrue.SetActive(false);
            passwordvisible.SetActive(true);
            usernameVisible.SetActive(false);
            LeaderboardMenu.SetActive(false);
            Pausemenu.SetActive(true);
        }
            
    }
        }
}
