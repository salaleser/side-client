using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class DealPopup : Popup
    {
        public TMP_Text caption;
        public TMP_Text description;
        public DealItem deal;
        public TMP_InputField quantity;
        public TMP_InputField deliveryAddress;

        private void Start()
        {
            description.text = deal.ToString();
            deliveryAddress.text = deal.buyer.room.id.ToString();
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

        public void Accept()
        {
            if (int.Parse(quantity.text) > deal.item.quantity)
            {
                NetworkManager.Instance.InstantiateNoticePopup("ERROR", "Нельзя купить больше чем доступно");
                return;
            }

            NetworkManager.Instance.DealAccept(deal.id, int.Parse(quantity.text));
            Destroy(this.gameObject);
        }

        public void Decline()
        {
            NetworkManager.Instance.DealDecline(deal.id);
            Destroy(this.gameObject);
        }
    }
}
