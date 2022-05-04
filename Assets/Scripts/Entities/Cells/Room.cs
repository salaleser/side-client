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
            if (roomItem.id == 0 && GameManager.Instance.currentParcel.owner_id == GameManager.Instance.me.id)
            {
                AddButton($"Build Room", () => NetworkManager.Instance.RoomTypes());
            }
            else if (GameManager.Instance.me.room_id == roomItem.id)
            {
                AddButton($"Inventory", () => NetworkManager.Instance.Inventory(roomItem.item_id));
                switch (roomItem.type_id)
                {
                    case 1:
                        AddButton("Get Tasks", () => NetworkManager.Instance.Tasks(GameManager.Instance.me.organization_id));
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
