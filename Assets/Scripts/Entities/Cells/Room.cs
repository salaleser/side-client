using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Room : Entity
    {
        public RoomItem roomItem;

        private void Start()
        {
            if (GameManager.Instance.currentParcel.owner_id == GameManager.Instance.me.id)
            {
                if (roomItem.id == 0)
                {
                    AddButton($"Build Room", () => NetworkManager.Instance.RoomTypes());
                }
            }
            
            if (GameManager.Instance.me.room_id == roomItem.id)
            {
                AddButton($"Inventory", () => NetworkManager.Instance.Inventory(roomItem.item_id));
                switch (roomItem.type_id)
                {
                    case 1:
                        AddButton("Get Tasks", () => NetworkManager.Instance.Tasks(GameManager.Instance.me.organization_id));
                        break;
                    case 13:
                        foreach (var floor in GameManager.Instance.currentParcel.floors)
                        {
                            if (floor.x == GameManager.Instance.currentFloor.x && floor.y == GameManager.Instance.currentFloor.y)
                            {
                                AddButton($"Floor {floor.z}", () => NetworkManager.Instance.Floor(floor.id));
                            }
                        }
                        break;
                }
            }
            else if (roomItem.id > 0)
            {
                AddButton($"Go to \"{roomItem.description}\"", () => NetworkManager.Instance.MoveIntoRoom(roomItem.id));
            }
        }
    }
}
