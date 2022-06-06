using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class OfferItem : Item
    {
        public OrganizationItem organization;
        public CitizenSimpleItem citizen;

        public override string ToString() => @$"Organization Title: {organization.title}
Salary: {organization.salary}";
    }
}
