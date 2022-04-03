using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationType : Entity
{
    new public LocationTypeItem item;

    private void Start()
    {
        AddButton($"Build {item.title}", () => NetworkManager.Instance.RegisterLocation(item.address_id, item.id));
    }
}
