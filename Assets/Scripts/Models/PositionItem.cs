
using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class PositionItem : Item
    {
        public int organization_id;
        public CitizenSimpleItem citizen;
        public int salary;
        public PositionTypeItem type;

        public override string ToString() => @$"[position]
type={type}
organization_id={organization_id}
citizen={citizen}
salary={salary}";
    }
}
