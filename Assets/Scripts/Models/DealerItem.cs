using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class DealerItem : Item
    {
        public int account_id;
        public RoomItem room;

        public override string ToString() => @$"[dealer]:
id={id}
title={title}
account_id={account_id}
room={room}";
    }
}
