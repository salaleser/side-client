using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class CitizenWindow : Window
    {
        public TMP_InputField citizenName;
        public TMP_Dropdown deliveryAddress;
        public TMP_InputField deliveryAddressId;
        public TMP_Dropdown friends;
        public TMP_InputField friendsCount;
        public Button kick;

        public void Awake()
        {
            GameManager.SetWindowActive(true);
        }

        public void Start()
        {
            citizenName.text = GameManager.Instance.me.title;

            UpdateFriends();
            UpdateDeliveryAddress();
        }

        private void UpdateFriends()
        {
            friends.AddOptions(GameManager.Instance.me.friends.Select(x => new TMP_Dropdown.OptionData(x.id.ToString())).ToList());
            friendsCount.text = GameManager.Instance.me.friends.Count.ToString();
        }

        private void UpdateDeliveryAddress()
        {
            deliveryAddress.AddOptions(GameManager.Instance.me.rented_rooms.Where(x => x.type.id == 4).Select(x => new TMP_Dropdown.OptionData(x.title)).ToList());
            deliveryAddressId.text = GameManager.Instance.me.delivery_address.ToString();
        }

        public void SetKickButtonInteractable(bool interactable) => kick.interactable = interactable;
        
        public void SetDeliveryAddress()
        {
            foreach (var rentedRoom in GameManager.Instance.me.rented_rooms)
            {
                if (rentedRoom.title == deliveryAddress.captionText.text)
                {
                    GameManager.Instance.me.delivery_address = rentedRoom.id;
                    deliveryAddressId.text = rentedRoom.id.ToString();
                    var args = new string[]{GameManager.Instance.me.id.ToString(), rentedRoom.id.ToString()};
		            StartCoroutine(NetworkManager.Instance.Request("citizen-delivery-address", args, (result) => {}));
                    break;
                }
            }
        }

        public void Kick()
        {
            int friendId;
            if (int.TryParse(friends.captionText.text, out friendId))
            {
                // NetworkManager.Instance.FriendDelete(citizen.id, friendId);
            }
        }
    }
}
