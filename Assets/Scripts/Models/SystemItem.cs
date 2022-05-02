using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class SystemItem : Item
    {
        public int id;
        public int number;
        public int explorer_id;
        public int galaxy_id;
        public List<PlanetItem> planets;

        public override string ToString() => @$"System:
        ID: {id}
        Title: {title}
        Explorer ID: {explorer_id}
        Planets Count: {planets.Count}";
    }
}
