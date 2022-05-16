using System;
using System.Collections.Generic;
using Models;

namespace Models
{
    [System.Serializable]
    public class MoveIntoRoomResponse
    {
        public CitizenItem citizen;
        public ParcelItem floor;
    }
}
