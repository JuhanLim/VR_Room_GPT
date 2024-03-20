using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChatUI : MonoBehaviour
{
    public InputField playerInputField;
    public Text chatText;
    public Button sendButton;

    private List<string> chatHistory = new List<string>();

    void Start()
    {
        sendButton.onClick.AddListener(SendMessageToChat);
    }

    void SendMessageToChat()
    {
        string message = playerInputField.text;
        if (!string.IsNullOrEmpty(message))
        {
            // Assuming the player sends the message
            chatHistory.Add("Player: " + message);
            // Here you would typically send the message to your NPC/chatbot and get a response
            // For demonstration, we'll just echo the message back as if from the NPC
            chatHistory.Add("NPC: " + message); // Replace this with the actual NPC response

            UpdateChatText();
            playerInputField.text = ""; // Clear the input field
            playerInputField.ActivateInputField(); // Refocus on the input field
        }
    }

    void UpdateChatText()
    {
        chatText.text = string.Join("\n", chatHistory.ToArray());
    }
}
