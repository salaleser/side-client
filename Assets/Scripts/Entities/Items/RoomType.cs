using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class RoomType : Entity, IItem
    {
        public RoomTypeItem roomTypeItem;

        private void Start()
        {
            AddButton($"Create \"{roomTypeItem.title}\"", () => NetworkManager.Instance.CreateRoom(GameManager.Instance.floor.id, roomTypeItem.id, GameManager.Instance.cursorX, GameManager.Instance.cursorY, roomTypeItem.w, roomTypeItem.h));
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{roomTypeItem}";
            ShowButtons();
        }
    }
}
