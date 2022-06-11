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
    public class CitizenItemsTab : MonoBehaviour
    {
        public TMP_Dropdown Inventories;
        public TMP_InputField Quantity;
        public GameObject ItemPrefab;
        public GameObject Content;
        public Button SplitButton;
        public Button StackButton;
        public Button DropButton;
        public Button TakeButton;
        
        private ItemItem _item;
        private List<GameObject> _items = new();

        private void Start()
        {
            List<TMP_Dropdown.OptionData> options = new();
            options.Add(new TMP_Dropdown.OptionData($"{GameManager.Instance.me.ToCaption()}"));
            options.Add(new TMP_Dropdown.OptionData($"{GameManager.Instance.me.room.type.title} {GameManager.Instance.me.room.title}"));
            Inventories.AddOptions(options);
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
                {
                    Inventories.Hide();
                }

                if (Keyboard.current.rKey.wasPressedThisFrame)
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        Inventories.Show();
                    }
                    else
                    {
                        if (Inventories.value == Inventories.options.Count - 1)
                        {
                            Inventories.value = 0;
                        }
                        else
                        {
                            Inventories.value++;
                        }
                    }
                }
                else if (Keyboard.current.sKey.wasPressedThisFrame)
                {
                    if (SplitButton.interactable)
                    {
                        Split();
                    }
                }
                else if (Keyboard.current.kKey.wasPressedThisFrame)
                {
                    if (StackButton.interactable)
                    {
                        Stack();
                    }
                }
                else if (Keyboard.current.dKey.wasPressedThisFrame)
                {
                    if (DropButton.interactable)
                    {
                        Drop();
                    }
                }
                else if (Keyboard.current.gKey.wasPressedThisFrame)
                {
                    if (TakeButton.interactable)
                    {
                        Take();
                    }
                }
            }
        }

        private void OnEnable()
        {
            UpdateItems();
            UpdateButtons();
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        public void UpdateButtons()
        {
            SplitButton.interactable = _item != null && Quantity.text != "";
            StackButton.interactable = _item != null;
            DropButton.interactable = _item != null && Inventories.value == 0;
            TakeButton.interactable = _item != null && Inventories.value == 1;
        }

        public void UpdateItems()
        {
            _items.ForEach(x => Destroy(x));
            _items = new();

            List<ItemItem> items = new();
            if (Inventories.value == 0)
            {
                items = GameManager.Instance.me.items.ToList();
            }
            else if (Inventories.value == 1)
            {
                items = GameManager.Instance.me.room.items.ToList();
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
                // instance.GetComponent<Image>().color = item.price == 0 ? Color.white : Color.green;
                var rectTransform = instance.transform.GetComponent<RectTransform>();
                rectTransform.transform.SetParent(Content.transform.GetComponent<RectTransform>());
                var x = (rectTransform.rect.width * col) + rectTransform.rect.width / 2;
                var y = -(rectTransform.rect.height * row) - rectTransform.rect.height / 2;
                rectTransform.anchoredPosition = new Vector3(x, y, 0);

                var button = instance.GetComponent<Button>();
                button.GetComponentInChildren<TMP_Text>().text = $"{item.type.title} x{item.quantity} ={item.price}";
                button.onClick.AddListener(() => {
                    _item = item;
                    GameManager.SetDescription(_item.ToString());
                    UpdateButtons();
                });

                row++;
            }
        }

        public void Split()
        {
            // var args = new string[]{_item.id.ToString(), Quantity.text};
            // StartCoroutine(NetworkManager.Instance.Request("item-split", args, (json) => {
            //     NetworkManager.Instance.Citizen(GameManager.Instance.me.id, "Items");
            // }));
        }

        public void Stack()
        {
            // var args = new string[]{_item.id.ToString()};
            // StartCoroutine(NetworkManager.Instance.Request("item-stack", args, (json) => {
            //     NetworkManager.Instance.Citizen(GameManager.Instance.me.id, "Items");
            // }));
        }

        public void Drop()
        {
            // var args = new string[]{_item.id.ToString(), GameManager.Instance.me.room.item_id.ToString()};
            // StartCoroutine(NetworkManager.Instance.Request("item-drop", args, (json) => {
            //     NetworkManager.Instance.Citizen(GameManager.Instance.me.id, "Items");
            // }));
        }

        public void Take()
        {
            // var args = new string[]{_item.id.ToString(), GameManager.Instance.me.item_id.ToString()};
            // StartCoroutine(NetworkManager.Instance.Request("item-take", args, (json) => {
            //     NetworkManager.Instance.Citizen(GameManager.Instance.me.id, "Items");
            // }));
        }
    }
}
