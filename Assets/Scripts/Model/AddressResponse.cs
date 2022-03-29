using System;
using System.Collections.Generic;

[System.Serializable]
public class AddressResponse
{
    public List<AddressItem> addresses = new();
    public AddressItem current_address;
}
