using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class PlanetSimpleItem : Item
    {
        public int number;
        public int explorer_id;
        public int system_id;
        public int size;
        public int continents_count;

        public override string ToString() => @$"[planet]:
id={id}
title={title}
explorer_id={explorer_id}
continents_count={continents_count}";
    }
}
