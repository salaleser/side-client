using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RegionItem : Item
    {
        public int id;
        public int x;
        public int y;
        public List<CityItem> cities;

        public override string ToString() => @$"Region:
        ID: {id}
        Title: {title}
        Cities Count: {cities.Count}";
    }
}
