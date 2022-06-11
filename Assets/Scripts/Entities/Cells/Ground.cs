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
            AddButton($"Reload Parcel", () => NetworkManager.Instance.ReloadButton());
            AddButton($"Zoom Out", () => NetworkManager.Instance.ZoomOutButton());
            AddButton($"Me", () => NetworkManager.Instance.MeButton());
            AddButton($"Center Camera", () => NetworkManager.Instance.CenterCameraButton());
            AddButton($"Move To Ground", () => NetworkManager.Instance.CitizenMove(GameManager.Instance.Me.id, Item.parcel_id, Item.x, Item.y, Item.z));
            AddButton($"PDA", () => NetworkManager.Instance.PdaButton());
        }

        private void OnMouseEnter()
        {
            if (!GameManager.RadialMenuActive
                && !GameManager.WindowActive
                && !GameManager.PopupActive
                && !Mouse.current.rightButton.isPressed)
            {
                GameManager.SetDescriptionText(Item.ToString());
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            }
        }
    }
}
