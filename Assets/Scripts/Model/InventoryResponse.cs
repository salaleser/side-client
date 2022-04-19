using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class InventoryResponse
    {
        public List<ItemItem> items = new();
        public string title;
    }
}
