using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class RentedRoom : Entity, IItem
    {
        public RentedRoomItem rentedRoomItem;

        private void Start()
        {
            AddButton($"Attach \"{rentedRoomItem.title}\" to organization", () => NetworkManager.Instance.OrganizationAttachRoom(GameManager.Instance.newOrganization.id, rentedRoomItem.id));
            AddButton($"Set storage room", () => NetworkManager.Instance.SetStorageRoom(rentedRoomItem.item_id));
            AddButton($"Inventory", () => NetworkManager.Instance.Inventory(rentedRoomItem.item_id));
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{rentedRoomItem}";
            ShowButtons();
        }
    }
}
