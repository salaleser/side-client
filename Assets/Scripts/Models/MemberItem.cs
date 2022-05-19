
using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class MemberItem : Item
    {
        public int organization_id;
        public CitizenSimpleItem citizen;
        public string created_at;

        public override string ToString() => @$"[member]:
organization_id={organization_id}
citizen={citizen}
created_at={created_at}";
    }
}
