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
        public OrganizationProperties properties;

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
    public class OrganizationProperties
    {
        public InOut @in;
        public InOut @out;
        public List<Task> tasks;

        public override string ToString() => @$"Properties:
            In: {@in}
            Out: {@out}
            Tasks: {Tasks()}";

        private string Tasks()
        {
            var result = "";
            foreach(var task in tasks)
            {
                result += $"{task}, ";
            }
            return result.Substring(0, result.Length-2);
        }
    }

    [System.Serializable]
    public class InOut
    {
        public List<int> item_type_ids;
        public int quantity;

        public override string ToString() => @$"{quantity}x{item_type_ids.Count}";
    }

    [System.Serializable]
    public class Task
    {
        public int type_id;
        public int count;

        public override string ToString() => @$"{count}x{type_id}";
    }
}
