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
            AddButton($"Zoom in \"{cityItem.title}\"", () => NetworkManager.Instance.City(cityItem.id));
            AddButton($"Found City", () => NetworkManager.Instance.CreateCity(cityItem.region_id, cityItem.x, cityItem.y, ""));
        }
    }
}
