using NUnit.Framework;
using Firebase.Auth; 

#if UNITY_EDITOR
public class GeterrorTests
{
    [Test]
    public void GetErrorMessage_InvalidEmail_ReturnsCorrectMessage()
    {
        var message = NewBehaviourScript.GetErrorMessage(AuthError.InvalidEmail);
        Assert.AreEqual("Your Email Invalid", message);
    }

    [Test]
    public void GetErrorMessage_WrongPassword_ReturnsCorrectMessage()
    {
        var message = NewBehaviourScript.GetErrorMessage(AuthError.WrongPassword);
        Assert.AreEqual("Wrong Password", message);
    }

    [Test]
    public void GetErrorMessage_MissingPassword_ReturnsCorrectMessage()
    {
        var message = NewBehaviourScript.GetErrorMessage(AuthError.MissingPassword);
        Assert.AreEqual("Missing Password", message);
    }

    [Test]
    public void GetErrorMessage_WeakPassword_ReturnsCorrectMessage()
    {
        var message = NewBehaviourScript.GetErrorMessage(AuthError.WeakPassword);
        Assert.AreEqual("Password So Weak", message);
    }
    [Test]
    public void GetErrorMessage_EmailAlreadyInUse_ReturnsCorrectMessage()
    {
        var message = NewBehaviourScript.GetErrorMessage(AuthError.EmailAlreadyInUse);
        Assert.AreEqual("Your Email Already in Use", message);
    }
    [Test]
    public void GetErrorMessage_MissingEmail_ReturnsCorrectMessage()
    {
        var message = NewBehaviourScript.GetErrorMessage(AuthError.MissingEmail);
        Assert.AreEqual("Your Email Missing", message);
    }


   
}
#endif
