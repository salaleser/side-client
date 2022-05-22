using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class CreateRoomPopup : MonoBehaviour
    {
        public TMP_InputField X;
        public TMP_InputField Y;
        public TMP_InputField Z;
        public TMP_InputField Title;
        public TMP_Dropdown RoomTypes;
        public TMP_Dropdown ConstructionOrganizations;
        public TMP_Text Description;

        private List<RoomTypeItem> _roomTypes;
        private List<OrganizationItem> _constructionOrganizations;

        private void Start()
        {
            X.text = GameManager.Instance.Cursor.transform.position.x.ToString();
            Y.text = GameManager.Instance.Cursor.transform.position.z.ToString();
            UpdateRoomTypes();
            UpdateConstructionOrganizations();
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
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
                    if (RoomTypes.value == RoomTypes.options.Count - 1)
                    {
                        RoomTypes.value = 0;
                    }
                    else
                    {
                        RoomTypes.value++;
                    }
                }
                else if (Keyboard.current.bKey.wasPressedThisFrame)
                {
                    if (ConstructionOrganizations.value == ConstructionOrganizations.options.Count - 1)
                    {
                        ConstructionOrganizations.value = 0;
                    }
                    else
                    {
                        ConstructionOrganizations.value++;
                    }
                }
            }
        }

        public void UpdateRoomTypeDescription()
        {
            Description.text = _roomTypes
                .Where(x => x.ToCaption() == RoomTypes.captionText.text)
                .FirstOrDefault()?
                .ToString();
        }

        public void UpdateConstructionOrganizationDescription()
        {
            Description.text = _constructionOrganizations
                .Where(x => x.ToCaption() == ConstructionOrganizations.captionText.text)
                .FirstOrDefault()?
                .ToString();
        }

        private void UpdateRoomTypes()
        {
            var args = new string[]{};
            StartCoroutine(NetworkManager.Instance.Request("room-types", args, (result) => {
                _roomTypes = JsonUtility.FromJson<RoomTypesResponse>(result).room_types;
                RoomTypes.AddOptions(_roomTypes
                    .Where(x => x.id != 14) // нельзя строить "construction site"
                    .Select(x => new TMP_Dropdown.OptionData(x.ToCaption()))
                    .ToList());
            }));
        }

        private void UpdateConstructionOrganizations()
        {
            var args = new string[]{11.ToString()};
            StartCoroutine(NetworkManager.Instance.Request("organizations", args, (result) => {
                _constructionOrganizations = JsonUtility.FromJson<OrganizationsResponse>(result).organizations;
                ConstructionOrganizations.AddOptions(_constructionOrganizations
                    .Select(x => new TMP_Dropdown.OptionData(x.ToCaption()))
                    .ToList());
            }));
        }

        public void Accept()
        {
            var roomType = _roomTypes
                .Where(x => x.ToCaption() == RoomTypes.captionText.text)
                .FirstOrDefault();
            if (roomType == null)
            {
                NetworkManager.Instance.InstantiateNoticePopup("ERROR", "Не указан тип комнаты");
                return;
            }

            var constructionOrganization = _constructionOrganizations
                .Where(x => x.ToCaption() == ConstructionOrganizations.captionText.text)
                .FirstOrDefault();
            if (constructionOrganization == null)
            {
                NetworkManager.Instance.InstantiateNoticePopup("ERROR", "Не указана строительная организация");
                return;
            }

            var size = 64;
            if ((roomType.properties.w + (int.Parse(X.text) - 1) <= size)
                && (roomType.properties.h + (size - int.Parse(Y.text)) <= size))
            {
                NetworkManager.Instance.CreateRoom(GameManager.Instance.currentParcel.id,
                    roomType.id, int.Parse(X.text), int.Parse(Y.text), int.Parse(Z.text),
                    roomType.properties.w, roomType.properties.h,
                    constructionOrganization.id, GameManager.Instance.me.id, Title.text);
                Destroy(this.gameObject);
            }
            else
            {
                NetworkManager.Instance.InstantiateNoticePopup("ERROR", @$"Unable to create
roomType.properties.w={roomType.properties.w}
roomType.properties.h={roomType.properties.h}
({roomType.properties.w} + {(int.Parse(X.text) - 1)} <= {size}) = {(roomType.properties.w + (int.Parse(X.text) - 1) <= size)}
({roomType.properties.h} + {(size - int.Parse(Y.text))} <= {size}) = {(roomType.properties.h + (size - int.Parse(Y.text)) <= size)}
");
            }
        }

        public void Decline()
        {
            Destroy(this.gameObject);
        }
    }
}
