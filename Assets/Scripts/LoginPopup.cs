using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class LoginPopup : MonoBehaviour
    {
        public TMP_InputField CitizenId;

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    Login(int.Parse(CitizenId.text));
                }
            }
        }

        public void Login(int citizenId)
        {
            NetworkManager.Instance.Login(citizenId == 0 ? int.Parse(CitizenId.text) : citizenId);
            Destroy(this.gameObject);
        }
    }
}
