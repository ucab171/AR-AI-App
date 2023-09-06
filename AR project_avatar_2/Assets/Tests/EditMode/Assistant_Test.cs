using System.Collections;
using System.Collections.Generic;
using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Watson.Assistant.V2;
using IBM.Watson.Assistant.V2.Model;

public class Assistant_Test
{
    private AssistantService service;
    private string versionDate = "2023-07-19";
    private string iamApikey = "v-qMASiboptmWMvhemWhF6-WuyFvEYUViBH486HigR0h";
    private string serviceUrl = "https://api.us-south.assistant.watson.cloud.ibm.com/v2/assistants/939d7cb2-0a9e-4858-a3d5-57ecd4146554/sessions";
    private string assistantId = "939d7cb2-0a9e-4858-a3d5-57ecd4146554";
    //private string sessionId;
    string sessionId = null;

    [SetUp]
    public void TestSetup()
    {
        LogSystem.InstallDefaultReactors();
    }

    [UnityTest, Order(0)]
    public IEnumerator TestSession()
    {
        IamAuthenticator authenticator = new IamAuthenticator(apikey: iamApikey);
        service = new AssistantService(versionDate, authenticator);
        service.SetServiceUrl(serviceUrl);

        while (!service.Authenticator.CanAuthenticate())
        {
            yield return null;
        }

        //assistantId = Environment.GetEnvironmentVariable("ASSISTANT_ASSISTANT_ID");

        SessionResponse createSessionResponse = null;
        Log.Debug("AssistantTests", "Attempting to CreateSession...");
        service.WithHeader("X-Watson-Test", "1");
        service.CreateSession(
            callback: (DetailedResponse<SessionResponse> response, IBMError error) =>
            {
                Log.Debug("AssistantTests", "result: {0}", response.Response);
                createSessionResponse = response.Result;
                sessionId = createSessionResponse.SessionId;
                Assert.IsNotNull(createSessionResponse);
                Assert.IsNotNull(response.Result.SessionId);
                Assert.IsNull(error);
            },
            assistantId: assistantId
        );


        while (createSessionResponse == null)
        {
            yield return null;
        }

        MessageResponse messageResponse = null;
        Log.Debug("AssistantTests", "Attempting to Message...");
        service.WithHeader("X-Watson-Test", "1");
        service.Message(
            callback: (DetailedResponse<MessageResponse> response, IBMError error) =>
            {
                Log.Debug("AssistantTests", "result: {0}", response.Response);
                messageResponse = response.Result;
                Assert.IsNotNull(messageResponse);
                Assert.IsNull(error);
            },
            assistantId: assistantId,
            sessionId: sessionId
        );

        while (messageResponse == null)
        {
            yield return null;
        }

        messageResponse = null;
        var input = new MessageInput()
        {
            Text = "Hello",
            Options = new MessageInputOptions()
            {
                ReturnContext = true
            }
        };
        Log.Debug("AssistantTests", "Attempting to Message...Hello");
        service.WithHeader("X-Watson-Test", "1");
        service.Message(
            callback: (DetailedResponse<MessageResponse> response, IBMError error) =>
            {
                Log.Debug("AssistantTests", "result: {0}", response.Response);
                messageResponse = response.Result;
                Assert.IsNotNull(messageResponse);
                Assert.IsNull(error);
            },
            assistantId: assistantId,
            sessionId: sessionId,
            input: input
        );

        while (messageResponse == null)
        {
            yield return null;
        }

        object deleteSessionResponse = null;
        Log.Debug("AssistantTests", "Attempting to DeleteSession...");
        service.WithHeader("X-Watson-Test", "1");
        service.DeleteSession(
            callback: (DetailedResponse<object> response, IBMError error) =>
            {
                Log.Debug("AssistantTests", "result: {0}", response.Response);
                deleteSessionResponse = response.Result;
                Assert.IsNotNull(response.Result);
                Assert.IsNull(error);
            },
            assistantId: assistantId,
            sessionId: sessionId
        );

        while (deleteSessionResponse == null)
        {
            yield return null;
        }
    }
}
