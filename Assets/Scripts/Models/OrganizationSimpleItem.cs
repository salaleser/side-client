using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class OrganizationSimpleItem : Item
    {
        public OrganizationTypeSimpleItem type;
        public bool is_active;
    }
}
