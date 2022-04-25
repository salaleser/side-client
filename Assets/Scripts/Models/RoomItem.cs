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
        public bool rentable;
        public int organization_id;
        public float r;
        public float g;
        public float b;
        public float a;

        public List<ItemItem> items = new();
        public List<CitizenItem> citizens = new();
        public List<MessageItem> messages = new();

        public override string ToString() => @$"Room:
        ID: {id}
        Type ID: {type_id}
        Description: {description}
        Title: {title}
        Items Count: {items.Count}
        Citizens Count: {citizens.Count}
        Messages Count: {messages.Count}";
    }
}
