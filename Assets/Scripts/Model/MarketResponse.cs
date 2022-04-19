using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class MarketResponse
    {
        public List<LotItem> lots = new();
    }
}
