using System;
using System.Collections.Generic;

[System.Serializable]
public class AddressResponse
{
    public AddressItem address;
    public List<AddressItem> addresses = new();
}
