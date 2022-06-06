using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class InputFieldPopup : MonoBehaviour
    {
        public TMP_Text Description;
        public TMP_InputField InputField;
        public UnityAction<string> Action;

        public void Accept()
        {
            Action(InputField.text);
            this.GetComponent<PopupManager>().ClosePopup();
        }

        public void Decline()
        {
            this.GetComponent<PopupManager>().ClosePopup();
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    Accept();
                }
                else if (Keyboard.current.escapeKey.wasPressedThisFrame)
                {
                    Decline();
                }
            }
        }
    }
}
