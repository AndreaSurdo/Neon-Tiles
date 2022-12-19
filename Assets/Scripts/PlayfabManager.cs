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
    public GameObject passwordvisible;
    public GameObject Login;
    public GameObject RegisterLink;
    public GameObject RegisterTrue;
    public GameObject ResetLink;
    public GameObject ResetTrue;
    

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
            RequireBothUsernameAndEmail=false        
        };
        PlayFabClientAPI.RegisterPlayFabUser(request,OnRregisterSuccess, OnError);
    }

    public void RegisterLinkMethod()
    {
        Login.SetActive(false);
        RegisterLink.SetActive(false);
        ResetLink.SetActive(false);
        RegisterTrue.SetActive(true);
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
        messageText.text="";
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
        }
            
    }
}
