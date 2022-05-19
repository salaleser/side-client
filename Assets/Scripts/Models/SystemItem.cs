using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class SystemItem : Item
    {
        public int number;
        public int explorer_id;
        public int galaxy_id;
        public List<PlanetSimpleItem> planets;
        public int planets_count;

        public override string ToString() => @$"[system]:
id={id}
title={title}
explorer_id={explorer_id}
planets_count={planets_count}";
    }
}
