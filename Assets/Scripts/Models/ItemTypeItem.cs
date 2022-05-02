﻿using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ItemTypeItem
    {
        public int id;
        public string title;
        public string description;

        public override string ToString() => @$"Item Type:
        Title: {title}
        Description: {description}";
    }
}