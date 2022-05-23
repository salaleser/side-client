using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Region : Entity
    {
        public RegionItem regionItem;

        private void Start()
        {
            if (regionItem.explorer_id == 0)
            {
                AddButton($"Explore \"{regionItem.title}\"", () => NetworkManager.Instance.RegionExplore(regionItem.id));
            }
            else
            {
                AddButton($"Zoom in \"{regionItem.title}\"", () => NetworkManager.Instance.Region(regionItem.id));
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
                GameManager.SetDescription($"\n\n{regionItem}");
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
