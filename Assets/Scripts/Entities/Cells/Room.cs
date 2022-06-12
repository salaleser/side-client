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
        public RoomItem Item;

        private void Start()
        {
            if (GameManager.Instance.Citizen.organizations
                .Any(x => x.id == Item.organization_id)
                || Item.id == GameManager.Instance.Citizen.room.id
                || GameManager.Instance.Citizen.workplace.id == Item.id)
            {
                AddButton($"Move Into Room", () => NetworkManager.Instance.CitizenMove(GameManager.Instance.Citizen.id, Item.parcel_id, (int)GameManager.Instance.Cursor.transform.position.x, (int)GameManager.Instance.Cursor.transform.position.z, Item.z));
            }
            else
            {
                AddButton($"Enter Password", () => NetworkManager.Instance.InstantiateEnterPasswordPopup(Item));
            }
            AddButton($"Zoom Out", () => NetworkManager.Instance.ZoomOutButton());
            AddButton($"Create Room", () => NetworkManager.Instance.InstantiateCreateRoomPopup(Item.z + 1));


            if (Item.id == GameManager.Instance.Citizen.room.id)
            {
                if (Item.organization_id == 0)
                {
                    UnityAction<string> action = (text) => StartCoroutine(NetworkManager.Instance.Request("room-attach", new string[]{Item.id.ToString(), text}, (json) => {
                        NetworkManager.Instance.InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
                    }));
                    AddButton($"Attach Room", () => NetworkManager.Instance.InstantiateInputFieldPopup("Enter Organization ID", "-", action));
                }
                else if (GameManager.Instance.Citizen.organizations
                    .Any(x => x.id == Item.organization_id))
                {
                    AddButton($"Detach Room", () => StartCoroutine(NetworkManager.Instance.Request("room-detach", new string[]{GameManager.Instance.Citizen.id.ToString(), Item.id.ToString()}, (json) => {
                        NetworkManager.Instance.InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
                    })));
                }
            }
        }

        private void OnMouseEnter()
        {
            if (!GameManager.RadialMenuActive
                && !GameManager.WindowActive
                && !GameManager.PopupActive
                && !Mouse.current.leftButton.isPressed)
            {
                GameManager.DescriptionSetText(Item.ToString());
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            }
        }
    }
}
