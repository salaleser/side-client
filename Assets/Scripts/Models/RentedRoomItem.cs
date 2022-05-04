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
        public int item_id;
        public List<int> organization_ids;

        public override string ToString() => @$"Rented Room:
        ID: {id}
        Type ID: {type_id}
        Title: {title}
        Description: {description}
        {OrganizationIds()}
        Renter ID: {renter_id}";

        private string OrganizationIds()
        {
            var result = "Organization IDs:";
            foreach (var v in organization_ids)
            {
                result += $"\n    {v}";
            }
            return result;
        }
    }
}
