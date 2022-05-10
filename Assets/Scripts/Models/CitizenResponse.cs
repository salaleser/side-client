using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class CitizenResponse
    {
        public CitizenItem citizen;
        public List<RentedRoomItem> rented_rooms;
        public List<OrganizationItem> organizations;
    }
}
