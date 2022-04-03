using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : Entity
{
    new public ItemItem item;

    private void Start()
    {
        AddButton("Market", () => NetworkManager.Instance.Market());
        AddButton("Open", () => NetworkManager.Instance.Inventory(item.id));
    }
}
