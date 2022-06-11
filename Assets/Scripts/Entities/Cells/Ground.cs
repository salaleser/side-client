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
        public GroundItem groundItem;

        private void Start()
        {
            AddButton($"Create Room", () => NetworkManager.Instance.InstantiateCreateRoomPopup(groundItem.z + 1));
            AddButton($"Reload Parcel", () => NetworkManager.Instance.ReloadButton());
            AddButton($"Zoom Out", () => NetworkManager.Instance.ZoomOutButton());
            AddButton($"Me", () => NetworkManager.Instance.MeButton());
            AddButton($"Center Camera", () => NetworkManager.Instance.CenterCameraButton());
            AddButton($"Move To Ground", () => NetworkManager.Instance.CitizenMove(GameManager.Instance.me.id, groundItem.parcel_id, groundItem.x, groundItem.y, groundItem.z));
            AddButton($"PDA", () => NetworkManager.Instance.PdaButton());
        }

        private void OnMouseEnter()
        {
            if (!GameManager.QuickMenuActive
                && !GameManager.WindowActive
                && !GameManager.PopupActive
                && !Mouse.current.rightButton.isPressed)
            {
                GameManager.SetDescription($"\n\n{groundItem}");
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
