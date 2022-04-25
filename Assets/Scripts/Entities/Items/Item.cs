using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Item : Entity, IItem
{
    public ItemItem itemItem;

    private void Start()
    {
        AddButton("Market", () => NetworkManager.Instance.Market());
        AddButton("Open", () => NetworkManager.Instance.Inventory(itemItem.id));
    }

    public void Handler()
    {
        NetworkManager.Instance.HideAllButtons();
        NetworkManager.Instance.text.text = $"{itemItem}";
        ShowButtons();
    }
}
