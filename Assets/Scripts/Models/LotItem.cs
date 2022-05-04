using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class LotItem
    {
        public int lot_id;
        public int item_id;
        public int root_item_id;
        public int owner_id;
        public int item_type_id;
        public string item_type_title;
        public int quantity;
        public int price;

        public override string ToString() => @$"Lot:
        ID: {lot_id}
        Item ID: {item_id}
        Root Item ID: {root_item_id}
        Owner ID: {owner_id}
        Type ID: {item_type_id}
        Type Title: {item_type_title}
        Quantity: {quantity}
        Price: {price}";
    }
}
