using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class PollItem : Item
    {
        public OrganizationItem organization;
        public string description;

        public override string ToString() => @$"Organization Title: {organization.title}
Description: {description}";
    }
}
