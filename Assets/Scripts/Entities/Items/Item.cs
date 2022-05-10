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
            if (itemItem.children_count > 0)
            {
                AddButton($"Open", () => NetworkManager.Instance.Inventory(itemItem.id));
            }
            if (GameManager.Instance.currentItem.parent_id != 0)
            {
                AddButton($"Back", () => NetworkManager.Instance.Inventory(GameManager.Instance.currentItem.parent_id));
            }
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{itemItem}";
            ShowButtons();
        }
    }
}
