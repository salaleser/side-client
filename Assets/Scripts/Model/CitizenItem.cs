using System;
using System.Collections.Generic;

[System.Serializable]
public class CitizenItem
{
    public int x = 1;
    public int y = 1;
    public int id;
    public string name;
    public string action_type_emoji;
    public int root_item_id;
    public List<ItemItem> items = new();
    public List<StatusItem> statuses = new();
    public List<CharacteristicItem> characteristics = new();

    public override string ToString() => @$"Citizen:
    X: {x}
    Y: {y}
    ID: {id}
    Name: {name}
    Items Count: {items.Count}";
}

[System.Serializable]
public class StatusItem
{
    public string type_id;
    public string type_title;
    public int value;

    public override string ToString() => @$"{type_title}={value}";
}

[System.Serializable]
public class CharacteristicItem
{
    public string type_id;
    public string type_title;
    public int value;
    public bool is_public;

    public override string ToString() => @$"{type_title}={value}";
}
