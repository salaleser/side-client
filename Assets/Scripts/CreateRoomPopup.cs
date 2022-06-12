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
        public TMP_InputField Price;
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
                else if (Keyboard.current.rKey.wasPressedThisFrame)
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
                else if (Keyboard.current.oKey.wasPressedThisFrame)
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
            StartCoroutine(NetworkManager.Instance.Request("room-types", args, (json) => {
                _roomTypes = JsonUtility.FromJson<RoomTypesResponse>(json).room_types;
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

        public void UpdatePrice()
        {
            var roomType = _roomTypes
                .Where(x => x.ToCaption() == RoomTypes.captionText.text)
                .FirstOrDefault();
            
            var constructionOrganization = _constructionOrganizations
                .Where(x => x.ToCaption() == ConstructionOrganizations.captionText.text)
                .FirstOrDefault();
            
            Price.text = (roomType?.properties.durability * constructionOrganization?.properties.price).ToString();
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

            const int Size = 64;
            if ((roomType.properties.w + (int.Parse(X.text) - 1) <= Size)
                && (roomType.properties.h + (Size - int.Parse(Y.text)) <= Size))
            {
                NetworkManager.Instance.CreateRoom(GameManager.Instance.Parcel.id,
                    roomType.id, int.Parse(X.text), int.Parse(Y.text), int.Parse(Z.text),
                    roomType.properties.w, roomType.properties.h,
                    constructionOrganization.id, GameManager.Instance.Citizen.id, Title.text);
                Destroy(gameObject);
            }
            else
            {
                NetworkManager.Instance.InstantiateNoticePopup("ERROR", "Not enough space");
            }
        }

        public void Decline()
        {
            Destroy(gameObject);
        }
    }
}
