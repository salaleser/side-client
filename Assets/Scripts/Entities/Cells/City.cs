using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class City : Entity
    {
        public CityItem cityItem;

        private void Start()
        {
            if (cityItem.explorer_id == 0)
            {
                AddButton($"Explore City", () => NetworkManager.Instance.CityExplore(cityItem.id));
            }
            else
            {
                AddButton($"Zoom in \"{cityItem.title}\"", () => NetworkManager.Instance.City(cityItem.id));
            }
        }

        private void OnMouseEnter()
        {
            NetworkManager.Instance.text.text = $"\n\n{cityItem}";
        }
    }
}
