using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class InventoryResponse
    {
        public ItemItem item;
        public List<ItemItem> children;
    }
}
