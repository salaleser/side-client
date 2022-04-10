using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Address : Entity
{
    public AddressItem item;

    private void Start()
    {
        if (item.type_id == AddressTypes.Facility && item.location == null)
        {
            AddButton("Build Location", () => NetworkManager.Instance.BuildLocation(item.id));
        }
        else if (item.type_id != AddressTypes.Facility)
        {
            AddButton($"Zoom in {item.type_id}", () => NetworkManager.Instance.Address(item.id));
        }
    }
}
