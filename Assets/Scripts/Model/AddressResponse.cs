﻿using System;
using System.Collections.Generic;
using Models;

namespace Models
{
    [System.Serializable]
    public class AddressResponse
    {
        public AddressItem address;
        public List<AddressItem> addresses = new();
    }
}
