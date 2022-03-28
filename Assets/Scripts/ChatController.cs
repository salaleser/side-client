﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace side
{
    public class ChatController : MonoBehaviour
    {
        public TMP_InputField inputField;

        public TMP_Text displayOutput;

        void OnEnable()
        {
            inputField.onSubmit.AddListener(Send);
        }

        void OnDisable()
        {
            inputField.onSubmit.RemoveListener(Send);
        }

        public void ReplaceChat(List<ChatItem> messages)
        {
            displayOutput.text = string.Empty;
            foreach(var m in messages)
            {
                displayOutput.text += $"{m}\n";
            }
        }

        public void Send(string text)
        {
            NetworkManager.Instance.Chat(GameManager.Instance.currentCitizen.id, GameManager.Instance.currentAddress.locations[0].id, text);

            inputField.text = string.Empty;
        }
    }
}
