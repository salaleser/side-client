using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RequiredRoomTypeItem : Item
    {
        public int room_type_id;
        public int organization_id;
        public RoomItem attached_room;
        public int w;
        public int h;

        public override string ToString() => @$"[required_room_type]:
title={title}
attached_room={attached_room}
w={w}
h={h}";
    }
}
