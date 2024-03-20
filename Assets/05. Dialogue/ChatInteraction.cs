using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class ChatInteraction : MonoBehaviour
{
    public InputField inputField; // Assign the Input Field UI element in the Inspector
    public Text chatText; // Assign the Text UI element for displaying chat messages in the Inspector
    private readonly string serverUrl = "https://juhanlim.github.io/gememi-front/"; // Replace with your actual server URL

    // Call this method when the Send Button is pressed
    public void SendInputToServer()
    {
        string userInput = inputField.text;
        if (!string.IsNullOrEmpty(userInput))
        {
            StartCoroutine(PostRequest(userInput));
            inputField.text = ""; // Optionally clear the input field
        }
    }

    IEnumerator PostRequest(string userInput)
    {
        // Prepare the JSON data
        string jsonPayload = JsonUtility.ToJson(new { userInput = userInput });
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonPayload);

        using (UnityWebRequest www = new UnityWebRequest(serverUrl, "POST"))
        {
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for the response
            yield return www.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            if (www.result != UnityWebRequest.Result.Success)
#else
            if (www.isNetworkError || www.isHttpError) // For Unity versions before 2020.1
#endif
            {
                Debug.LogError($"Error: {www.error}");
            }
            else
            {
                // Process the server's response
                ChatResponse response = JsonUtility.FromJson<ChatResponse>(www.downloadHandler.text);
                chatText.text += "\nNPC: " + response.text; // Append the NPC's response to the chat text
            }
        }
    }

    // Define a class to match the response structure expected from the server
    [System.Serializable]
    private class ChatResponse
    {
        public string text;
    }
}
