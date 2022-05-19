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
    public class CitizenRoomsTab : MonoBehaviour
    {
        public TMP_Dropdown RentedRooms;

        public void Start()
        {
            UpdateRentedRooms();
        }

        private void OnEnable()
        {
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        public void UpdateRentedRooms()
        {
            RentedRooms.AddOptions(GameManager.Instance.me.rented_rooms
                .Select(x => new TMP_Dropdown.OptionData(x.title)).ToList());
        }
    }
}
