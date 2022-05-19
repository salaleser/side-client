using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RegionItem : Item
    {
        public int x;
        public int y;
        public int z;
        public int continent_id;
        public int explorer_id;
        public List<CityItem> cities;
        public int cities_count;

        public override string ToString() => @$"[region]:
id={id}
title={title}
z={z}
explorer_id={explorer_id}
cities_count={cities_count}";
    }
}
