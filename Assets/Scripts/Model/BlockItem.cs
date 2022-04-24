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
        public List<ParcelItem> parcels;

        public override string ToString() => @$"Block:
        ID: {id}
        Title: {title}
        Parcels Count: {parcels.Count}";
    }
}
