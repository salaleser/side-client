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
    public int floors_count;

    public override string ToString() => @$"Location:
    ID: {id}
    Title: {title}
    Type ID: {type_id}
    Owner ID: {owner_id}
    Parent ID: {parent_id}
    Address ID: {address_id}
    Floors Count: {floors_count}";
}