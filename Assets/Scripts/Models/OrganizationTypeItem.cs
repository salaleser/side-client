using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class OrganizationTypeItem
    {
        public int id;
        public string title;
        public OrganizationRequirements requirements;
        public OrganizationTypeProperties properties;

        public string ToCaption() => $"{title}";

        public override string ToString() => @$"[organization_type]:
id={id}
title={title}
properties={properties}
requirements.room_types.Count={requirements.room_types.Count}";
    }

    [System.Serializable]
    public class OrganizationRequirements
    {
        public List<RoomTypeItem> room_types;
        public List<int> room_type_ids;
    }

    [System.Serializable]
    public class OrganizationTypeProperties
    {
        public List<Items> in_items;
        public List<Items> out_items;

        public override string ToString() => @$"[properties]:
in_item={ListToString<Items>(in_items)}
out_items={ListToString<Items>(out_items)}";

        private string ListToString<T>(List<T> items)
        {
            if (items.Count == 0)
            {
                return "—";
            }

            var result = "";
            foreach(var item in items)
            {
                result += $"{item},";
            }
            return result.Substring(0, result.Length-2);
        }
    }

    [System.Serializable]
    public class Items
    {
        public ItemTypeItem type;
        public int quantity;

        public override string ToString() => @$"[items]:
quantity={quantity}
item_type={type}";
    }
}
