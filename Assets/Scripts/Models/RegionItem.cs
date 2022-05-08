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
        public int z;
        public int continent_id;
        public int explorer_id;
        public List<CityItem> cities;
        public int cities_count;

        public override string ToString() => @$"Region:
        ID: {id}
        Title: {title}
        Height: {z}
        Explorer ID: {explorer_id}
        Cities Count: {cities_count}";
    }
}
