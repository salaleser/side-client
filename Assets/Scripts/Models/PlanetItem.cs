using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class PlanetItem : Item
    {
        public int id;
        public int number;
        public int explorer_id;
        public int system_id;
        public int size;
        public List<ContinentItem> continents;
        public int continents_count;

        public override string ToString() => @$"[planet]:
id={id}
title={title}
explorer_id={explorer_id}
continents_count={continents_count}";
    }
}
