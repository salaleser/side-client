using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class OfferItem : Item
    {
        public OrganizationSimpleItem organization;
        public PositionItem position;
        public CitizenSimpleItem citizen;

        public override string ToString() => @$"Organization Title: {organization.title}
Organization Type Title: {organization.type.title}
Is Active: {organization.is_active}
Position Type: {position}
Position Type: {position.type}
Position Type Title: {position.type.title}
Salary: {position.salary}";
    }
}
