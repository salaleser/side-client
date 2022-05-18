using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class FriendItem
    {
        public int id;
        public int citizen_id;
        public string citizen_name;

        public override string ToString() => @$"[friend]:
id={id}
citizen_id={citizen_id}
citizen_name={citizen_name}";
    }
}
