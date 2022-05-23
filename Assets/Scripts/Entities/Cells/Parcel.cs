using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Parcel : Entity
    {
        public ParcelItem parcelItem;

        private void Start()
        {
            AddButton($"Zoom in \"{parcelItem.title}\"", () => NetworkManager.Instance.Parcel(parcelItem.id));
            AddButton($"Center Me", () => NetworkManager.Instance.CenterMeButton());
            AddButton($"Zoom Out", () => NetworkManager.Instance.ZoomOutButton());
        }

        private void OnMouseEnter()
        {
            if (!GameManager.QuickMenuActive
                && !GameManager.WindowActive
                && !GameManager.PopupActive
                && !Mouse.current.rightButton.isPressed)
            {
                GameManager.SetDescription($"\n\n{parcelItem}");
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
