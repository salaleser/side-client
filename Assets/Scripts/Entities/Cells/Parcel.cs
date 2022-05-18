using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Parcel : Entity
    {
        public ParcelItem parcelItem;

        private void Start()
        {
            AddButton($"Zoom in \"{parcelItem.title}\"", () => NetworkManager.Instance.Parcel(parcelItem.id));
        }

        private void OnMouseEnter()
        {
            if (!GameManager.QuickMenuActive && !GameManager.WindowActive && !GameManager.PopupActive)
            {
                NetworkManager.Instance.text.text = $"\n\n{parcelItem}";
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
