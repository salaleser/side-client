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
        public int attached_room_id;
        public int w;
        public int h;

        public override string ToString() => @$"Required Room Type:
        Title: {title}
        Is Attached: {is_attached}
        Attached Room ID: {attached_room_id}
        Width: {w}
        Height: {h}";
    }
}
