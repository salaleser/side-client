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
        public TMP_Dropdown rentedRooms;
        public TMP_InputField rentedRoomsCount;

        public void Start()
        {
            UpdateRentedRooms();
        }

        public void UpdateRentedRooms()
        {
            var options = GameManager.Instance.currentCitizen.rented_rooms.Select(x => new TMP_Dropdown.OptionData(x.title)).ToList();
            rentedRooms.AddOptions(options);
            rentedRoomsCount.text = options.Count.ToString();
        }
    }
}
