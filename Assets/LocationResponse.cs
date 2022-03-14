using System.Collections.Generic;
using Newtonsoft.Json;

public class Citizen
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("action_type_emoji")]
    public string? ActionTypeEmoji { get; set; }
}

public class Address
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("type_id")]
    public int TypeId { get; set; }

    [JsonProperty("parent_id")]
    public int ParentId { get; set; }

    [JsonProperty("type_emoji")]
    public string TypeEmoji { get; set; }
}

public class Location
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("x")]
    public int X { get; set; }

    [JsonProperty("y")]
    public int Y { get; set; }

    [JsonProperty("address")]
    public Address Address { get; set; }

    [JsonProperty("type_id")]
    public int TypeId { get; set; }

    [JsonProperty("owner_id")]
    public int? OwnerId { get; set; }

    [JsonProperty("parent_id")]
    public int ParentId { get; set; }

    [JsonProperty("type_emoji")]
    public string TypeEmoji { get; set; }
}

public class LocationResponse
{
    [JsonProperty("citizens")]
    public List<Citizen> Citizens { get; } = new List<Citizen>();

    [JsonProperty("locations")]
    public List<Location> Locations { get; } = new List<Location>();

    [JsonProperty("location_id")]
    public int LocationId { get; set; }
}
