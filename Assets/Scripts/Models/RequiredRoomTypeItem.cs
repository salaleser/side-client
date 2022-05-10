using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RequiredRoomTypeItem
    {
        public int room_type_id;
        public int organization_id;
        public string title;
        public bool is_attached;
        public RoomItem attached_room;
        public int w;
        public int h;

        public override string ToString() => @$"Required Room Type:
        Title: {title}
        Attached Room: {attached_room}
        Width: {w}
        Height: {h}";
    }
}
