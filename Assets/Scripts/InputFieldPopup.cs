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
        public TMP_Text Caption;
        public TMP_InputField InputField;
        public UnityAction<string> Action;

        private void Start()
        {
            InputField.Select();
        }

        private void Update()
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                Action(InputField.text);
                GameManager.SetShortcutsActive(true);
                Destroy(gameObject);
            }
            else if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                GameManager.SetShortcutsActive(true);
                Destroy(gameObject);
            }
        }
    }
}
