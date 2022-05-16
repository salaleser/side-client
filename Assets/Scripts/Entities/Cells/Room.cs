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
            if (GameManager.Instance.me.room_id == roomItem.id)
            {
                AddButton($"Inventory", () => NetworkManager.Instance.Inventory(roomItem.item_id));
                switch (roomItem.type.id)
                {
                    case 1:
                        AddButton("Get Tasks", () => NetworkManager.Instance.Tasks(roomItem.organization_ids));
                        break;
                }
            }
            else
            {
                AddButton($"Move into {roomItem.type.title} \"{roomItem.title}\"", () => NetworkManager.Instance.MoveIntoRoom(GameManager.Instance.me.id, roomItem.id));
            }
        }

        private void OnMouseEnter()
        {
            NetworkManager.Instance.text.text = $"\n\n{roomItem}";
        }
    }
}
