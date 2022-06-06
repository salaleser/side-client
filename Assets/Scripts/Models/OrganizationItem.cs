using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class OrganizationItem : Item
    {
        public int salary;
        public List<RoomItem> attached_rooms;
        public OrganizationProperties properties;
        public List<PageItem> pages;
        public int join_type_id;
        public List<MemberItem> members;

        public override string ToString() => @$"[organization]:
id={id}
title={title}
pages.Count={pages.Count}
members.Count={members.Count}
attached_rooms.Count={attached_rooms.Count}";
    }

    [System.Serializable]
    public class OrganizationProperties
    {
        public int membership_fee;
        public int price;
    }
}
