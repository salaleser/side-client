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
        }
    }
}
