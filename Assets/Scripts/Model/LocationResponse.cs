using System;
using System.Collections.Generic;

[System.Serializable]
public class ChatItem
{
    public string citizen_id;
    public string text;
    public string created_at;

    public override string ToString() => @$"[{DateTime.Parse(created_at).ToString("dd.MM.yyyy hh:mm:ss")}] {citizen_id}: {text}";
}

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

[System.Serializable]
public class Address
{
    public int id;
    public string title;
    public int type_id;
    public int parent_id;
    public string type_emoji;

    public override string ToString() => @$"Address:
    ID: {id}
    Title: {title}
    Type ID: {type_id}
    Parent ID: {parent_id}";
}

[System.Serializable]
public class ItemItem
{
    public int id;
    public int type_id;
    public string type_title;
    public int quantity;

    public override string ToString() => @$"Item:
    ID: {id}
    Type ID: {type_id}
    Type Title: {type_title}
    Quantity: {quantity}";
}

[System.Serializable]
public class LocationItem
{
    public int x;
    public int y;
    public int id;
    public string title;
    public Address address;
    public int type_id;
    public string owner_id;
    public int parent_id;
    public List<ItemItem> items = new();
    public string type_emoji;
    public int root_item_id;

    public override string ToString() => @$"Location:
    X: {x}
    Y: {y}
    ID: {id}
    Title: {title}
    Type ID: {type_id}
    Owner ID: {owner_id}
    Parent ID: {parent_id}
    Items Count: {items.Count}

{address}";
}

[System.Serializable]
public class LocationResponse
{
    public List<CitizenItem> citizens = new();
    public List<LocationItem> locations = new();
    public List<ChatItem> chat = new();
    public LocationItem current_location;
}
