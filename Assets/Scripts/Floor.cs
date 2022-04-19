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
        AddButton($"Zoom in \"{floorItem.title}\"", () => NetworkManager.Instance.Floor(floorItem.id));
    }
}
