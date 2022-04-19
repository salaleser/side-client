using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Lot : Entity, IItem
{
    public LotItem item;

    private void Start()
    {
    }

    public void Handler()
    {
        NetworkManager.Instance.HideAllButtons();
        NetworkManager.Instance.text.text = $"{item}";
        ShowButtons();
    }
}
