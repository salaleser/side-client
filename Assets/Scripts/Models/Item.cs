using System;
using System.Collections.Generic;

namespace Models
{
    public class Item : IItem
    {
        public int id;
        public string title;

        public override string ToString() => @$"{title}";

        public virtual string ToCaption() => $"{title} ({id})";
        
        public int ParseId(string caption)
        {
            int id;
            
            var a1 = caption.Split("(");
            if (a1.Length < 2)
            {
                return -1;
            }

            var s2 = a1[1].Split(")")[0];
            if (!int.TryParse(s2, out id))
            {
                return -2;
            }

            return id;
        }
    }
}
