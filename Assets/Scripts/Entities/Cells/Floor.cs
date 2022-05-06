using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Floor : Entity
    {
        public FloorItem floorItem;

        private void Start()
        {
            if (floorItem.entrance_room_id != 0)
            {
                AddButton($"Move inside \"{floorItem.title}\"", () => NetworkManager.Instance.MoveIntoRoom(floorItem.entrance_room_id));
            }

            if (GameManager.Instance.currentParcel.owner_id == GameManager.Instance.me.id)
            {
                AddButton($"Build Floor {floorItem.z + 1 + 1}", () => NetworkManager.Instance.CreateFloor(GameManager.Instance.currentParcel.id, 1, floorItem.x, floorItem.y, floorItem.z + 1, floorItem.w, floorItem.h));
            }
        }
    }
}
