using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class BlockItem : Item
    {
        public int id;
        public int x;
        public int y;
        public int city_id;
        public int explorer_id;
        public List<ParcelItem> parcels;

        public override string ToString() => @$"Block:
        ID: {id}
        Title: {title}
        Explorer ID: {explorer_id}
        Parcels Count: {parcels.Count}";
    }
}
