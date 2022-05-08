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
        public int z;
        public int w;
        public int h;
        public int entrance_room_id;
        public int parcel_id;
        public List<RoomItem> rooms;
        public int rooms_count;

        public override string ToString() => @$"Floor:
        ID: {id}
        X: {x}
        Y: {y}
        Z: {z}
        Width: {w}
        Height: {h}
        Entrance Room ID: {entrance_room_id}";
    }
}
