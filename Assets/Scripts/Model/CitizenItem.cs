using System;
using System.Collections.Generic;

[System.Serializable]
public class CitizenItem
{
    public int id;
    public string name;
    public int location_id;
    public int account_id;
    public int active_task_id;
    public int room_id;
    public int address_id;
    public int address_type_id;
    public int address_parent_id;
    public int floor_number;
    public string action_type_emoji;
    public int root_item_id;
    public List<ItemItem> items = new();
    public List<StatusItem> statuses = new();
    public List<CharacteristicItem> characteristics = new();

    private string Characteristics()
    {
        var result = "Characteristics:";
        foreach (var c in characteristics)
        {
            result += $"\n        {c}";
        }
        return result;
    }

    private string Statuses()
    {
        var result = "Statuses:";
        foreach (var s in statuses)
        {
            result += $"\n        {s}";
        }
        return result;
    }

    public override string ToString() => @$"Citizen:
    ID: {id}
    Name: {name}
    {Characteristics()}
    {Statuses()}
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
    public string value;
    public bool is_public;

    public override string ToString() => @$"{type_title}={value}";
}