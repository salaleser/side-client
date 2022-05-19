using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class PlanetItem : Item
    {
        public int number;
        public int explorer_id;
        public int system_id;
        public int size;
        public List<ContinentItem> continents;
        public int continents_count;

        public PlanetItem(PlanetSimpleItem planetSimple)
        {
            number = planetSimple.number;
            explorer_id = planetSimple.explorer_id;
            system_id = planetSimple.system_id;
            size = planetSimple.size;
            continents_count = planetSimple.continents_count;
        }

        public override string ToString() => @$"[planet]:
id={id}
title={title}
explorer_id={explorer_id}
continents_count={continents_count}";
    }
}
