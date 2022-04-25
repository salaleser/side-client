using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class OrganizationType : Entity, IItem
{
    public OrganizationTypeItem organizationTypeItem;

    private void Start()
    {
        AddButton($"Start \"{organizationTypeItem.title}\"", () => NetworkManager.Instance.CreateOrganization(organizationTypeItem.id));
    }

    public void Handler()
    {
        NetworkManager.Instance.HideAllButtons();
        NetworkManager.Instance.text.text = $"{organizationTypeItem}";
        ShowButtons();
    }
}
