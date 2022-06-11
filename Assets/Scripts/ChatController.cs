using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class ChatController : MonoBehaviour
    {
        public TMP_Text Output;

        private ConcurrentQueue<string> _chatMessages;

        void OnEnable()
        {
            _chatMessages = GameManager.Instance.ChatMessages;
        }

        private void Update()
        {
            if (_chatMessages.TryDequeue(out var text))
            {
                Output.text += $"\n{text}";
            }
        }
    }
}
