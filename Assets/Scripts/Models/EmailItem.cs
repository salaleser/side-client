using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class EmailItem
    {
        public int id;
        public int from;
        public DateTime date;
        public string subject;
        public string body;
        public int status; // 0 - новое; 1 - прочитанное

        public override string ToString() => $"{subject}({id})";
    }
}
