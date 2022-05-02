using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class PlanetItem : Item
    {
        public int id;
        public int number;
        public int system_id;
        public List<ContinentItem> continents;

        public override string ToString() => @$"Continent:
        ID: {id}
        Title: {title}";
    }
}
