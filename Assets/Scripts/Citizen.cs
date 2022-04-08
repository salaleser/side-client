using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Citizen : Entity
{
    public CitizenItem item;

    private void Start()
    {
        AddButton($"Inventory ({item.name})", () => NetworkManager.Instance.Inventory(item.root_item_id));
    }
}
