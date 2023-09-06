using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Watson.SpeechToText.V1;
using IBM.Watson.SpeechToText.V1.Model;

public class STT_Test
{
    private SpeechToTextService service;
    private string iamApikey = "K8onNueGPnN2stOCH2RJ2x21Fv7mk3127w7aPEsZlyYL";
    private string serviceUrl = "https://api.eu-gb.speech-to-text.watson.cloud.ibm.com/instances/8b5f6293-fe19-4a30-9faf-97fa3fa2b9ee";
    private string testAudioPath;
    private string usBroadbandModel = "en-US_BroadbandModel";

    [SetUp]
    public void TestSetup()
    {
        LogSystem.InstallDefaultReactors();
        testAudioPath = Application.dataPath + "/ExampleAssets/unity-sdk-6.0.0/unity-sdk-6.0.0/Tests/TestData/SpeechToTextV1/test-audio.wav";
    }

    [UnityTest, Order(0)]
    public IEnumerator TestSTT()
    {
        IamAuthenticator authenticator = new IamAuthenticator(apikey: iamApikey);
        service = new SpeechToTextService(authenticator);
        service.SetServiceUrl(serviceUrl);

        while (!service.Authenticator.CanAuthenticate())
        {
            yield return null;
        }

        SpeechRecognitionResults recognizeResponse = null;
        MemoryStream audio = new MemoryStream(File.ReadAllBytes(testAudioPath));

        service.Recognize(
            callback: (DetailedResponse<SpeechRecognitionResults> response, IBMError error) =>
            {
                Log.Debug("SpeechToTextServiceTests", "Recognize result: {0}", response.Response);
                recognizeResponse = response.Result;
                Assert.IsNotNull(recognizeResponse);
                Assert.IsNotNull(recognizeResponse.Results);
                Assert.IsNull(error);
            },
            audio: audio,
            model: usBroadbandModel,
            contentType: "audio/wav",
            speechDetectorSensitivity: 1,
            backgroundAudioSuppression: 0
        );

        while (recognizeResponse == null)
            yield return null;
    }
}
