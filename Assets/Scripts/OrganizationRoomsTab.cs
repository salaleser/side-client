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
    public class OrganizationRoomsTab : MonoBehaviour
    {
        public TMP_Dropdown requiredRoomTypes;
        public Button detach;

        public GameObject rentedRoomPrefab;
        public GameObject rentedRooms;
        public Button attach;
        public TMP_Text Description;

        public void Start()
        {
            UpdateRequiredRoomTypes();
        }

        private void OnEnable()
        {
            UpdateRentedRooms();
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
                {
                    requiredRoomTypes.Hide();
                }

                if (Keyboard.current.tKey.wasPressedThisFrame)
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        requiredRoomTypes.Show();
                    }
                    else
                    {
                        if (requiredRoomTypes.value == requiredRoomTypes.options.Count - 1)
                        {
                            requiredRoomTypes.value = 0;
                        }
                        else
                        {
                            requiredRoomTypes.value++;
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
                else if (Keyboard.current.rKey.wasPressedThisFrame)
                {
                    //
                }
            }
        }

        public void SetDetachButtonInteractable() => detach.interactable = GetIdFromDropdown(requiredRoomTypes) > 0;

        public void Attach()
        {
            NetworkManager.Instance.OrganizationAttachRoom(GameManager.Instance.currentOrganization.id, GameManager.Instance.currentRentedRoom.id);
        }

        public void Detach()
        {
            NetworkManager.Instance.OrganizationDetachRoom(GameManager.Instance.currentOrganization.id, GetIdFromDropdown(requiredRoomTypes));
        }

        public void UpdateRequiredRoomTypes()
        {
            var organization = GameManager.Instance.currentOrganization;

            List<RequiredRoomTypeItem> rrts = new();
            foreach(var roomType in organization.type.requirements.room_types)
            {
                RequiredRoomTypeItem rrt = new();
                rrt.title = roomType.title;
                rrt.room_type_id = roomType.id;
                rrt.organization_id = organization.id;
                foreach (var attachedRoom in organization.attached_rooms.Where(x => x.type.id == roomType.id))
                {
                    rrt.attached_room = attachedRoom;
                }
                rrts.Add(rrt);
            }

            requiredRoomTypes.AddOptions(rrts.Select(x => new TMP_Dropdown.OptionData($"{x.title} ({x.room_type_id}) [{(x.attached_room != null ? x.attached_room.id : "")}]")).ToList());
        }

        public void UpdateRentedRooms()
        {
            var rrs = GameManager.Instance.me.rented_rooms;
            var rrRectTransform = rentedRooms.transform.GetComponent<RectTransform>();

            var col = 0;
            var row = 0;
            for (var i = 0; i < rrs.Count; i++)
            {
                var rentedRoom = rrs[i];

                var instance = Instantiate(rentedRoomPrefab);
                instance.GetComponent<Image>().color = rentedRoom.type.id == GetTypeIdFromDropdown(requiredRoomTypes)
                    ? Color.green
                    : GameManager.Instance.currentOrganization.attached_rooms
                        .Where(x => x.id == rentedRoom.id)
                        .Any() ? Color.blue : Color.grey;
                var rectTransform = instance.transform.GetComponent<RectTransform>();
                rectTransform.transform.SetParent(rrRectTransform);
                var x = (rectTransform.rect.width * col) + rectTransform.rect.width / 2;
                var y = -(rectTransform.rect.height * row) - rectTransform.rect.height / 2;
                rectTransform.anchoredPosition = new Vector3(x, y, 0);

                var button = instance.GetComponent<Button>();
                button.GetComponentInChildren<TMP_Text>().text = $"{rentedRoom.type.title} {rentedRoom.title}";
                button.onClick.AddListener(() => GameManager.Instance.currentRentedRoom = rentedRoom);
                button.onClick.AddListener(() => Description.text = rentedRoom.ToString());

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
