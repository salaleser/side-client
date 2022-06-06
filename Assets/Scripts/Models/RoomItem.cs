using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RoomItem : Item
    {
        public RoomTypeItem type;
        public int x;
        public int y;
        public int z;
        public int w;
        public int h;
        public int health;
        public int creator_id;
        public int renter_id;
        public int item_id;
        public int parcel_id;
        public bool rentable;
        public int organization_id;
        public List<ItemItem> items = new();
        public List<CitizenSimpleItem> citizens = new();
        public List<MessageItem> messages = new();
        public RoomProperties properties;

        public override string ToString() => @$"[room]:
id={id}
title={title}
x={x}
y={y}
z={z}
w={w}
h={h}
type={type}
health={health}
renter_id={renter_id}
creator_id={creator_id}
rentable={rentable}
organization_id={organization_id}
items.Count={items.Count}
citizens.Count={citizens.Count}
messages.Count={messages.Count}";
    }

    [System.Serializable]
    public class RoomProperties
    {
        public int type_id;
        public Door door;
    }

    [System.Serializable]
    public class Door
    {
        public string password;
    }
}
