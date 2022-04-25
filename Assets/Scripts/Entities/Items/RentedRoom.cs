using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Items
{
    public class RentedRoom : Entity, IItem
    {
        public RentedRoomItem rentedRoomItem;

        private void Start()
        {
            AddButton($"Attach \"{rentedRoomItem.title}\" to organization", () => NetworkManager.Instance.AttachRoomToOrganization(GameManager.Instance.newOrganization.id, rentedRoomItem.id));
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{rentedRoomItem}";
            ShowButtons();
        }
    }
}
