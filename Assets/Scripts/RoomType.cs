using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomType : Entity, IItem
{
    public RoomTypeItem item;

    private void Start()
    {
        AddButton($"Build {item.title}", () => NetworkManager.Instance.RegisterRoom(GameManager.Instance.floor.id, item.id));
    }

    public void Handler()
    {
        NetworkManager.Instance.HideAllButtons();
        NetworkManager.Instance.text.text = $"{item}";
        ShowButtons();
    }
}
