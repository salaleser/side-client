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
            AddButton($"Attached rooms...", () => NetworkManager.Instance.InstantiateRequiredRoomTypes(organizationItem));
            AddButton($"Pages...", () => NetworkManager.Instance.OrganizationPage(organizationItem));
            AddButton($"Properties...", () => NetworkManager.Instance.OrganizationProperties(organizationItem));
            
            var isMember = false;
            foreach (var member in organizationItem.members)
            {
                if (member.citizen_id == GameManager.Instance.me.id)
                {
                    isMember = true;
                }
            }
            if (isMember)
            {
                AddButton($"Leave", () => NetworkManager.Instance.MemberDelete(organizationItem.id, GameManager.Instance.me.id));
            }
            else
            {
                AddButton($"Join", () => NetworkManager.Instance.MemberCreate(organizationItem.id, GameManager.Instance.me.id));
            }
            
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
