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
        public Button DetachButton;
        public Button AttachButton;
        public GameObject RequiredRoomTypePrefab;
        public GameObject RequiredRoomTypesContent;
        public GameObject RentedRoomPrefab;
        public GameObject RentedRoomsContent;

        private List<RentedRoomItem> _rentedRooms = new();
        private RentedRoomItem _rentedRoom;
        private List<GameObject> _rentedRoomsContent = new();
        private List<RequiredRoomTypeItem> _requiredRoomTypes = new();
        private RequiredRoomTypeItem _requiredRoomType;
        private List<GameObject> _requiredRoomTypesContent = new();

        private void Awake()
        {
            _allowed_position_ids.Add(1);
        }

        private void Start()
        {
            LoadRequiredRoomTypes();
            LoadRentedRooms();
        }

        private void OnEnable()
        {
            gameObject.SetActive(GameManager.Instance.currentOrganization.positions
                .Where(x => _allowed_position_ids.Contains(x.type.id))
                .Where(x => x.citizen.id == GameManager.Instance.me.id)
                .Any());
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.dKey.wasPressedThisFrame)
                {
                    Detach();
                }
                else if (Keyboard.current.aKey.wasPressedThisFrame)
                {
                    Attach();
                }
            }
        }

        public void Attach()
        {
            NetworkManager.Instance.OrganizationAttachRoom(GameManager.Instance.currentOrganization.id, _rentedRoom.id);
        }

        public void Detach()
        {
            NetworkManager.Instance.OrganizationDetachRoom(GameManager.Instance.currentOrganization.id, _requiredRoomType.attached_room.id);
        }

        public void LoadRentedRooms()
        {
            var args = new string[]{GameManager.Instance.me.id.ToString()};
            StartCoroutine(NetworkManager.Instance.Request("rented-rooms", args, (result) => {
                _rentedRooms = JsonUtility.FromJson<RentedRoomsResponse>(result).rented_rooms;
                UpdateRequiredRoomTypesContent();
            }));
        }

        public void LoadRequiredRoomTypes()
        {
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
                _requiredRoomTypes.Add(rrt);
            }
        }

        public void UpdateRentedRoomsContent(int requiredRoomTypeId)
        {
            _rentedRoomsContent.ForEach(x => Destroy(x));
            _rentedRoomsContent = new();

            var rentedRooms = _rentedRooms
                .Where(x => x.type.id == requiredRoomTypeId)
                .ToList();

            var col = 0;
            var row = 0;
            for (var i = 0; i < rentedRooms.Count; i++)
            {
                var rentedRoom = rentedRooms[i];

                var instance = Instantiate(RentedRoomPrefab);
                _rentedRoomsContent.Add(instance);
                var rectTransform = instance.transform.GetComponent<RectTransform>();
                rectTransform.transform.SetParent(RentedRoomsContent.transform.GetComponent<RectTransform>());
                var x = (rectTransform.rect.width * col) + rectTransform.rect.width / 2;
                var y = -(rectTransform.rect.height * row) - rectTransform.rect.height / 2;
                rectTransform.anchoredPosition = new Vector3(x, y, 0);

                var button = instance.GetComponent<Button>();
                button.GetComponentInChildren<TMP_Text>().text = $"{rentedRoom.type.title} {rentedRoom.title}";
                button.onClick.AddListener(() => {
                    _rentedRoom = rentedRoom;
                    _rentedRoomsContent.ForEach(x => x.GetComponent<Image>().color = Color.white);
                    instance.GetComponent<Image>().color = Color.yellow;
                    DetachButton.interactable = _requiredRoomType != null;
                    AttachButton.interactable = _rentedRoom != null;
                });

                row++;
            }
        }

        public void UpdateRequiredRoomTypesContent()
        {
            _requiredRoomTypesContent.ForEach(x => Destroy(x));
            _requiredRoomTypesContent = new();

            var col = 0;
            var row = 0;
            for (var i = 0; i < _requiredRoomTypes.Count; i++)
            {
                var requiredRoomType = _requiredRoomTypes[i];

                var instance = Instantiate(RequiredRoomTypePrefab);
                _requiredRoomTypesContent.Add(instance);
                instance.GetComponent<Image>().color = requiredRoomType.attached_room != null ? Color.green : Color.red;
                var rectTransform = instance.transform.GetComponent<RectTransform>();
                rectTransform.transform.SetParent(RequiredRoomTypesContent.transform.GetComponent<RectTransform>());
                var x = (rectTransform.rect.width * col) + rectTransform.rect.width / 2;
                var y = -(rectTransform.rect.height * row) - rectTransform.rect.height / 2;
                rectTransform.anchoredPosition = new Vector3(x, y, 0);

                var button = instance.GetComponent<Button>();
                button.GetComponentInChildren<TMP_Text>().text = $"{requiredRoomType.title} => {requiredRoomType.attached_room?.ToCaption()}";
                button.onClick.AddListener(() => {
                    _requiredRoomType = requiredRoomType;
                    GameManager.SetDescription(_requiredRoomType.ToString());
                    DetachButton.interactable = _requiredRoomType != null;
                    AttachButton.interactable = _rentedRoom != null;
                    UpdateRentedRoomsContent(_requiredRoomType.room_type_id);
                });

                row++;
            }
        }
    }
}
