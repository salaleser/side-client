using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class AddressItem : Item
    {
        public int id;
        public int x;
        public int y;
        public AddressTypes type_id;
        public string type_title;
        public int parent_id;
        public LocationItem location;

        public override string ToString() => @$"Address:
        ID: {id}
        X: {x}; Y: {y}
        Type Title: {type_title}
        Title: {title}
        Type ID: {type_id}
        Parent ID: {parent_id}";
    }
}
