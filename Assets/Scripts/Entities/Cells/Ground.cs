using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Ground : Entity
    {
        public GroundItem groundItem;

        private void Start()
        {
            AddButton($"Create Room", () => NetworkManager.Instance.InstantiateCreateRoomPopup());
            AddButton($"2", () => Debug.Log(2));
            AddButton($"3", () => Debug.Log(3));
            AddButton($"4", () => Debug.Log(4));
            QuickButtons.Add(null);
            AddButton($"6", () => Debug.Log(6));
            AddButton($"7", () => Debug.Log(7));
            AddButton($"8", () => Debug.Log(8));
            AddButton($"9", () => Debug.Log(9));
        }

        private void OnMouseEnter()
        {
            NetworkManager.Instance.text.text = $"\n\n{groundItem}";
        }
    }
}
