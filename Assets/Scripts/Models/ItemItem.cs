using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ItemItem
    {
        public int id;
        public int parent_id;
        public int type_id;
        public int owner_id;
        public string type_title;
        public int quantity;
        public int children_count;

        public override string ToString() => @$"Item:
        ID: {id}
        Parent ID: {parent_id}
        Type ID: {type_id}
        Type Title: {type_title}
        Owner ID: {owner_id}
        Quantity: {quantity}
        Children Count: {children_count}";
    }
}
