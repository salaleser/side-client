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

        public override string ToString() => @$"Organization Type:
        ID: {id}
        Title: {title}
        {properties}
        Required Room Types Count: {requirements.room_types.Count}";
    }

    [System.Serializable]
    public class OrganizationRequirements
    {
        public List<RequiredRoomTypeItem> room_types;
        public List<int> room_type_ids;
    }

    [System.Serializable]
    public class OrganizationTypeProperties
    {
        public List<Items> in_items;
        public List<Items> out_items;
        public List<Task> tasks;

        public override string ToString() => @$"Properties:
            In Items: {ListToString<Items>(in_items)}
            Out Items: {ListToString<Items>(out_items)}
            Tasks: {ListToString<Task>(tasks)}";

        private string ListToString<T>(List<T> items)
        {
            if (items.Count == 0)
            {
                return "—";
            }

            var result = "";
            foreach(var item in items)
            {
                result += $"\n• {item}, ";
            }
            return result.Substring(0, result.Length-2);
        }
    }

    [System.Serializable]
    public class Items
    {
        public ItemTypeItem type;
        public int quantity;

        public override string ToString() => @$"
        Quantity: {quantity}
        {type}";
    }

    [System.Serializable]
    public class Task
    {
        public int type_id;
        public int count;

        public override string ToString() => @$"Task:
        Count: {count}
        Type ID: {type_id}";
    }
}
