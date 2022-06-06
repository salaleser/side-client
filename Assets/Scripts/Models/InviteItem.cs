using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class InviteItem : Item
    {
        public OrganizationItem organization;
        public CitizenSimpleItem citizen;

        public override string ToString() => @$"Organization Title: {organization.title}
Members Count: {organization.members.Count}
Join Type ID: {organization.join_type_id}
Membership Fee: {organization.properties.membership_fee}";
    }
}
