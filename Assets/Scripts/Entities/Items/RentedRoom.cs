using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class RentedRoom : Entity, IItem
    {
        public RentedRoomItem rentedRoomItem;

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{rentedRoomItem}";
            ShowButtons();
        }
    }
}
