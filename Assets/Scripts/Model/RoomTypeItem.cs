using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RoomTypeItem
    {
        public int id;
        public string title;

        public override string ToString() => @$"{title}";
    }
}
