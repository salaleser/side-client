﻿using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RentedRoomItem : Item
    {
        public int id;
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
{OrganizationIds()}
renter_id={renter_id}";

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
