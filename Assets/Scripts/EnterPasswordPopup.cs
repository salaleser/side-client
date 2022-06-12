using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class EnterPasswordPopup : MonoBehaviour
    {
        public TMP_Text Description;
        public TMP_InputField Password;

        private RoomItem _room;

        private void Start()
        {
            Description.text = _room.ToString();
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    EnterRoom();
                }
                else if (Keyboard.current.escapeKey.wasPressedThisFrame)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void EnterRoom()
        {
            NetworkManager.Instance.CitizenEnterRoom(GameManager.Instance.Citizen.id, _room.id, Password.text);
        }

        public void SetRoom(RoomItem room) => _room = room;
    }
}
