using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Watson.TextToSpeech.V1;
using IBM.Watson.TextToSpeech.V1.Model;

public class TTS_Test
{
    private TextToSpeechService service;
    private string iamApikey = "ER52Q1vucnFaDhaZTBYrfK-6M5JnBfQzNfxQJa8ofwW1";
    private string serviceUrl = "https://api.eu-gb.text-to-speech.watson.cloud.ibm.com/instances/8d931edb-9473-4237-92a8-7988be706936";
    private string allisionVoice = "en-US_AllisonVoice";
    private string synthesizeText = "Welcome to IBM innovation studio.";
    private string synthesizeMimeType = "audio/wav";

    [SetUp]
    public void TestSetup()
    {
        LogSystem.InstallDefaultReactors();
    }

    [UnityTest, Order(0)]
    public IEnumerator TestTTS()
    {
        IamAuthenticator authenticator = new IamAuthenticator(apikey: iamApikey);
        service = new TextToSpeechService(authenticator);
        service.SetServiceUrl(serviceUrl);

        while (!service.Authenticator.CanAuthenticate())
        {
            yield return null;
        }

        Log.Debug("TextToSpeechServiceTests", "Attempting to Synthesize...");
        byte[] synthesizeResponse = null;
        AudioClip clip = null;
        service.Synthesize(
            callback: (DetailedResponse<byte[]> response, IBMError error) =>
            {
                synthesizeResponse = response.Result;
                Assert.IsNotNull(synthesizeResponse);
                Assert.IsNull(error);
                clip = WaveFile.ParseWAV("myClip", synthesizeResponse);
                Assert.IsNotNull(clip);
                //PlayClip(clip);

            },
            text: synthesizeText,
            voice: allisionVoice,
            accept: synthesizeMimeType
        );

        while (synthesizeResponse == null)
            yield return null;

        //yield return new WaitForSeconds(clip.length);
    }
}
