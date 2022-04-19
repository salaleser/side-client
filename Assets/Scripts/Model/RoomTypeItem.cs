using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RoomTypeItem
    {
        public int id;
        public int default_x;
        public int default_y;
        public int default_w;
        public int default_h;
        public string title;

        public override string ToString() => @$"{title}";
    }
}
