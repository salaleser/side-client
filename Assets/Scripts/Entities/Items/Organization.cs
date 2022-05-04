using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class Organization : Entity, IItem
    {
        public OrganizationItem organizationItem;

        private void Start()
        {
            AddButton($"Manage rooms", () => NetworkManager.Instance.InstantiateRequiredRoomTypes(organizationItem));
            
            foreach (var room in organizationItem.attached_rooms)
            {
                if (room.type_id == 4)
                {
                    AddButton($"Manage storage ({room.item_id})", () => NetworkManager.Instance.Inventory(room.item_id));
                    break;
                }
            }
        }

        public void Handler()
        {
            GameManager.Instance.currentOrganization = organizationItem;
            GameManager.Instance.state = GameManager.Organization;
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{organizationItem}";
            ShowButtons();
            GameManager.Instance.newOrganization = organizationItem;
        }
    }
}
