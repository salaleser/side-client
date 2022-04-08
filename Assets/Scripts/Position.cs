using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Position : Entity, IItem
{
    public PositionItem item;

    private void Start()
    {
        AddButton($"Request {item.title}", () => NetworkManager.Instance.PositionRequest(GameManager.Instance.citizen.id, item.id));
    }

    public void Handler()
    {
        NetworkManager.Instance.HideAllButtons();
        NetworkManager.Instance.text.text = $"{item}";
        ShowButtons();
    }
}
