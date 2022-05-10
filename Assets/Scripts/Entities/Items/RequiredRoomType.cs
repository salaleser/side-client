using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class RequiredRoomType : Entity, IItem
    {
        public RequiredRoomTypeItem requiredRoomTypeItem;

        private void Start()
        {
            if (requiredRoomTypeItem.attached_room != null)
            {
                AddButton($"Inventory...", () => NetworkManager.Instance.Inventory(requiredRoomTypeItem.attached_room.item_id));
            }
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{requiredRoomTypeItem}";
            ShowButtons();
        }
    }
}
