using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Planet : Entity
    {
        public PlanetItem Item;

        private void Start()
        {
            if (Item.explorer_id == 0)
            {
                AddButton($"Explore \"{Item.title}\"", () => NetworkManager.Instance.PlanetExplore(Item.id));
            }
            else
            {
                AddButton($"Zoom in \"{Item.title}\"", () => NetworkManager.Instance.Planet(Item.id));
            }
            AddButton($"Zoom Out", () => NetworkManager.Instance.ZoomOutButton());
            AddButton($"Center Camera", () => NetworkManager.Instance.CenterCameraButton());
            AddButton($"Center Me", () => NetworkManager.Instance.CenterMeButton());
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
