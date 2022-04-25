using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RoomTypeItem
    {
        public int id;
        public string title;
        public int w;
        public int h;

        public override string ToString() => @$"Room Type:
        Title: {title}
        Width: {w}
        Height: {h}";
    }
}
