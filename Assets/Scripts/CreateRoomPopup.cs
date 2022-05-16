using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class CreateRoomPopup : Popup
    {
        public TMP_InputField x;
        public TMP_InputField y;
        public TMP_InputField z;
        public TMP_Dropdown roomTypes;
        public TMP_InputField title;
        public TMP_Text description;

        private List<RoomTypeItem> _roomTypes;

        private void Start()
        {
            x.text = GameManager.Instance.cursorX.ToString();
            y.text = GameManager.Instance.cursorY.ToString();
            z.text = GameManager.Instance.cursorZ.ToString();
            UpdateRoomTypes();
        }

        private void Update()
        {
            if (GameManager.IsShortcutsActive)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    Accept();
                }
                else if (Keyboard.current.escapeKey.wasPressedThisFrame)
                {
                    Decline();
                }
                else if (Keyboard.current.tKey.wasPressedThisFrame)
                {
                    if (roomTypes.value == roomTypes.options.Count - 1)
                    {
                        roomTypes.value = 0;
                    }
                    else
                    {
                        roomTypes.value++;
                    }
                }
            }
        }

        public void UpdateRoomTypeDescription()
        {
            description.text = _roomTypes
                .Where(x => x.ToCaption() == roomTypes.captionText.text)
                .FirstOrDefault()?
                .ToString();
        }

        public void UpdateRoomTypes()
        {
            StartCoroutine(NetworkManager.Instance.Request("room-types", "", (result) => {
                _roomTypes = JsonUtility.FromJson<RoomTypesResponse>(result).room_types;
                roomTypes.AddOptions(_roomTypes
                    .Where(x => x.id != 14) // нельзя строить "construction site"
                    .Select(x => new TMP_Dropdown.OptionData(x.ToCaption()))
                    .ToList());
            }));
        }

        public void Accept()
        {
            var roomType = _roomTypes
                .Where(x => x.ToCaption() == roomTypes.captionText.text)
                .FirstOrDefault();
            if (roomType == null)
            {
                return;
            }

            var size = 64;
            if ((roomType.properties.size.w + (int.Parse(x.text) - 1) <= size) && (roomType.properties.size.h + (size - int.Parse(y.text)) <= size))
            {
                NetworkManager.Instance.CreateRoom(GameManager.Instance.currentParcel.id,
                    roomType.id, int.Parse(x.text), int.Parse(y.text), int.Parse(z.text), roomType.properties.size.w, roomType.properties.size.h,
                    GameManager.Instance.me.id, title.text);
                Destroy(this.gameObject);
            }
            else
            {
                NetworkManager.Instance.InstantiateNoticePopup("ERROR", @$"Unable to create");
            }
        }

        public void Decline()
        {
            Destroy(this.gameObject);
        }
    }
}
