using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RoomItem : Item
    {
        public int id;
        public int type_id;
        public string description = "empty";
        public int x;
        public int y;
        public int w;
        public int h;
        public int renter_id;
        public int item_id;
        public int floor_id;
        public bool rentable;
        public List<int> organization_ids = new();
        public float r;
        public float g;
        public float b;
        public float a;

        public List<ItemItem> items = new();
        public List<CitizenItem> citizens = new();
        public List<MessageItem> messages = new();

        public string ToCaption() => $"{title} ({id})";

        public override string ToString() => @$"Room:
        ID: {id}
        Type ID: {type_id}
        Description: {description}
        Renter ID: {renter_id}
        Rentable: {rentable}
        Organization IDs Count: {organization_ids.Count}
        Title: {title}
        Items Count: {items.Count}
        Citizens Count: {citizens.Count}
        Messages Count: {messages.Count}";
    }
}
