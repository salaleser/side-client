using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class CityItem : Item
    {
        public int id;
        public int region_id;
        public int x;
        public int y;
        public List<BlockItem> blocks;

        public override string ToString() => @$"City:
        ID: {id}
        Title: {title}
        Region ID: {region_id}
        Blocks Count: {blocks.Count}";
    }
}
