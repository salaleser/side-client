using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class CitizenItem : Item
    {
        public int account_id;
        public int parcel_id;
        public int x;
        public int y;
        public int z;
        public int item_id;
        public RoomItem workplace;
        public RoomItem delivery_address;
        public List<ItemItem> items;
        public List<FriendItem> friends;
        public List<ActionItem> actions;
        public List<StatusItem> statuses;
        public List<CharacteristicItem> characteristics;
        public List<OrganizationItem> organizations;
        public List<EmailItem> emails;
        public RoomItem room;

        public override string ToString() => @$"[citizen]:
id={id}
title={title}
x={x}
y={y}
z={z}
workplace={workplace}
delivery_address={delivery_address}";
    }

    [System.Serializable]
    public class StatusItem
    {
        public string type_id;
        public string type_title;
        public int value;

        public override string ToString() => @$"{type_title}={value}";
    }

    [System.Serializable]
    public class CharacteristicItem
    {
        public string type_id;
        public string type_title;
        public string value;
        public bool is_public;

        public override string ToString() => @$"{type_title}={value}";
    }
}
