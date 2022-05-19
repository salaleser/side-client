using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities
{
    public class Citizen : Entity
    {
        public CitizenItem citizenItem;

        private void Start()
        {
        }

        private void OnMouseEnter()
        {
            if (!GameManager.QuickMenuActive && !GameManager.WindowActive && !GameManager.PopupActive)
            {
                NetworkManager.Instance.text.text = $"\n\n{citizenItem}";
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
