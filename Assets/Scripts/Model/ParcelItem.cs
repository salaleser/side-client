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
        public List<FloorItem> floors;

        public override string ToString() => @$"Parcel:
        ID: {id}
        Title: {title}
        Owner ID: {owner_id}
        Floors Count: {floors.Count}";
    }
}
