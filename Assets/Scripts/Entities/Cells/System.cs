using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class System : Entity
    {
        public SystemItem systemItem;

        private void Start()
        {
            if (systemItem.planets.Count == 0)
            {
                AddButton($"Explore \"{systemItem.title}\"", () => NetworkManager.Instance.SystemExplore(systemItem.id));
            }
            else
            {
                AddButton($"Zoom in \"{systemItem.title}\"", () => NetworkManager.Instance.System(systemItem.id));
            }
        }
    }
}
