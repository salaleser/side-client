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
            AddButton($"Attach...", () => NetworkManager.Instance.RentedRooms(requiredRoomTypeItem.id));
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{requiredRoomTypeItem}";
            ShowButtons();
        }
    }
}
