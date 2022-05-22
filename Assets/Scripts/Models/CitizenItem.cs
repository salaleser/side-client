using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class CitizenItem : Item
    {
        public int account_id;
        public int active_task_id;
        public int parcel_id;
        public int item_id;
        public RentedRoomItem delivery_address;
        public List<ItemItem> items;
        public List<FriendItem> friends;
        public List<ActionItem> actions;
        public List<StatusItem> statuses;
        public List<CharacteristicItem> characteristics;
        public List<OrganizationSimpleItem> organizations;
        public List<EmailItem> emails;
        public RoomItem room;
        public List<RentedRoomItem> rented_rooms;

        private string Characteristics()
        {
            var result = "[characteristics]:";
            foreach (var c in characteristics)
            {
                result += $"{c}";
            }
            return result;
        }

        private string Statuses()
        {
            var result = "[statuses]:";
            foreach (var s in statuses)
            {
                result += $"{s},";
            }
            return result;
        }

        public override string ToString() => @$"[citizen]:
id={id}
title={title}
active_task_id={active_task_id}
delivery_address={delivery_address}
{Characteristics()}
{Statuses()}
items.Count={items.Count}
actions.Count={actions.Count}";
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
