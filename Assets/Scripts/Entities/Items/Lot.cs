using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class Lot : Entity, IItem
    {
        public LotItem lotItem;

        private void Start()
        {
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{lotItem}";
            ShowButtons();
        }
    }
}
