using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class OrganizationType : Entity
    {
        public OrganizationTypeItem organizationTypeItem;

        private void Start()
        {
            AddButton($"Create \"{organizationTypeItem.title}\"", () => NetworkManager.Instance.OrganizationCreate(organizationTypeItem.id));
        }
    }
}
