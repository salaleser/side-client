using System;
using System.Collections.Generic;

[System.Serializable]
public class RoomItem
{
    public int id;
    public int type_id;
    public int x;
    public int y;
    public int w;
    public int h;
    public string title;
    public int room_item_id;
    public List<ItemItem> items = new();
    public List<CitizenItem> citizens = new();
    public List<MessageItem> messages = new();

    public override string ToString() => @$"Room:
    ID: {id}
    Type ID: {type_id}
    Title: {title}
    Items Count: {items.Count}
    Citizens Count: {citizens.Count}
    Messages Count: {messages.Count}
    Root Item ID: {room_item_id}";
}
