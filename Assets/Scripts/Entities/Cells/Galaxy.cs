using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        }

        private void OnMouseEnter()
        {
            if (!GameManager.QuickMenuActive && !GameManager.WindowActive && !GameManager.PopupActive)
            {
                NetworkManager.Instance.text.text = $"\n\n{galaxyItem}";
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
