using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;

namespace Entities
{
    public class Citizen : Entity
    {
        public CitizenItem citizenItem;

        private void Update()
        {
            if (citizenItem.id == GameManager.Instance.me.id)
            {
                
            }

            
        }

        private void OnGUI()
        {
            if (Keyboard.current.gKey.isPressed)
            {
                GUI.Label(new Rect(100, 100, 200, 100), "Test...");
            }
        }

        private void OnMouseEnter()
        {
            if (!GameManager.QuickMenuActive
                && !GameManager.WindowActive
                && !GameManager.PopupActive
                && !Mouse.current.rightButton.isPressed)
            {
                GameManager.SetDescription($"\n\n{citizenItem}");
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
