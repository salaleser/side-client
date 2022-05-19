using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RentedRoomItem : Item
    {
        public RoomTypeItem type;
        public int x;
        public int y;
        public int w;
        public int h;
        public int renter_id;
        public int item_id;
        public List<int> organization_ids;

        public override string ToString() => @$"[rented_room]:
id={id}
type={type}
title={title}
organization_ids={OrganizationIds()}
renter_id={renter_id}";

        private string OrganizationIds()
        {
            var result = "";
            foreach (var v in organization_ids)
            {
                result += $"{v},";
            }
            return result;
        }
    }
}
