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
    public class OrganizationRoomsTab : OrganizationTab
    {
        public TMP_Dropdown RequiredRoomTypes;
        public Button DetachButton;

        public GameObject RentedRoomPrefab;
        public GameObject RentedRooms;

        private TMP_Text _description;

        private void Awake()
        {
            _allowed_position_ids.Add(1);
        }

        private void OnEnable()
        {
            gameObject.SetActive(GameManager.Instance.currentOrganization.positions
                .Where(x => _allowed_position_ids.Contains(x.type.id))
                .Where(x => x.citizen.id == GameManager.Instance.me.id)
                .Any());
            UpdateRentedRooms();
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        public void Start()
        {
            _description = GameObject.Find("MainDescription").GetComponent<TMP_Text>();
            UpdateRequiredRoomTypes();
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
                {
                    RequiredRoomTypes.Hide();
                }

                if (Keyboard.current.tKey.wasPressedThisFrame)
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        RequiredRoomTypes.Show();
                    }
                    else
                    {
                        if (RequiredRoomTypes.value == RequiredRoomTypes.options.Count - 1)
                        {
                            RequiredRoomTypes.value = 0;
                        }
                        else
                        {
                            RequiredRoomTypes.value++;
                        }
                    }
                }
                else if (Keyboard.current.dKey.wasPressedThisFrame)
                {
                    Detach();
                }
                else if (Keyboard.current.aKey.wasPressedThisFrame)
                {
                    Attach();
                }
            }
        }

        public void SetDetachButtonInteractable() => DetachButton.interactable = GetIdFromDropdown(RequiredRoomTypes) > 0;

        public void Attach()
        {
            NetworkManager.Instance.OrganizationAttachRoom(GameManager.Instance.currentOrganization.id, GameManager.Instance.currentRentedRoom.id);
        }

        public void Detach()
        {
            NetworkManager.Instance.OrganizationDetachRoom(GameManager.Instance.currentOrganization.id, GetIdFromDropdown(RequiredRoomTypes));
        }

        public void UpdateRequiredRoomTypes()
        {
            List<RequiredRoomTypeItem> rrts = new();
            foreach(var roomType in GameManager.Instance.currentOrganization.type.requirements.room_types)
            {
                RequiredRoomTypeItem rrt = new();
                rrt.title = roomType.title;
                rrt.room_type_id = roomType.id;
                rrt.organization_id = GameManager.Instance.currentOrganization.id;
                foreach (var attachedRoom in GameManager.Instance.currentOrganization.attached_rooms.Where(x => x.type.id == roomType.id))
                {
                    rrt.attached_room = attachedRoom;
                }
                rrts.Add(rrt);
            }

            RequiredRoomTypes.AddOptions(rrts.Select(x => new TMP_Dropdown.OptionData($"{x.title} ({x.room_type_id}) [{(x.attached_room != null ? x.attached_room.id : "")}]")).ToList());
        }

        public void UpdateRentedRooms()
        {
            var col = 0;
            var row = 0;
            for (var i = 0; i < GameManager.Instance.me.rented_rooms.Count; i++)
            {
                var rentedRoom = GameManager.Instance.me.rented_rooms[i];

                var instance = Instantiate(RentedRoomPrefab);
                instance.GetComponent<Image>().color = rentedRoom.type.id == GetTypeIdFromDropdown(RequiredRoomTypes)
                    ? Color.green
                    : GameManager.Instance.currentOrganization.attached_rooms
                        .Where(x => x.id == rentedRoom.id)
                        .Any() ? Color.blue : Color.grey;
                var rectTransform = instance.transform.GetComponent<RectTransform>();
                rectTransform.transform.SetParent(RentedRooms.transform.GetComponent<RectTransform>());
                var x = (rectTransform.rect.width * col) + rectTransform.rect.width / 2;
                var y = -(rectTransform.rect.height * row) - rectTransform.rect.height / 2;
                rectTransform.anchoredPosition = new Vector3(x, y, 0);

                var button = instance.GetComponent<Button>();
                button.GetComponentInChildren<TMP_Text>().text = $"{rentedRoom.type.title} {rentedRoom.title}";
                button.onClick.AddListener(() => {
                    GameManager.Instance.currentRentedRoom = rentedRoom;
                    _description.text = rentedRoom.ToString();
                });

                row++;
            }
        }

        private int GetIdFromDropdown(TMP_Dropdown dropdown)
        {
            int id;
            
            var a1 = dropdown.captionText.text.Split("[");
            if (a1.Length < 2)
            {
                return -1;
            }

            var s2 = a1[1].Split("]")[0];
            if (!int.TryParse(s2, out id))
            {
                return -2;
            }

            return id;
        }

        private int GetTypeIdFromDropdown(TMP_Dropdown dropdown)
        {
            int id;
            
            var a1 = dropdown.captionText.text.Split("(");
            if (a1.Length < 2)
            {
                return -1;
            }

            var s2 = a1[1].Split(")")[0];
            if (!int.TryParse(s2, out id))
            {
                return -2;
            }

            return id;
        }
    }
}
