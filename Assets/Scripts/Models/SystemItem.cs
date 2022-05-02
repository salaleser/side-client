using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class SystemItem : Item
    {
        public int id;
        public int number;
        public int galaxy_id;
        public List<PlanetItem> planets;

        public override string ToString() => @$"System:
        ID: {id}
        Title: {title}
        Planets Count: {planets.Count}";
    }
}
