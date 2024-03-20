using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private InputField inputField;         // 입력창
        [SerializeField] private Button button;                 // Send 버튼
        [SerializeField] private ScrollRect scroll;             // 스크롤
        
        [SerializeField] private RectTransform sent;            // 사용자 메시지 
        [SerializeField] private RectTransform received;        // 받은 메시지 

        private float height;                                   // 스크롤 영역 높이 계산 변수
        private OpenAIApi openai = new OpenAIApi();             // OpenAI 객체 생성 

        private List<ChatMessage> messages = new List<ChatMessage>();    // 대화내역 저장 리스트 
        private string prompt = "You are a Python instructor.Problem 1. Complete the function of returning a string of alphabetic lowercase letters in which more than once the alphabet is divided into two or more parts and the string given by defining the alphabet as the lone alphabet is returned.Please answer in detail what the student asks about.";

        private void Start()  
        {
            button.onClick.AddListener(SendReply);    // 버튼 클릭하면 SendReply 함수 호출 
        }

        private void AppendMessage(ChatMessage message) 
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);    // 스크롤 조절 

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);  
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        private async void SendReply()                  // 버튼 클릭시 
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text 
            };
            
            AppendMessage(newMessage); 

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text; 
            
            messages.Add(newMessage);
            
            button.enabled = false;       // 메시지 보낼 시 입력 잠시 비활성화 
            inputField.text = "";
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                messages.Add(message);
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}
