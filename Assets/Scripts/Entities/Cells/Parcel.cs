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
        public ParcelItem Item;

        private void Start()
        {
            AddButton($"Zoom in \"{Item.title}\"", () => NetworkManager.Instance.Parcel(Item.id));
            AddButton($"Center Me", () => NetworkManager.Instance.CenterMeButton());
            AddButton($"Zoom Out", () => NetworkManager.Instance.ZoomOutButton());
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
