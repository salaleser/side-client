using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class UserItem : Item
    {
        public string username;
        public CitizenItem citizen;
        public string created_at;
    }
}
