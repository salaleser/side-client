using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class FloorItem : Item
    {
        public int id;
        public int x;
        public int y;
        public int w;
        public int h;
        public int number;
        public int owner_id;
        public int entrance_room_id;
        public List<RoomItem> rooms;

        public override string ToString() => @$"Floor:
        ID: {id}
        Number: {number}
        Rooms Count: {rooms.Count}";
    }
}
