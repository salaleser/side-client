using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ActionItem
    {
        public int type_id;
        public string type_title;
        public string type_description;
        public ItemItem item;

        public override string ToString() => @$"Action:
        Type ID: {type_id}
        Type Title: {type_title}
        Type Description: {type_description}
        Item: {item}";
    }
}
