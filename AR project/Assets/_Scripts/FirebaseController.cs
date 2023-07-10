using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject loginPanel,signupPanel,forgetPasswordPanel,notificationPanel,aRpanel;
    public InputField loginEmail,loginPassword,singupEmail,signupPassword,signupCPassword,forgetPassEmail;
    public Text notif_Text,notif_Message;
    public Toggle remeber;
    

    //Screen object variables
    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
        aRpanel.SetActive(false);
    }
    public void OpenSignUpPanel()
    {
        loginPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
        signupPanel.SetActive(true);
        aRpanel.SetActive(false);
    }
    public void OpenForgetPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        forgetPasswordPanel.SetActive(true);
        aRpanel.SetActive(false);
    }
    
    public void LoginUser()
    {
        if(string.IsNullOrEmpty(loginEmail.text)&&string.IsNullOrEmpty(loginPassword.text))
        {
            showNotifcationMessage("Error","Fields Empty");
        return;
        }
        else{
            aRpanel.SetActive(true);
            loginPanel.SetActive(false);
        }
    }
    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(singupEmail.text)&&string.IsNullOrEmpty(signupPassword.text)&&string.IsNullOrEmpty(signupCPassword.text))
        {
            showNotifcationMessage("Error","Fileds Empty");
            return;
        }
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

    
}


