using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Floor : Entity
{
    public FloorItem floorItem;

    private void Start()
    {
        AddButton($"Move inside \"{floorItem.title}\"", () => NetworkManager.Instance.MoveIntoRoom(floorItem.entrance_room_id));
    }
}
