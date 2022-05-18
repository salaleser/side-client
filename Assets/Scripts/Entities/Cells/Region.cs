using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        }

        private void OnMouseEnter()
        {
            if (!GameManager.QuickMenuActive && !GameManager.WindowActive && !GameManager.PopupActive)
            {
                NetworkManager.Instance.text.text = $"\n\n{regionItem}";
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
