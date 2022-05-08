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
        public int floors_count;
        public ParcelProperties properties;

        public override string ToString() => @$"Parcel:
        ID: {id}
        Title: {title}
        Owner ID: {owner_id}
        Explorer ID: {explorer_id}
        {properties}
        Floors Count: {floors_count}";
    }

    [System.Serializable]
    public class ParcelProperties
    {
        public List<Resource> resources;

        public override string ToString() => @$"Properties:
        {Resources()}";

        private string Resources()
        {
            var result = "Resources:";
            foreach (var resource in resources)
            {
                result += $"\n        {resource}";
            }
            return result;
        }
    }

    [System.Serializable]
    public class Resource
    {
        public ItemTypeItem type;
        public int quantity;

        public override string ToString() => @$"{type}
        Quantity: {quantity}";
    }
}
