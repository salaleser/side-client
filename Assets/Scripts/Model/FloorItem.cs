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
        public List<RoomItem> rooms = new();

        public override string ToString() => @$"Floor:
        ID: {id}
        Number: {number}
        Rooms Count: {rooms.Count}";
    }
}
