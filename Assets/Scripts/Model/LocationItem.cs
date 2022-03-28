using System;
using System.Collections.Generic;

[System.Serializable]
public class LocationItem
{
    public int id;
    public string title;
    public int type_id;
    public string owner_id;
    public int parent_id;
    public int address_id;
    public List<ItemItem> items = new();
    public List<CitizenItem> citizens = new();
    public string type_emoji;
    public int root_item_id;

    public override string ToString() => @$"Location:
    ID: {id}
    Title: {title}
    Type ID: {type_id}
    Owner ID: {owner_id}
    Parent ID: {parent_id}
    Address ID: {address_id}
    Citizens Count: {citizens.Count}
    Items Count: {items.Count}";
}