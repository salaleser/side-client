using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class UniverseItem : Item
    {
        public int id;
        public int number;
        public List<GalaxyItem> galaxies;

        public override string ToString() => @$"[universe]:
id={id}
title={title}
galaxies.Count={galaxies.Count}";
    }
}
