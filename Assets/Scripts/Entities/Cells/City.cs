using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class City : Entity
    {
        public CityItem cityItem;

        private void Start()
        {
            if (cityItem.explorer_id == 0)
            {
                AddButton($"Explore City", () => NetworkManager.Instance.CityExplore(cityItem.id));
            }
            else
            {
                AddButton($"Zoom in \"{cityItem.title}\"", () => NetworkManager.Instance.City(cityItem.id));
            }
            AddButton($"Zoom Out", () => NetworkManager.Instance.ZoomOutButton());
            AddButton($"Center Camera", () => NetworkManager.Instance.CenterCameraButton());
            AddButton($"Center Me", () => NetworkManager.Instance.CenterMeButton());
        }

        private void OnMouseEnter()
        {
            if (!GameManager.QuickMenuActive
                && !GameManager.WindowActive
                && !GameManager.PopupActive
                && !Mouse.current.rightButton.isPressed)
            {
                GameManager.SetDescription($"\n\n{cityItem}");
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
