using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        }
    }
}
