using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class PlanetItem : Item
    {
        public int id;
        public int number;
        public int explorer_id;
        public int system_id;
        public int size;
        public List<ContinentItem> continents;

        public override string ToString() => @$"Planet:
        ID: {id}
        Title: {title}
        Explorer ID: {explorer_id}
        Continents Count: {continents.Count}";
    }
}
