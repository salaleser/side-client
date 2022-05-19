using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            AddButton($"Create Room", () => NetworkManager.Instance.InstantiateCreateRoomPopup(roomItem.z + 1));
            if (GameManager.Instance.me.room.id == roomItem.id)
            {
                AddButton($"Inventory", () => NetworkManager.Instance.InstantiateCitizen("Items"));
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
            if (!GameManager.QuickMenuActive && !GameManager.WindowActive && !GameManager.PopupActive)
            {
                NetworkManager.Instance.text.text = $"\n\n{roomItem}";
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
