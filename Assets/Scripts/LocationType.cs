using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class LocationType : Entity, IItem
{
    public LocationTypeItem item;

    private void Start()
    {
        AddButton($"Build {item.title}", () => NetworkManager.Instance.RegisterLocation(item.address_id, item.id));
    }

    public void Handler()
    {
        NetworkManager.Instance.HideAllButtons();
        NetworkManager.Instance.text.text = $"{item}";
        ShowButtons();
    }
}
