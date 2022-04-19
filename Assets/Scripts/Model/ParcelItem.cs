using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ParcelItem : Item
    {
        public int id;
        public int block_id;
        public int x;
        public int y;
        public List<FloorItem> floors = new();

        public override string ToString() => @$"Parcel:
        ID: {id}
        Title: {title}
        Block ID: {block_id}
        Floors Count: {floors.Count}";
    }
}
