using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class CitizenItem
    {
        public int id;
        public string name;
        public int account_id;
        public int active_task_id;
        public int organization_id;
        public int room_id;
        public int storage_root_item_id;
        public int parcel_id;
        public int floor_id;
        public int z;
        public int root_item_id;
        public List<ItemItem> items;
        public List<FriendItem> friends;
        public List<ActionItem> actions;
        public List<StatusItem> statuses;
        public List<CharacteristicItem> characteristics;
        public List<OrganizationItem> organizations;
        public List<RentedRoomItem> rented_rooms;

        private string Characteristics()
        {
            var result = "Characteristics:";
            foreach (var c in characteristics)
            {
                result += $"\n        {c}";
            }
            return result;
        }

        private string Statuses()
        {
            var result = "Statuses:";
            foreach (var s in statuses)
            {
                result += $"\n        {s}";
            }
            return result;
        }

        private string Actions()
        {
            var result = "Actions:";
            foreach (var a in actions)
            {
                result += $"\n{a.type_description} {a.item.type_title}";
            }
            return result;
        }

        public override string ToString() => @$"Citizen:
        ID: {id}
        Name: {name}
        Active Task ID: {active_task_id}
        Storage Root Item ID: {storage_root_item_id}
        {Characteristics()}
        {Statuses()}
        Items Count: {items.Count}
        {Actions()}";
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
