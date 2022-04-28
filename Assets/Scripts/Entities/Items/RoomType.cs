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
            var x = GameManager.Instance.cursorX;
            var y = GameManager.Instance.cursorY;
            var floor = GameManager.Instance.floor;
            var w = NetworkManager.Width * floor.w;
            var h = NetworkManager.Height * floor.h;
            if (roomTypeItem.w + (x - 1) <= w &&
                roomTypeItem.h + (h - y) <= h)
            {
                AddButton($"Create \"{roomTypeItem.title}\"", () => NetworkManager.Instance.CreateRoom(floor.id, roomTypeItem.id, x, y, roomTypeItem.w, roomTypeItem.h));
            }
            else
            {
                AddButton($"Unable to Create \"{roomTypeItem.title}\"", () => NetworkManager.Instance.CreateRoom(floor.id, roomTypeItem.id, x, y, roomTypeItem.w, roomTypeItem.h), false);
            }
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{roomTypeItem}";
            ShowButtons();
        }
    }
}
