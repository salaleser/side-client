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
        public bool is_active;
        public List<TaskItem> tasks;
        public List<PageItem> pages;
        public List<MemberItem> members;

        public string ToCaption() => $"{type.title} \"{title}\" [{(is_active ? "O" : "X")}]";

        public override string ToString() => @$"[organization]:
id={id}
title={title}
type={type}
pages.Count={pages.Count}
members.Count={members.Count}
attached_rooms.Count={attached_rooms.Count}";
    }

    [System.Serializable]
    public class OrganizationProperties
    {
        public int membership_fee;
        public int join_type_id;
    }
}
