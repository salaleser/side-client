using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class LocationTypeItem
    {
        public int id;
        public int address_id;
        public string title;

        public override string ToString() => @$"{title}";
    }
}
