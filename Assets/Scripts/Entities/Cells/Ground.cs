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
                break;
            case 8: // block
                break;
            case 9: // parcel
                break;
            case 10: // floor
                AddButton($"Build Floor 1x1", () => NetworkManager.Instance.CreateFloor(GameManager.Instance.parcel.id, 1, groundItem.x, groundItem.y, 0, 1, 1));
                AddButton($"Build Floor 2x2", () => NetworkManager.Instance.CreateFloor(GameManager.Instance.parcel.id, 1, groundItem.x, groundItem.y, 0, 2, 2));
                AddButton($"Build Floor 3x3", () => NetworkManager.Instance.CreateFloor(GameManager.Instance.parcel.id, 1, groundItem.x, groundItem.y, 0, 3, 3));
                AddButton($"Build Floor 4x4", () => NetworkManager.Instance.CreateFloor(GameManager.Instance.parcel.id, 1, groundItem.x, groundItem.y, 0, 4, 4));
                break;
        }
    }
}
