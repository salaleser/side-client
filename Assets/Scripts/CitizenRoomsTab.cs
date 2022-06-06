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

        private List<RoomItem> _rentedRooms = new();

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
            // var args = new string[]{GameManager.Instance.me.id.ToString()};
            // StartCoroutine(NetworkManager.Instance.Request("rented-rooms", args, (result) => {
            //     _rentedRooms = JsonUtility.FromJson<RoomsResponse>(result).rented_rooms;
            //     RentedRooms.AddOptions(_rentedRooms
            //         .Select(x => new TMP_Dropdown.OptionData(x.ToCaption()))
            //         .ToList());
            // }));
        }
    }
}
