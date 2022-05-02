using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RegionItem : Item
    {
        public int id;
        public int x;
        public int y;
        public int z;
        public int continent_id;
        public int cities_count;

        public override string ToString() => @$"Region:
        ID: {id}
        Title: {title}";
    }
}
