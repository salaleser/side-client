using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class PopupManager : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.SetPopupActive(true);
        }

        private void OnDisable()
        {
            GameManager.SetPopupActive(false);
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.qKey.wasPressedThisFrame
                    || Keyboard.current.escapeKey.wasPressedThisFrame
                    || Mouse.current.rightButton.wasPressedThisFrame)
                {
                    ClosePopup();
                }
            }
        }

        public void ClosePopup()
        {
            Destroy(this.gameObject);
        }
    }
}
