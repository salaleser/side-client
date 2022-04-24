using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Citizen : Entity
{
    public CitizenItem citizenItem;

    private void Start()
    {
        AddButton($"Inventory ({citizenItem.name})", () => NetworkManager.Instance.Inventory(citizenItem.root_item_id));
    }
}
