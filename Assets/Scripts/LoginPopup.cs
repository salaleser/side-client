using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class LoginPopup : Popup
    {
        public TMP_InputField citizenId;

        public void Login()
        {
            NetworkManager.Instance.Login(int.Parse(citizenId.text));
            Destroy(this.gameObject);
        }
    }
}
