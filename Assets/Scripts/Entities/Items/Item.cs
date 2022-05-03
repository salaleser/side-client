using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class Item : Entity, IItem
    {
        public ItemItem itemItem;

        private void Start()
        {
            AddButton($"Sell", () => NetworkManager.Instance.ItemSell(itemItem));
            AddButton($"Open", () => NetworkManager.Instance.Inventory(itemItem.id));
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{itemItem}";
            ShowButtons();
        }
    }
}
