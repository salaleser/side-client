using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class RoomType : Entity, IItem
{
    public RoomTypeItem roomTypeItem;

    private void Start()
    {
        AddButton($"Create \"{roomTypeItem.title}\" 1x1", () => NetworkManager.Instance.CreateRoom(GameManager.Instance.floor.id, roomTypeItem.id, GameManager.Instance.cursorX, GameManager.Instance.cursorY, 1, 1));
        AddButton($"Create \"{roomTypeItem.title}\" 2x2", () => NetworkManager.Instance.CreateRoom(GameManager.Instance.floor.id, roomTypeItem.id, GameManager.Instance.cursorX, GameManager.Instance.cursorY, 2, 2));
        AddButton($"Create \"{roomTypeItem.title}\" 2x3", () => NetworkManager.Instance.CreateRoom(GameManager.Instance.floor.id, roomTypeItem.id, GameManager.Instance.cursorX, GameManager.Instance.cursorY, 2, 3));
        AddButton($"Create \"{roomTypeItem.title}\" 3x3", () => NetworkManager.Instance.CreateRoom(GameManager.Instance.floor.id, roomTypeItem.id, GameManager.Instance.cursorX, GameManager.Instance.cursorY, 3, 3));
        AddButton($"Create \"{roomTypeItem.title}\" 3x4", () => NetworkManager.Instance.CreateRoom(GameManager.Instance.floor.id, roomTypeItem.id, GameManager.Instance.cursorX, GameManager.Instance.cursorY, 3, 4));
        AddButton($"Create \"{roomTypeItem.title}\" 4x4", () => NetworkManager.Instance.CreateRoom(GameManager.Instance.floor.id, roomTypeItem.id, GameManager.Instance.cursorX, GameManager.Instance.cursorY, 4, 4));
    }

    public void Handler()
    {
        NetworkManager.Instance.HideAllButtons();
        NetworkManager.Instance.text.text = $"{roomTypeItem}";
        ShowButtons();
    }
}
