using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class RoomTypeItem : Item
    {
        public RoomTypeProperties properties;

        public override string ToString() => @$"[room Type]:
title={title}
properties={properties}";
    }

    [System.Serializable]
    public class RoomTypeProperties
    {
        public UnityEngine.Color color;
        public int w;
        public int h;
        public int durability;

        public override string ToString() => @$"[properties]:
color={color}
w={w}
h={h}
durability={durability}";
    }
}
