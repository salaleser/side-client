using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class BuildRoomResponse
    {
        public List<RoomTypeItem> room_types = new();
    }
}
