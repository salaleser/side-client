using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lot : Entity
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
