using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Ground : Entity
{
    public GroundItem groundItem;

    private void Start()
    {
        switch (groundItem.location_type_id)
        {
            case 7: // city
                AddButton($"Found City", () => NetworkManager.Instance.CreateCity(GameManager.Instance.region.id, groundItem.x, groundItem.y, "untitled"));
                break;
            case 8: // block
                foreach(var block in GameManager.Instance.city.blocks)
                {
                    if ((Math.Abs(groundItem.x - block.x) == 1 && groundItem.y == block.y) ||
                        (Math.Abs(groundItem.y - block.y) == 1 && groundItem.x == block.x))
                    {
                        AddButton($"Expand Block", () => NetworkManager.Instance.CreateBlock(GameManager.Instance.city.id, groundItem.x, groundItem.y));
                    }
                }
                break;
            case 9: // parcel
                AddButton($"Claim Parcel", () => NetworkManager.Instance.CreateParcel(GameManager.Instance.block.id, groundItem.x, groundItem.y));
                break;
            case 10: // floor
                if (GameManager.Instance.parcel.owner_id == GameManager.Instance.citizen.id)
                {
                    AddButton($"Build Floor 1x1", () => NetworkManager.Instance.CreateFloor(GameManager.Instance.parcel.id, 1, groundItem.x, groundItem.y, 0, 1, 1));
                    AddButton($"Build Floor 2x2", () => NetworkManager.Instance.CreateFloor(GameManager.Instance.parcel.id, 1, groundItem.x, groundItem.y, 0, 2, 2));
                    AddButton($"Build Floor 3x3", () => NetworkManager.Instance.CreateFloor(GameManager.Instance.parcel.id, 1, groundItem.x, groundItem.y, 0, 3, 3));
                    AddButton($"Build Floor 4x4", () => NetworkManager.Instance.CreateFloor(GameManager.Instance.parcel.id, 1, groundItem.x, groundItem.y, 0, 4, 4));
                }
                break;
        }
    }
}
