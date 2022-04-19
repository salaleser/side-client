using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class BlockItem : Item
    {
        public int id;
        public int city_id;
        public int x;
        public int y;
        public List<ParcelItem> parcels = new();

        public override string ToString() => @$"Block:
        ID: {id}
        Title: {title}
        City ID: {city_id}
        Parcels Count: {parcels.Count}";
    }
}
