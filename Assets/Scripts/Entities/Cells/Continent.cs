using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Continent : Entity
    {
        public ContinentItem continentItem;

        private void Start()
        {
            if (continentItem.explorer_id == 0)
            {
                AddButton($"Explore \"{continentItem.title}\"", () => NetworkManager.Instance.ContinentExplore(continentItem.id));
            }
            else
            {
                AddButton($"Zoom in \"{continentItem.title}\"", () => NetworkManager.Instance.Continent(continentItem.id));
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
                GameManager.SetDescription($"\n\n{continentItem}");
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
