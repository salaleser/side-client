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
        public GalaxyItem galaxyItem;

        private void Start()
        {
            if (galaxyItem.explorer_id == 0)
            {
                AddButton($"Explore \"{galaxyItem.title}\"", () => NetworkManager.Instance.GalaxyExplore(galaxyItem.id));
            }
            else
            {
                AddButton($"Zoom in \"{galaxyItem.title}\"", () => NetworkManager.Instance.Galaxy(galaxyItem.id));
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
                GameManager.SetDescription($"\n\n{galaxyItem}");
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
