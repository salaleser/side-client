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
        public int explorer_id;
        public int region_id;
        public List<BlockItem> blocks;
        public int blocks_count;

        public override string ToString() => @$"City:
        ID: {id}
        Title: {title}
        Explorer ID: {explorer_id}
        Blocks Count: {blocks_count}";
    }
}
