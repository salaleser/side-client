using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ContinentItem : Item
    {
        public int id;
        public int number;
        public int planet_id;
        public List<RegionItem> regions;

        public override string ToString() => @$"Continent:
        ID: {id}
        Number: {number}
        Title: {title}
        Regions Count: {regions.Count}";
    }
}
