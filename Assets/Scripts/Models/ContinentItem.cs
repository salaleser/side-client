using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ContinentItem : Item
    {
        public int number;
        public int explorer_id;
        public int planet_id;
        public List<RegionItem> regions;
        public int regions_count;

        public override string ToString() => @$"[continent]:
id={id}
number={number}
title={title}
explorer_id={explorer_id}
regions_count={regions_count}";
    }
}
