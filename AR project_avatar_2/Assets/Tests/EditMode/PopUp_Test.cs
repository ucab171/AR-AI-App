using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IBM.Cloud.SDK.Utilities;
using IBM.Watson.Examples;
using IBM.Cloud.SDK;
using NUnit.Framework;

public class TestPopUp
{
    private GameObject canvasGameObject;
    private GameObject demoGameObject;

    [SetUp]
    public void TestSetup()
    {
        LogSystem.InstallDefaultReactors();

        canvasGameObject = new GameObject("Canvas");
        Canvas canvas = canvasGameObject.AddComponent<Canvas>();
        canvasGameObject.AddComponent<CanvasScaler>();
        canvasGameObject.AddComponent<GraphicRaycaster>();
    }

    [Test]
    public void UpdateTest()
    {
        demoGameObject = new GameObject("Demo");
        DemoVideo Demo = demoGameObject.AddComponent<DemoVideo>();

        GameObject yesButtonGameObject = new GameObject("YesButton");
        Button yesButton = yesButtonGameObject.AddComponent<Button>();
        yesButton.transform.SetParent(canvasGameObject.transform);
        Demo.YesButton = yesButton;

        GameObject noButtonGameObject = new GameObject("NoButton");
        Button noButton = noButtonGameObject.AddComponent<Button>();
        noButton.transform.SetParent(canvasGameObject.transform);
        Demo.NoButton = noButton;

        GameObject askGameObject = new GameObject("Ask");
        Text ask = askGameObject.AddComponent<Text>();
        ask.transform.SetParent(canvasGameObject.transform);
        Demo.Ask = ask;

        //No Keyword, hide the pop-up
        Demo.show_hide_PopUp(false);
        Assert.IsFalse(Demo.YesButton.IsActive());
        Assert.IsFalse(Demo.NoButton.IsActive());
        Assert.IsFalse(Demo.Ask.IsActive());

        //Show the pop-up
        Demo.show_hide_PopUp(true);
        Assert.IsTrue(Demo.YesButton.IsActive());
        Assert.IsTrue(Demo.NoButton.IsActive());
        Assert.IsTrue(Demo.Ask.IsActive());

        //Hide the pop-up
        Demo.HideButton();
        Assert.IsFalse(Demo.YesButton.IsActive());
        Assert.IsFalse(Demo.NoButton.IsActive());
        Assert.IsFalse(Demo.Ask.IsActive());
    }
}
