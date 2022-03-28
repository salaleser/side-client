using System;
using System.Collections.Generic;

[System.Serializable]
public class RoomItem
{
    public int id;
    public int type_id;
    public int floor;

    public override string ToString() => @$"Room:
    ID: {id}
    Type ID: {type_id}
    Floor: {floor}";
}
