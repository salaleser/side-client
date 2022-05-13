using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class DealCreateResponse
    {
        public DealItem deal;
        public string warning;
        public string error;
    }
}
