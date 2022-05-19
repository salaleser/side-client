using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class BlockItem : Item
    {
        public int x;
        public int y;
        public int city_id;
        public int explorer_id;
        public List<ParcelItem> parcels;
        public int parcels_count;

        public override string ToString() => @$"[block]:
id={id}
title={title}
explorer_id={explorer_id}
parcels_count={parcels_count}";
    }
}
