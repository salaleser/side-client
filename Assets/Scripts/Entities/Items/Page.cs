using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class Page : Entity, IItem
    {
        public PageItem pageItem;

        private void Start()
        {
            AddButton($"Load \"{pageItem.path}\"", () => NetworkManager.Instance.LoadPage(pageItem));
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{pageItem}";
            ShowButtons();
        }
    }
}
