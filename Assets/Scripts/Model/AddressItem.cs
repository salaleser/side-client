using System;
using System.Collections.Generic;

[System.Serializable]
public class AddressItem
{
    public int? id;
    public int x;
    public int y;
    public string title;
    public int type_id;
    public string type_title;
    public int parent_id;
    public LocationItem location;

    public override string ToString() => @$"Address:
    ID: {id}
    X: {x}; Y: {y}
    Type Title: {type_title}
    Title: {title}
    Type ID: {type_id}
    Parent ID: {parent_id}";
}
