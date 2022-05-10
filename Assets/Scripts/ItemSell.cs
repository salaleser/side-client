using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class ItemSell : MonoBehaviour
    {
        public TMP_InputField quantity;
        public TMP_InputField price;
        public TMP_Text quantityValue;
        public TMP_Text priceValue;

        void OnEnable()
        {
            quantity.onSubmit.AddListener(QuantityChange);
            price.onSubmit.AddListener(PriceChange);
        }

        void OnDisable()
        {
            quantity.onSubmit.RemoveListener(QuantityChange);
            price.onSubmit.RemoveListener(PriceChange);
        }

        public void QuantityChange(string text)
        {
            GameManager.Instance.newLot.quantity += int.Parse(text);
            quantityValue.text = GameManager.Instance.newLot.quantity.ToString();
        }

        public void PriceChange(string text)
        {
            GameManager.Instance.newLot.price += int.Parse(text);
            priceValue.text = GameManager.Instance.newLot.price.ToString();
        }

        public void SellItem()
        {
            NetworkManager.Instance.LotCreate(GameManager.Instance.newLot);
        }
    }
}
