using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Ground : Entity
    {
        public GroundItem Item;

        private void Start()
        {
            AddButton($"Create Room", () => NetworkManager.Instance.InstantiateCreateRoomPopup(Item.z + 1));
            AddButton($"Zoom Out", () => NetworkManager.Instance.ZoomOutButton());
            AddButton($"Me", () => NetworkManager.Instance.MeButton());
            AddButton($"Move", () => NetworkManager.Instance.CitizenMove(GameManager.Instance.Citizen.id, Item.parcel_id, Item.x, Item.y, Item.z));
            AddButton($"", null, false);
            AddButton($"Center Camera", () => NetworkManager.Instance.CenterCameraButton());
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
