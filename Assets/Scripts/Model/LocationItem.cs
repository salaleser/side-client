using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class LocationItem
    {
        public int id;
        public int type_id;
        public string type_title;
        public int owner_id;
        public int address_id;
        public int address_parent_id;
        public List<FloorItem> floors;

        public override string ToString() => @$"Location:
        ID: {id}
        Type ID: {type_id}
        Type Title: {type_title}
        Owner ID: {owner_id}
        Address ID: {address_id}
        Floors Count: {floors.Count}";
    }
}
