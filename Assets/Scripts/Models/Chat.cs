using System;
using System.Collections.Generic;

namespace Models
{
    public class Chat
    {
        public int CitizenId;
        public int RoomId;
        public string Text;
        public string CreatedAt;

        public override string ToString() => @$"[{DateTime.Parse(CreatedAt).ToString("dd.MM.yyyy hh:mm:ss")}] <{RoomId}> {CitizenId}: {Text}";
    }
}
