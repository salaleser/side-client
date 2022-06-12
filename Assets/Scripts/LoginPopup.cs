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
        public TMP_InputField UserId;

        private void Update()
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                Login(int.Parse(UserId.text));
            }
        }

        public void Login(int userId)
        {
            NetworkManager.Instance.User(userId);
            Destroy(gameObject);
        }
    }
}
