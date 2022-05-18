using System;
using System.Collections.Generic;
using Models;

namespace Models
{
    [System.Serializable]
    public class GroundItem : Item
    {
        public int id;
        public int x;
        public int y;
        public int z;
        
        public override string ToString() => @$"[ground]:
id={id}
title={title}";
    }
}
