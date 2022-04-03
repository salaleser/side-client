using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Address : Entity
{
    new public AddressItem item;

    private void Start()
    {
        if (item.id != 0)
        {
            if (item.type_id < AddressTypes.Facility)
            {
                AddButton("Zoom in", () => NetworkManager.Instance.Address(item.id));
            }

            if (item.type_id == AddressTypes.Facility && item.location == null)
            {
                AddButton("Build Location", () => NetworkManager.Instance.Build(item.id));
            }
        }
        else
        {
            AddButton("Build Address", () => NetworkManager.Instance.RegisterAddress(GameManager.Instance.address.id, item.x, item.y, ""));
        }
    }
}
