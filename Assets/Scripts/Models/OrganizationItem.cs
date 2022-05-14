using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class OrganizationItem
    {
        public int id;
        public string title;
        public List<RoomItem> attached_rooms;
        public OrganizationTypeItem type;
        public OrganizationProperties properties;
        public int owner_id;
        public bool is_active;
        public List<PageItem> pages;
        public List<MemberItem> members;

        public string ToCaption() => $"{type.title} \"{title}\" [{(is_active ? "O" : "X")}]";

        public override string ToString() => @$"Organization:
        ID: {id}
        Title: {title}
        Owner ID: {owner_id}
        Type ID: {type.id}
        Type Title: {type.title}
        Pages Count: {pages.Count}
        Members Count: {members.Count}
        Attached Rooms Count: {attached_rooms.Count}
        Required Room Types Count: {type.requirements.room_types.Count}";
    }

    [System.Serializable]
    public class OrganizationProperties
    {
        public int membership_fee;
        public int join_type_id;
    }
}
