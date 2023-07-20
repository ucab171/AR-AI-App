using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Firebase.Extensions;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject loginPanel,signupPanel,forgetPasswordPanel,notificationPanel,aRpanel,successtext,homePanel;
    public InputField loginEmail,loginPassword,singupEmail,signupPassword,signupCPassword,forgetPassEmail;
    public Text notif_Text,notif_Message;
    public Toggle remeber;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    bool isSignIn = false;
    void Start(){
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available) {
            // Create and hold a reference to your FirebaseApp,
            // where app is a Firebase.FirebaseApp property of your application class.
            // app = Firebase.FirebaseApp.DefaultInstance;
            InitializeFirebase();

            // Set a flag here to indicate whether Firebase is ready to use by your app.
        } else {
            UnityEngine.Debug.LogError(System.String.Format(
            "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            // Firebase Unity SDK is not safe to use here.
        }
});
    }

    //Screen object variables
    public void OpenLoginPanel()
    {

        loginPanel.SetActive(true);
        homePanel.SetActive(false);
        signupPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
        aRpanel.SetActive(false);
        successtext.SetActive(false);
    }
    public void OpenSignUpPanel()
    {
        loginPanel.SetActive(false);
        homePanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
        signupPanel.SetActive(true);
        aRpanel.SetActive(false);
    }
    public void OpenForgetPanel()
    {
        loginPanel.SetActive(false);
        homePanel.SetActive(false);
        signupPanel.SetActive(false);
        forgetPasswordPanel.SetActive(true);
        aRpanel.SetActive(false);
    }
    public void OpenAR()
    {
        loginPanel.SetActive(false);
        homePanel.SetActive(false);
        signupPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
        aRpanel.SetActive(true);
    }
    public void OpenHome()
    {
        loginPanel.SetActive(false);
        homePanel.SetActive(true);
        signupPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
        aRpanel.SetActive(false);
    }
    public void RegisSuc()
    {
        successtext.SetActive(true);
    }
    
    public void LoginUser()
    {
        if(string.IsNullOrEmpty(loginEmail.text)&&string.IsNullOrEmpty(loginPassword.text))
        {
            showNotifcationMessage("Error","Fields Empty");
        return;
        }
        
        SignInUser(loginEmail.text,loginPassword.text);
        
    }
    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(singupEmail.text)&&string.IsNullOrEmpty(signupPassword.text)&&string.IsNullOrEmpty(signupCPassword.text))
        {
            showNotifcationMessage("Error","Fileds Empty");
            return;
        }
        CreateUser(singupEmail.text,signupPassword.text);
        RegisSuc();


    }
    public void LogOut(){
        auth.SignOut();
        OpenHome();
    }
    public void forgetPass()
    {
        if (string.IsNullOrEmpty(forgetPassEmail.text))
        {
            showNotifcationMessage("Error","Forget Email Empty");

            return;
        }
    }
    private void showNotifcationMessage(string title,string message)
    {
        notif_Text.text=""+title;
        notif_Message.text=""+message;
        notificationPanel.SetActive(true);
    }
    public void CloseNotif_Panel(){
        notif_Text.text="";
        notif_Message.text="";
        notificationPanel.SetActive(false);

    }
    void CreateUser(string email, string password){
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            });
    }
    public void SignInUser(string email,string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            OpenAR();
            });
    }
    void InitializeFirebase() {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs) {
        if (auth.CurrentUser != user) {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
                && auth.CurrentUser.IsValid();
            if (!signedIn && user != null) {
            Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn) {
            Debug.Log("Signed in " + user.UserId);
            isSignIn=true;
            
            }
        }
    }

    void OnDestroy() {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    bool isSigned=false;
    void Update()
    {
        if(isSignIn)
        {
            if(!isSigned)
            {
                isSigned=true;
                OpenAR();
            }
        }
    }

    
}




