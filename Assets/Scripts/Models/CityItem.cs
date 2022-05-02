using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class CityItem : Item
    {
        public int id;
        public int x;
        public int y;
        public int region_id;
        public List<BlockItem> blocks;

        public override string ToString() => @$"City:
        ID: {id}
        Title: {title}
        Blocks Count: {blocks.Count}";
    }
}
