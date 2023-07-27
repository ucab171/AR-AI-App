using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoVideo : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text Response;
    [SerializeField]
    private UnityEngine.UI.InputField Input;
    [SerializeField]
    private UnityEngine.UI.Button YesButton;
    [SerializeField]
    private UnityEngine.UI.Button NoButton;
    [SerializeField]
    private UnityEngine.UI.Text Ask;
    // Start is called before the first frame update
    void Start()
    {
        YesButton.gameObject.SetActive(false);
        NoButton.gameObject.SetActive(false);
        Ask.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Response.text.Contains("demo video"))
        {
            YesButton.gameObject.SetActive(true);
            NoButton.gameObject.SetActive(true);
            Ask.gameObject.SetActive(true);
        }
        else
        {
            YesButton.gameObject.SetActive(false);
            NoButton.gameObject.SetActive(false);
            Ask.gameObject.SetActive(false);
        }
    }

    public void HideButton()
    {
        NoButton.gameObject.SetActive(false);
        YesButton.gameObject.SetActive(false);
        Ask.gameObject.SetActive(false);
    }
}
