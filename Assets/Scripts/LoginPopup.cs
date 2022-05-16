using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class LoginPopup : Popup
    {
        public TMP_InputField citizenId;

        private void OnEnable()
        {
            citizenId.Select();
        }

        private void Update()
        {
            if (GameManager.IsShortcutsActive)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    Login();
                }
            }
        }

        public void Login()
        {
            NetworkManager.Instance.Login(int.Parse(citizenId.text == "" ? "1" : citizenId.text));
            Destroy(this.gameObject);
        }
    }
}
