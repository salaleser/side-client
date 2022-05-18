using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Planet : Entity
    {
        public PlanetItem planetItem;

        private void Start()
        {
            if (planetItem.explorer_id == 0)
            {
                AddButton($"Explore \"{planetItem.title}\"", () => NetworkManager.Instance.PlanetExplore(planetItem.id));
            }
            else
            {
                AddButton($"Zoom in \"{planetItem.title}\"", () => NetworkManager.Instance.Planet(planetItem.id));
            }
        }

        private void OnMouseEnter()
        {
            if (!GameManager.QuickMenuActive && !GameManager.WindowActive && !GameManager.PopupActive)
            {
                NetworkManager.Instance.text.text = $"\n\n{planetItem}";
                GameManager.Instance.Cursor.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
    }
}
