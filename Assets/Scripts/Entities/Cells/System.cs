using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class System : Entity
    {
        public SystemItem systemItem;

        private void Start()
        {
            if (systemItem.explorer_id == 0)
            {
                AddButton($"Explore \"{systemItem.title}\"", () => NetworkManager.Instance.SystemExplore(systemItem.id));
            }
            else
            {
                AddButton($"Zoom in \"{systemItem.title}\"", () => NetworkManager.Instance.System(systemItem.id));
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
                GameManager.SetDescription($"\n\n{systemItem}");
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
