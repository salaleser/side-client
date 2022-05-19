using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class EmailItem : Item
    {
        public int from;
        public string from_name;
        public string date;
        public string date_formatted;
        public string subject;
        public string body;
        public int status; // 0 - новое; 1 - прочитанное

        public override string ToCaption() => $"({id}) {from_name}: {subject}";

        public override string ToString() => @$"[{date_formatted}] {subject}:
{body}

{from_name}";
    }
}
