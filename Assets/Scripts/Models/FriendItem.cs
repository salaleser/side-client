using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class FriendItem : Item
    {
        public CitizenSimpleItem citizen;

        public override string ToString() => @$"[friend]:
id={id}
citizen={citizen}";
    }
}
