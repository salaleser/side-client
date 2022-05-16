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

        private void OnEnable()
        {
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        public void Inventory()
        {
            GameManager.Instance.currentCitizen.items
                .ForEach(x => NetworkManager.Instance.text.text += $"{x}\n");
        }
    }
}
