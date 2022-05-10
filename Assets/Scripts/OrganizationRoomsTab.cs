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
    public class OrganizationRoomsTab : MonoBehaviour
    {
        public TMP_Dropdown requiredRoomTypes;
        public TMP_InputField requiredRoomTypesCount;
        public Button detach;
        public Button attach;

        public void Start()
        {
            UpdateRequiredRoomTypes();
        }

        public void SetDetachButtonInteractable() => detach.interactable = GetIdFromDropdown(requiredRoomTypes) > 0;

        public void Attach()
        {
            NetworkManager.Instance.RentedRooms(GetTypeIdFromDropdown(requiredRoomTypes));
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
                rrt.w = roomType.w;
                rrt.h = roomType.h;
                rrt.organization_id = organization.id;
                foreach (var attachedRoom in organization.attached_rooms.Where(x => x.type_id == roomType.id))
                {
                    rrt.attached_room = attachedRoom;
                }
                rrts.Add(rrt);
            }

            requiredRoomTypes.AddOptions(rrts.Select(x => new TMP_Dropdown.OptionData($"{x.title} ({x.room_type_id}) [{(x.attached_room != null ? x.attached_room.id : "")}]")).ToList());
            requiredRoomTypesCount.text = rrts.Count.ToString();
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
