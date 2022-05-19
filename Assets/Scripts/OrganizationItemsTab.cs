using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Entities.Items;
using Entities.Cells;
using Models;
using TMPro;

namespace Side
{
    public class OrganizationItemsTab : MonoBehaviour
    {
        public TMP_Dropdown AttachedRooms;
        public TMP_InputField Price;
        public TMP_InputField Quantity;
        public GameObject ItemPrefab;
        public GameObject Content;
        public TMP_Text Description;
        
        private ItemItem _item;
        private List<GameObject> _items = new();

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
                {
                    AttachedRooms.Hide();
                }

                if (Keyboard.current.rKey.wasPressedThisFrame)
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        AttachedRooms.Show();
                    }
                    else
                    {
                        if (AttachedRooms.value == AttachedRooms.options.Count - 1)
                        {
                            AttachedRooms.value = 0;
                        }
                        else
                        {
                            AttachedRooms.value++;
                        }
                    }
                }
            }
        }

        private void OnEnable()
        {
            UpdateAttachedRooms();
            UpdateItems();
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        public void UpdateItems()
        {
            _items.ForEach(x => Destroy(x));
            _items = new();

            List<ItemItem> items = new();
            if (AttachedRooms.value == 0)
            {
                items = GameManager.Instance.currentOrganization.attached_rooms
                    .SelectMany(x => x.items)
                    .ToList();
            }
            else
            {
                items = GameManager.Instance.currentOrganization.attached_rooms
                    .Where(x => x.ToCaption() == AttachedRooms.captionText.text)
                    .SelectMany(x => x.items)
                    .ToList();
            }

            var col = 0;
            var row = 0;
            for (var i = 0; i < items.Count; i++)
            {
                if (i > 0 && i % 4 == 0)
                {
                    col++;
                    row = 0;
                }

                var item = items[i];

                var instance = Instantiate(ItemPrefab);
                _items.Add(instance);
                instance.GetComponent<Image>().color = item.price == 0 ? Color.white : Color.green;
                var rectTransform = instance.transform.GetComponent<RectTransform>();
                rectTransform.transform.SetParent(Content.transform.GetComponent<RectTransform>());
                var x = (rectTransform.rect.width * col) + rectTransform.rect.width / 2;
                var y = -(rectTransform.rect.height * row) - rectTransform.rect.height / 2;
                rectTransform.anchoredPosition = new Vector3(x, y, 0);

                var button = instance.GetComponent<Button>();
                button.GetComponentInChildren<TMP_Text>().text = $"{item.type.title} x{item.quantity} ={item.price}";
                button.onClick.AddListener(() => {
                    _item = item;
                    Description.text = _item.ToString();
                });

                row++;
            }
        }

        public void UpdateAttachedRooms()
        {
            AttachedRooms.AddOptions(GameManager.Instance.currentOrganization.attached_rooms
                .Select(x => new TMP_Dropdown.OptionData(x.ToCaption())).ToList());
        }

        public void Sell()
        {
            var args = new string[]{_item.id.ToString(), Price.text};
            StartCoroutine(NetworkManager.Instance.Request("item-sell", args, (result) => {
                NetworkManager.Instance.Organization(GameManager.Instance.currentOrganization.id);
            }));
        }

        public void Split()
        {
            var args = new string[]{_item.id.ToString(), Quantity.text};
            StartCoroutine(NetworkManager.Instance.Request("item-split", args, (result) => {
                NetworkManager.Instance.Organization(GameManager.Instance.currentOrganization.id);
            }));
        }

        public void Stack()
        {
            var args = new string[]{_item.id.ToString()};
            StartCoroutine(NetworkManager.Instance.Request("item-stack", args, (result) => {
                NetworkManager.Instance.Organization(GameManager.Instance.currentOrganization.id);
            }));
        }

        public void PublishItems()
        {
            foreach (var room in GameManager.Instance.currentOrganization.attached_rooms)
            {
                if (room.ToCaption() == AttachedRooms.captionText.text)
                {
                    NetworkManager.Instance.OrganizationPagesItemsPublish(GameManager.Instance.currentOrganization.id, room.id);
                    break;
                }
            }
        }
    }
}
