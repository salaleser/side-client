using System;
using System.Collections.Generic;

[System.Serializable]
public class LocationResponse
{
    public List<ChatItem> chat = new();
    public List<AddressItem> addresses = new();
    public AddressItem current_address;
}
