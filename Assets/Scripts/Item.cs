using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Item : Entity, IItem
{
    public ItemItem item;

    private void Start()
    {
        AddButton("Market", () => NetworkManager.Instance.Market());
        AddButton("Open", () => NetworkManager.Instance.Inventory(item.id));
    }

    public void Handler()
    {
        NetworkManager.Instance.HideAllButtons();
        NetworkManager.Instance.text.text = $"{item}";
        ShowButtons();
    }
}
