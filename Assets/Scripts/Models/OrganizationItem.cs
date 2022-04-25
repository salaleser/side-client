using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class OrganizationItem
    {
        public int id;
        public string title;
        public List<RentedRoomItem> attached_rooms;
        public int type_id;
        public OrganizationTypeItem type;
        public int owner_id;
        public bool is_active;

        public override string ToString() => @$"Organization:
        ID: {id}
        Title: {title}
        Is Active: {is_active}
        Attached Rooms Count: {attached_rooms.Count}";
    }
}
