using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RegionItem : Item
    {
        public int id;
        public int continent_id;
        public int x;
        public int y;
        public List<CityItem> cities = new();

        public override string ToString() => @$"Region:
        ID: {id}
        Title: {title}
        Continent ID: {continent_id}
        Cities Count: {cities.Count}";
    }
}
