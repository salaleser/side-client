using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class OrganizationTypeItem
    {
        public int id;
        public string title;
        public OrganizationRequirements requirements;

        public override string ToString() => @$"Organization Type:
        ID: {id}
        Title: {title}
        Required Room Types Count: {requirements.room_types.Count}";
    }

    [System.Serializable]
    public class OrganizationRequirements
    {
        public List<RequiredRoomTypeItem> room_types;
        public List<int> room_type_ids;
    }
}
