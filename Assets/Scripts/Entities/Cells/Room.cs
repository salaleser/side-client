using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
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
            if (GameManager.Instance.me.organizations
                .Any(x => x.id == roomItem.organization_id)
                || roomItem.id == GameManager.Instance.me.room.id
                || GameManager.Instance.me.workplace.id == roomItem.id)
            {
                AddButton($"Move Into Room", () => NetworkManager.Instance.CitizenMove(GameManager.Instance.me.id, roomItem.parcel_id, (int)GameManager.Instance.Cursor.transform.position.x, (int)GameManager.Instance.Cursor.transform.position.z, roomItem.z));
            }
            else
            {
                AddButton($"Enter Password", () => NetworkManager.Instance.InstantiateEnterPasswordPopup(roomItem));
            }
            AddButton($"Zoom Out", () => NetworkManager.Instance.ZoomOutButton());
            AddButton($"Create Room", () => NetworkManager.Instance.InstantiateCreateRoomPopup(roomItem.z + 1));


            if (roomItem.id == GameManager.Instance.me.room.id)
            {
                if (roomItem.organization_id == 0)
                {
                    UnityAction<string> action = (text) => StartCoroutine(NetworkManager.Instance.Request("room-attach", new string[]{roomItem.id.ToString(), text}, (json) => {
                        NetworkManager.Instance.InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
                    }));
                    AddButton($"Attach Room", () => NetworkManager.Instance.InstantiateInputFieldPopup("Enter Organization ID", action));
                }
                else if (GameManager.Instance.me.organizations
                    .Any(x => x.id == roomItem.organization_id))
                {
                    AddButton($"Detach Room", () => StartCoroutine(NetworkManager.Instance.Request("room-detach", new string[]{GameManager.Instance.me.id.ToString(), roomItem.id.ToString()}, (json) => {
                        NetworkManager.Instance.InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
                    })));
                }
            }
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
