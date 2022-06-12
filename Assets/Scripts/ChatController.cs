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

        private int _state;
        private RectTransform _rectTransform;
        private List<string> _messages = new();
        private ConcurrentQueue<Message> _messageQueue;

        private void Awake()
        {
            _messageQueue = GameManager.Instance.ChatMessages;
            _rectTransform = transform.GetComponent<RectTransform>();
        }

        private void Start()
        {
            ChangeState(0);
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.tKey.wasPressedThisFrame)
                {
                    ChangeState(++_state);
                }
            }

            if (_messageQueue.TryDequeue(out var message))
            {
                if (_messages.Count > 200)
                {
                    _messages.Clear(); // FIXME удалять самые старые
                }
                _messages.Add(message.ToString());
                Render();
            }
        }

        private void Render()
        {
            Output.text = $"<b>{_messages.Count}</b>\n";
            if (_state < 2) {
                for (var i = _messages.Count - 1; i >= 0; i--)
                {
                    if (i > 20)
                    {
                        Output.text += "...";
                        break;
                    }
                    Output.text += $"{_messages[i]}\n";
                }
            }
        }

        public void ChangeState(int state)
        {
            switch (state)
            {
                case 0:
                    _rectTransform.sizeDelta = new Vector2(Screen.width / 4, Screen.height);
                    break;
                case 1:
                    _rectTransform.sizeDelta = new Vector2(Screen.width / 3, Screen.height);
                    break;
                case 2:
                    _rectTransform.sizeDelta = new Vector2(50, 30);
                    break;
                default:
                    ChangeState(0);
                    return;
            }
            _state = state;
            Render();
        }
    }
}
