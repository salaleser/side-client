using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class BuildLocationResponse
    {
        public List<LocationTypeItem> location_types = new();
    }
}
