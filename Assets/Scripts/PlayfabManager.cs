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
    public GameObject RegisterLink;
    public GameObject RegisterTrue;
    public GameObject ResetLink;
    public GameObject ResetTrue;
    public GameObject rowPrefab;
    public Transform rowsParent;
    public GameObject LeaderboardMenu;



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

    public void GetLeaderboard(){
        LeaderboardMenu.SetActive(true);
        var request= new GetLeaderboardRequest{StatisticName="Neon-Tiles-Leaderboard", StartPosition=0, MaxResultsCount=10};
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet,OnError);
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
            texts[0].text= item.Position.ToString();
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
        Login.SetActive(false);
        RegisterLink.SetActive(false);
        ResetLink.SetActive(false);
        RegisterTrue.SetActive(true);
        usernameVisible.SetActive(true);
    }

    public void ResetLinkMethod()
    {
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
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
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








    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Login.SetActive(true);
            RegisterLink.SetActive(true);
            ResetLink.SetActive(true);
            RegisterTrue.SetActive(false);
            ResetTrue.SetActive(false);
            passwordvisible.SetActive(true);
            usernameVisible.SetActive(false);
            LeaderboardMenu.SetActive(false);
        }
            
    }
}
