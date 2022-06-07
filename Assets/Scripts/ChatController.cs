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
        public TMP_InputField inputField;
        public TMP_Text displayOutput;

        private ConcurrentQueue<string> _chatMessages;

        void OnEnable()
        {
            inputField.onSubmit.AddListener(Send);
            _chatMessages = GameManager.Instance.ChatMessages;
        }

        void OnDisable()
        {
            inputField.onSubmit.RemoveListener(Send);
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    if (!GameManager.WindowActive && !GameManager.PopupActive)
                    {
                        inputField.Select();
                    }
                }
            }

            if (_chatMessages.TryDequeue(out var text))
            {
                displayOutput.text += $"\n{text}";
            }
        }

        public void Send(string text)
        {
            NetworkManager.Instance.Chat(GameManager.Instance.me.id, GameManager.Instance.me.room.id, text);
        }
    }
}
