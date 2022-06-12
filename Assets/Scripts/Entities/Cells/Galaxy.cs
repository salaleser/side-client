using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Galaxy : Entity
    {
        public GalaxyItem Item;

        private void Start()
        {
            if (Item.explorer_id == 0)
            {
                AddButton($"Explore \"{Item.title}\"", () => NetworkManager.Instance.GalaxyExplore(Item.id));
            }
            else
            {
                AddButton($"Zoom in \"{Item.title}\"", () => NetworkManager.Instance.Galaxy(Item.id));
            }
            AddButton($"Zoom Out", () => NetworkManager.Instance.ZoomOutButton());
            AddButton($"Center Camera", () => NetworkManager.Instance.CenterCameraButton());
            AddButton($"Center Me", () => NetworkManager.Instance.CenterCitizenButton(GameManager.Instance.Citizen));
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
