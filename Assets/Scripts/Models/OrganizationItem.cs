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
        public OrganizationTypeItem type;
        public int owner_id;
        public List<PageItem> pages;

        public override string ToString() => @$"Organization:
        ID: {id}
        Title: {title}
        Owner ID: {owner_id}
        Type ID: {type.id}
        Type Title: {type.title}
        Pages Count: {pages.Count}
        Attached Rooms Count: {attached_rooms.Count}
        Required Room Types Count: {type.requirements.room_types.Count}";
    }
}
