using System;
using System.Collections.Generic;
using Models;

namespace Models
{
    [System.Serializable]
    public class GroundItem : Item
    {
        public int x;
        public int y;
        public int z;
        public int parcel_id;
        
        public override string ToString() => @$"[ground]:
id={id}
title={title}
x={x}
y={y}
z={z}";
    }
}
