using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RequiredRoomTypeItem
    {
        public int id;
        public string title;
        public bool is_attached;
        public int w;
        public int h;

        public override string ToString() => @$"Required Room Type:
        Title: {title}
        Is Attached: {is_attached}
        Width: {w}
        Height: {h}";
    }
}
