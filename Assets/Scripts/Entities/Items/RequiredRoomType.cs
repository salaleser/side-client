using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class RequiredRoomType : Entity
    {
        public RequiredRoomTypeItem requiredRoomTypeItem;

        private void Start()
        {
            if (requiredRoomTypeItem.attached_room != null)
            {
                AddButton($"Inventory...", () => NetworkManager.Instance.Inventory(requiredRoomTypeItem.attached_room.item_id));
            }
        }
    }
}
