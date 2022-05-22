
using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class PositionItem : Item
    {
        public OrganizationSimpleItem organization;
        public CitizenSimpleItem citizen;
        public int salary;
        public PositionTypeItem type;

        public override string ToString() => @$"[position]
type={type}
organization_id={organization}
citizen={citizen}
salary={salary}";
    }
}
