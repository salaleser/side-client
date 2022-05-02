using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ParcelItem : Item
    {
        public int id;
        public int x;
        public int y;
        public int owner_id;
        public int explorer_id;
        public int block_id;
        public List<FloorItem> floors;
        public ParcelProperties properties;

        public override string ToString() => @$"Parcel:
        ID: {id}
        Title: {title}
        Owner ID: {owner_id}
        Explorer ID: {explorer_id}
        Floors Count: {floors.Count}";
    }

    [System.Serializable]
    public class ParcelProperties
    {
        public List<Resource> resources;
    }

    [System.Serializable]
    public class Resource
    {
        public int type_id;
        public int quantity;
    }
}
