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
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{organizationItem}";
            ShowButtons();
            GameManager.Instance.organization = organizationItem;
        }
    }
}
