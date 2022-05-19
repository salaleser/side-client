using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ItemTypeItem : Item
    {
        public string description;

        public override string ToString() => @$"[item_type]:
title={title}
description={description}";
    }
}
