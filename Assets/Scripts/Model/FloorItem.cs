using System;
using System.Collections.Generic;

[System.Serializable]
public class FloorItem
{
    public int id;
    public int number;
    public List<RoomItem> rooms = new();

    public override string ToString() => @$"Floor:
    ID: {id}
    Number: {number}
    Rooms Count: {rooms.Count}";
}
