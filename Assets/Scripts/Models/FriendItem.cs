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

        public override string ToString() => @$"Friend:
        ID: {id}
        Citizen ID: {citizen_id}
        Citizen Name: {citizen_name}";
    }
}
