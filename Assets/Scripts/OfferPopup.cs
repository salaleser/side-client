using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class OfferPopup : MonoBehaviour
    {
        public TMP_Text Description;
        public OfferItem Offer;

        private void Start()
        {
            Description.text = Offer.ToString();
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
            NetworkManager.Instance.OfferAccept(Offer.id);
            Destroy(this.gameObject);
        }

        public void Decline()
        {
            NetworkManager.Instance.OfferDecline(Offer.id);
            Destroy(this.gameObject);
        }
    }
}
