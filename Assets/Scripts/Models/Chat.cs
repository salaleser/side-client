using System;
using System.Collections.Generic;

namespace Models
{
    public class Chat
    {
        public int CitizenId;
        public int OrganizationId;
        public string Text;
        public string CreatedAt;

        public override string ToString() => @$"[{DateTime.Parse(CreatedAt).ToString("ddd hh:mm:ss")}] {(OrganizationId == 0 ? "" : $"<{OrganizationId}>")} <link={CitizenId}><color=blue><u>{CitizenId}</u></color></link>: {Text}";
    }
}
