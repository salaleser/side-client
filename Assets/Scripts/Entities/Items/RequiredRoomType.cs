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
            if (requiredRoomTypeItem.is_attached)
            {
                AddButton($"Detach \"{requiredRoomTypeItem.title}\" from \"{GameManager.Instance.currentOrganization.title}\"", () => NetworkManager.Instance.OrganizationDetachRoom(requiredRoomTypeItem.attached_room_id, GameManager.Instance.currentOrganization.id));
            }
            else
            {
                AddButton($"Attach...", () => NetworkManager.Instance.RentedRooms(requiredRoomTypeItem.id));
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
