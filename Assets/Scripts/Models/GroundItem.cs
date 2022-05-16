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
        
        public override string ToString() => @$"Ground: Position{{x={x},y={y},z={z}}}";
    }
}
