using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Room : Entity
    {
        public RoomItem roomItem;

        private void Start()
        {
            if (roomItem.creator_id == GameManager.Instance.me.id || roomItem.id == GameManager.Instance.me.room.id)
            {
                AddButton($"Move Into Room", () => NetworkManager.Instance.CitizenMove(GameManager.Instance.me.id, GameManager.Instance.me.parcel_id, (int)GameManager.Instance.Cursor.transform.position.x, (int)GameManager.Instance.Cursor.transform.position.z, roomItem.z));
            }
            else
            {
                AddButton($"Enter Password", () => NetworkManager.Instance.InstantiateEnterPasswordPopup(roomItem));
            }
            AddButton($"Zoom Out", () => NetworkManager.Instance.ZoomOutButton());
            AddButton($"Create Room", () => NetworkManager.Instance.InstantiateCreateRoomPopup(roomItem.z + 1));
        }

        private void OnMouseEnter()
        {
            if (!GameManager.QuickMenuActive
                && !GameManager.WindowActive
                && !GameManager.PopupActive
                && !Mouse.current.rightButton.isPressed)
            {
                GameManager.SetDescription($"\n\n{roomItem}");
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
