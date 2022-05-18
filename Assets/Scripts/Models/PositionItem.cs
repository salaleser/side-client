using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class PositionItem : Item
    {
        public int id;
        public int type_id;
        public bool is_free;

        public override string ToString() => @$"[position]:
id={id}
type_id={type_id}
title={title}
is_free={is_free}";
    }
}
