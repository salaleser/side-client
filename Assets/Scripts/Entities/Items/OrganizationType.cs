using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class OrganizationType : Entity, IItem
    {
        public OrganizationTypeItem organizationTypeItem;

        private void Start()
        {
            AddButton($"Create \"{organizationTypeItem.title}\"", () => NetworkManager.Instance.OrganizationCreate(organizationTypeItem.id));
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{organizationTypeItem}";
            ShowButtons();
        }
    }
}
