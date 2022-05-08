﻿using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class GalaxyItem : Item
    {
        public int id;
        public int number;
        public int explorer_id;
        public List<SystemItem> systems;
        public int systems_count;

        public override string ToString() => @$"Galaxy:
        ID: {id}
        Title: {title}
        Explorer ID: {explorer_id}
        Systems Count: {systems_count}";
    }
}
