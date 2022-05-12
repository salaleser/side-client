using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Entities.Items;
using Entities.Cells;
using Models;
using TMPro;

namespace Side
{
    public class OrganizationItemsTab : MonoBehaviour
    {
        public TMP_Dropdown attachedRooms;
        public TMP_InputField price;
        public TMP_InputField quantity;

        public void Start()
        {
            UpdateAttachedRooms();
        }

        public void Inventory()
        {
            var items = GameManager.Instance.currentOrganization.attached_rooms
                .Where(x => x.title == attachedRooms.captionText.text)
                .SelectMany(x => x.items)
                .ToList();
            NetworkManager.Instance.InstantiateInventory(items);
        }

        public void UpdateAttachedRooms()
        {
            var organization = GameManager.Instance.currentOrganization;
            var options = organization.attached_rooms.Select(x => new TMP_Dropdown.OptionData(x.title)).ToList();
            attachedRooms.AddOptions(options);
        }

        public void Sell()
        {
            NetworkManager.Instance.ItemSell(GameManager.Instance.currentItem.id, int.Parse(price.text));
        }

        public void Split()
        {
            NetworkManager.Instance.ItemSplit(GameManager.Instance.currentItem.id, int.Parse(quantity.text));
        }

        public void Stack()
        {
            NetworkManager.Instance.ItemStack(GameManager.Instance.currentItem.id);
        }
    }
}
