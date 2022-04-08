using System;
using System.Collections.Generic;

namespace Models {
    [System.Serializable]
    public class PositionItem : Item
    {
        public int id;
        public int type_id;
        public bool is_free;

        public override string ToString() => @$"Position:
        ID: {id}
        Type ID: {type_id}
        Title: {title}
        Is Free: {is_free}";
    }
}
