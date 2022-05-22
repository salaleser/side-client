using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class DealPopup : MonoBehaviour
    {
        public TMP_Text Description;
        public DealItem Deal;
        public TMP_InputField Quantity;
        public TMP_InputField DeliveryAddress;

        private void Start()
        {
            Description.text = Deal.ToString();
            DeliveryAddress.text = Deal.buyer.room.id.ToString();
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
            if (int.Parse(Quantity.text) > Deal.item.quantity)
            {
                NetworkManager.Instance.InstantiateNoticePopup("ERROR", "Нельзя купить больше чем доступно");
                return;
            }

            NetworkManager.Instance.DealAccept(Deal.id, int.Parse(Quantity.text));
            Destroy(this.gameObject);
        }

        public void Decline()
        {
            NetworkManager.Instance.DealDecline(Deal.id);
            Destroy(this.gameObject);
        }
    }
}
