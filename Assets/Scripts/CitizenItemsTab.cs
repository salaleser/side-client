using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Entities.Items;
using Entities.Cells;
using Models;
using TMPro;

namespace Side
{
    public class CitizenItemsTab : MonoBehaviour
    {
        public void Start()
        {
            Inventory();
        }

        public void Inventory()
        {
            var items = GameManager.Instance.currentCitizen.items;
            NetworkManager.Instance.InstantiateInventory(items);
        }
    }
}
