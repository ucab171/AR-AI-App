using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;

#if UNITY_EDITOR
public class NotificationTests
{
    [Test]
    public void ShowNotificationMessage_UpdatesTextAndOpensPanel()
    {
        // ARRANGE
        var script = new NewBehaviourScript();
        var notifPanel = new GameObject("NotificationPanel").AddComponent<CanvasGroup>();
        var notif_Text = new GameObject("Notif_Text").AddComponent<Text>();
        var notif_Message = new GameObject("Notif_Message").AddComponent<Text>();
        script.notificationPanel = notifPanel.gameObject;
        script.notif_Text = notif_Text;
        script.notif_Message = notif_Message;

        // ACTION
        script.showNotifcationMessage("TestTitle", "TestMessage");

        // ASSERT
        Assert.AreEqual("TestTitle", script.notif_Text.text);
        Assert.AreEqual("TestMessage", script.notif_Message.text);
        Assert.IsTrue(script.notificationPanel.activeSelf);
    }

    [Test]
    public void CloseNotif_Panel_ClosesPanelAndClearsText()
    {
        // ARRANGE
        var script = new NewBehaviourScript();
        var notifPanel = new GameObject("NotificationPanel").AddComponent<CanvasGroup>();
        var notif_Text = new GameObject("Notif_Text").AddComponent<Text>();
        var notif_Message = new GameObject("Notif_Message").AddComponent<Text>();
        script.notificationPanel = notifPanel.gameObject;
        script.notif_Text = notif_Text;
        script.notif_Message = notif_Message;
        script.notificationPanel.SetActive(true);
        script.notif_Text.text = "SampleTitle";
        script.notif_Message.text = "SampleMessage";

        // ACTION
        script.CloseNotif_Panel();

        // ASSERT
        Assert.AreEqual("", script.notif_Text.text);
        Assert.AreEqual("", script.notif_Message.text);
        Assert.IsFalse(script.notificationPanel.activeSelf);
    }
}
#endif
