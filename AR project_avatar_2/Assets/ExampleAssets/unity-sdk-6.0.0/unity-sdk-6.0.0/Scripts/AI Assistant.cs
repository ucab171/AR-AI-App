using System.Collections;
using System.Collections.Generic;
using IBM.Cloud.SDK.Utilities;
using UnityEngine;

using IBM.Watson.Examples;

public class AIAssistant : MonoBehaviour
{
    [SerializeField]
    private ExampleTextToSpeechV1 TTS; // IBM Watson Text to Speech gameobject
    [SerializeField]
    private ExampleStreaming STT; // IBM Watson Speech to Text gameobject
    [SerializeField]
    private ExampleAssistantV2 Assistant; // IBM Watson Assistant


    private void Start()
    {
        Runnable.Run(Assistant.Welcome());
    }
}
