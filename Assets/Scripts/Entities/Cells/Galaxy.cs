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
            if (galaxyItem.systems.Count == 0)
            {
                AddButton($"Explore \"{galaxyItem.title}\"", () => NetworkManager.Instance.GalaxyExplore(galaxyItem.id));
            }
            else
            {
                AddButton($"Zoom in \"{galaxyItem.title}\"", () => NetworkManager.Instance.Galaxy(galaxyItem.id));
            }
        }
    }
}
