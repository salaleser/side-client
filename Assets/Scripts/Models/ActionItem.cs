using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ActionItem : Item
    {
        public int type_id;
        public string type_title;
        public string type_description;

        public override string ToString() => @$"[action]:
type_id={type_id}
type_title={type_title}
type_description={type_description}";
    }
}
