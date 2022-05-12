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
        public CitizenItem citizen;
        public TMP_InputField citizenName;
        public TMP_Dropdown friends;
        public TMP_InputField friendsCount;
        public Button kick;

        public void Awake()
        {
            citizen = GameManager.Instance.currentCitizen;
        }

        public void Start()
        {
            citizenName.text = citizen.name;
            friends.AddOptions(citizen.friends.Select(x => new TMP_Dropdown.OptionData(x.id.ToString())).ToList());
            friendsCount.text = citizen.friends.Count.ToString();
        }

        public void SetKickButtonInteractable(bool interactable) => kick.interactable = interactable;

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
