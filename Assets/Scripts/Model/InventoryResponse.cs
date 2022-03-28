using System;
using System.Collections.Generic;

[System.Serializable]
public class InventoryResponse
{
    public List<ItemItem> items = new();
    public string title;
}
