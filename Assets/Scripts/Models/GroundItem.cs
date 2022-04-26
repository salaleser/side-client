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
        public int location_type_id;
        
        public override string ToString() => @$"Ground:
        X: {x} / Y: {y}
        Location Type ID: {location_type_id}";
    }
}
