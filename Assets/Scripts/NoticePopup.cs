using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class NoticePopup : MonoBehaviour
    {
        public TMP_Text caption;
        public TMP_Text description;

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    this.GetComponent<PopupManager>().ClosePopup();
                }
                else if (Keyboard.current.escapeKey.wasPressedThisFrame)
                {
                    this.GetComponent<PopupManager>().ClosePopup();
                }
            }
        }
    }
}
