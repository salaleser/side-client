using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class InvitePopup : MonoBehaviour
    {
        public TMP_Text Description;
        public InviteItem Invite;

        private void Start()
        {
            Description.text = Invite.ToString();
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    Accept();
                }
            }
        }

        public void Accept()
        {
            NetworkManager.Instance.InviteAccept(Invite.id);
            Destroy(this.gameObject);
        }

        public void Decline()
        {
            NetworkManager.Instance.InviteDecline(Invite.id);
            Destroy(this.gameObject);
        }
    }
}
