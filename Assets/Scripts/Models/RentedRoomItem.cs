using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RentedRoomItem : Item
    {
        public int id;
        public int type_id;
        public int x;
        public int y;
        public int w;
        public int h;
        public string description = "empty";
        public int renter_id;
        public int organization_id;

        public override string ToString() => @$"Room:
        ID: {id}
        Type ID: {type_id}
        Description: {description}
        Renter ID: {renter_id}";
    }
}
