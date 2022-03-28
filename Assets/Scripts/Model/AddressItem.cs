using System;
using System.Collections.Generic;

[System.Serializable]
public class AddressItem
{
    public int id;
    public int x;
    public int y;
    public string title;
    public int type_id;
    public int parent_id;
    public string type_emoji;
    public List<LocationItem> locations = new();

    public override string ToString() => @$"Address:
    ID: {id}
    X: {x}; Y: {y}
    Title: {title}
    Type ID: {type_id}
    Parent ID: {parent_id}
    Locations Count: {locations.Count}";
}
