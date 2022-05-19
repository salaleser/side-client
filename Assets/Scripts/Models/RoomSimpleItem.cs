using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RoomSimpleItem : Item
    {
        public RoomTypeSimpleItem type;
        public int x;
        public int y;
        public int z;
        public int w;
        public int h;
        public int health;
        public int renter_id;
        public bool rentable;
    }
}
