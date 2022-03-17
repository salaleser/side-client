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

    public override string ToString() => @$"Citizen:
    X: {x}
    Y: {y}
    ID: {id}
    Name: {name}";
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

{address}";
}

[System.Serializable]
public class LocationResponse
{
    public List<CitizenItem> citizens = new();
    public List<LocationItem> locations = new();
    public List<ItemItem> items = new();
    public LocationItem current_location;
}
