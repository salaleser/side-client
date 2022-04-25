using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class Position : Entity, IItem
    {
        public PositionItem positionItem;

        private void Start()
        {
            AddButton($"Request \"{positionItem.title}\"", () => NetworkManager.Instance.PositionRequest(GameManager.Instance.citizen.id, positionItem.id));
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{positionItem}";
            ShowButtons();
        }
    }
}
