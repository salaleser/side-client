﻿using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RoomItem : Item
    {
        public int id;
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
        public List<int> organization_ids = new();

        public List<ItemItem> items = new();
        public List<CitizenItem> citizens = new();
        public List<MessageItem> messages = new();
        public RoomProperties properties;

        public string ToCaption() => $"{title} ({id})";

        public override string ToString() => @$"[room]:
id={id}
title={title}
type={type}
health={health}
renter_id={renter_id}
creator_id={creator_id}
rentable={rentable}
organization_ids.Count={organization_ids.Count}
items.Count={items.Count}
citizens.Count={citizens.Count}
messages.Count={messages.Count}";
    }

    [System.Serializable]
    public class RoomProperties
    {
        public int type_id;
    }
}
