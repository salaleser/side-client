using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ItemItem
    {
        public int id;
        public int parent_id;
        public ItemTypeItem type;
        public int quantity;
        public int price;
        public int children_count;

        public override string ToString() => @$"[item]:
id={id}
parent_id={parent_id}
type={type}
quantity={quantity}
price={price}
children_count={children_count}";
    }
}
