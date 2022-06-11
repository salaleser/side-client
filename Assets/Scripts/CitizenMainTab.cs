using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class CitizenMainTab : MonoBehaviour
    {
        public TMP_InputField CitizenName;
        public TMP_Dropdown DeliveryAddress;
        public TMP_InputField DeliveryAddressId;

        private List<RoomItem> _rentedRooms = new();

        public void Start()
        {
            CitizenName.text = GameManager.Instance.Me.title;
            UpdateDeliveryAddress();
        }

        private void UpdateDeliveryAddress()
        {
            DeliveryAddress.AddOptions(_rentedRooms
                .Where(x => x.type.id == 4)
                .Select(x => new TMP_Dropdown.OptionData(x.ToCaption()))
                .ToList());
            DeliveryAddressId.text = GameManager.Instance.Me.delivery_address.ToCaption();
        }

        public void SetProperties()
        {
            // var rentedRoom = _rentedRooms
            //     .Where(x => x.ToCaption() == DeliveryAddress.captionText.text)
            //     .FirstOrDefault();
            // GameManager.Instance.Me.delivery_address = rentedRoom;
            // DeliveryAddressId.text = rentedRoom.ToCaption();
            // var args = new string[]{GameManager.Instance.Me.id.ToString(), rentedRoom.id.ToString()};
            // StartCoroutine(NetworkManager.Instance.Request("citizen-delivery-address", args, (json) => {
            //     NetworkManager.Instance.ProcessCitizen(json, "Main");
            // }));
        }
    }
}
