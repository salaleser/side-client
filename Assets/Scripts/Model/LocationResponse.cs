using System.Collections.Generic;

[System.Serializable]
public class CitizenItem
{
    public int id;
    public string name;
    public string action_type_emoji;

    public override string ToString() => @$"Citizen:
    ID: {id}
    Name: {name}
    Action Type Emoji: {action_type_emoji}";
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
    Parent ID: {parent_id}
    Type Emoji: {type_emoji}";
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

    public override string ToString() => @$"Location:
    X: {x}
    Y: {y}
    ID: {id}
    Title: {title}
    Type ID: {type_id}
    Owner ID: {owner_id}
    Parent ID: {parent_id}
    Type Emoji: {type_emoji}

{address}";
}

[System.Serializable]
public class LocationResponse
{
    public List<CitizenItem> citizens = new();
    public List<LocationItem> locations = new();
    public LocationItem current_location;
}
