using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class CityItem : Item
    {
        public int x;
        public int y;
        public int explorer_id;
        public int region_id;
        public List<BlockItem> blocks;
        public int blocks_count;

        public override string ToString() => @$"[city]:
id={id}
title={title}
explorer_id={explorer_id}
blocks_count={blocks_count}";
    }
}
