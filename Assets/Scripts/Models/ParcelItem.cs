using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ParcelItem : Item
    {
        public int x;
        public int y;
        public int explorer_id;
        public int region_id;
        public List<RoomItem> rooms;
        public List<CitizenItem> citizens;
        public int rooms_count;
        public ParcelProperties properties;

        public override string ToString() => @$"[parcel]:
id={id}
title={title}
region_id={region_id}
explorer_id={explorer_id}
properties={properties}
rooms_count={rooms_count}";
    }

    [System.Serializable]
    public class ParcelProperties
    {
        public List<Resource> resources;

        public override string ToString() => @$"[properties]:
resources={Resources()}";

        private string Resources()
        {
            var result = "";
            foreach (var resource in resources)
            {
                result += $"{resource},";
            }
            return result;
        }
    }

    [System.Serializable]
    public class Resource
    {
        public ItemTypeItem type;
        public int quantity;

        public override string ToString() => @$"[resource]:
item_type={type}
quantity={quantity}";
    }
}
