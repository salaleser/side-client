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
        
        public override string ToString() => @$"Ground:
        X: {x} / Y: {y}";
    }
}
