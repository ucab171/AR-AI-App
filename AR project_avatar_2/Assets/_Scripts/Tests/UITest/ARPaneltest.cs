using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

#if UNITY_EDITOR

public class ARPaneltest 
{
    private NewBehaviourScript _script;

    [SetUp]
    public void Setup() 
    {
        GameObject go = new GameObject();
        _script = go.AddComponent<NewBehaviourScript>();
        _script.loginPanel = new GameObject();
        _script.homePanel = new GameObject();
        _script.signupPanel = new GameObject();
        _script.forgetPasswordPanel = new GameObject();
        _script.demoPanel = new GameObject();
        _script.aRpanel = new GameObject();
    }

    [TearDown]
    public void Teardown() 
    {
        Object.Destroy(_script.loginPanel);
        Object.Destroy(_script.homePanel);
        Object.Destroy(_script.signupPanel);
        Object.Destroy(_script.forgetPasswordPanel);
        Object.Destroy(_script.demoPanel);
        Object.Destroy(_script.aRpanel);
        Object.Destroy(_script.gameObject);
    }

    [UnityTest]
    public IEnumerator TestOpenARPanel() 
    {
        _script.OpenAR();
        
        Assert.IsFalse(_script.loginPanel.activeSelf);
        Assert.IsFalse(_script.homePanel.activeSelf);
        Assert.IsFalse(_script.signupPanel.activeSelf);
        Assert.IsFalse(_script.forgetPasswordPanel.activeSelf);
        Assert.IsFalse(_script.demoPanel.activeSelf);
        Assert.IsTrue(_script.aRpanel.activeSelf);
        
        yield return null;
    }
}

#endif
