using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            if (organizationItem.owner_id == GameManager.Instance.me.id)
            {
                AddButton($"Properties...", () => NetworkManager.Instance.InstantiateOrganization(organizationItem));
            }
            
            if (organizationItem.members.Any(x => x.citizen_id == GameManager.Instance.me.id))
            {
                AddButton($"Leave", () => NetworkManager.Instance.MemberDelete(organizationItem.id, GameManager.Instance.me.id));
            }
            else
            {
                AddButton($"Join", () => NetworkManager.Instance.MemberCreate(organizationItem.id, GameManager.Instance.me.id));
            }
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{organizationItem}";
            ShowButtons();
            GameManager.Instance.newOrganization = organizationItem;
        }
    }
}
