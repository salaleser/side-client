using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class PageItem : Item
    {
        public int organization_id;
        public string content;
        public string path;
    }
}
