using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class MemberItem
    {
        public int id;
        public int organization_id;
        public int citizen_id;
        public DateTime created_at;

        public override string ToString() => @$"Member:
        ID: {id}
        Organization ID: {organization_id}
        Citizen ID: {citizen_id}
        Created At: {created_at}";
    }
}
