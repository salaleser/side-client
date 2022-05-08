using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ContinentItem : Item
    {
        public int id;
        public int number;
        public int explorer_id;
        public int planet_id;
        public List<RegionItem> regions;
        public int regions_count;

        public override string ToString() => @$"Continent:
        ID: {id}
        Number: {number}
        Title: {title}
        Explorer ID: {explorer_id}
        Regions Count: {regions_count}";
    }
}
