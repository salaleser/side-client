using System;
using System.Text;
using System.Collections.Generic;

namespace Models
{
    public class Message
    {
        public int Id;
        public int SenderId;
        public string Text;
        public int ParcelId;
        public int CitizenId;
        public int OrganizationId;
        public DateTime CreatedAt;
        public long TgChatId;
        public long TgUserId;

        public Message(string data)
        {
            var elements = data.Split("◊");

            for (var i = 0; i < elements.Length; i++)
            {
                if (elements[i] == string.Empty)
                {
                    continue;
                }

                switch (i)
                {
                    case 0:
                        Id = int.Parse(elements[i]);
                        break;
                    case 1:
                        SenderId = int.Parse(elements[i]);
                        break;
                    case 2:
                        Text = elements[i];
                        break;
                    case 3:
                        ParcelId = int.Parse(elements[i]);
                        break;
                    case 4:
                        CitizenId = int.Parse(elements[i]);
                        break;
                    case 5:
                        OrganizationId = int.Parse(elements[i]);
                        break;
                    case 6:
                        CreatedAt = DateTime.Parse(elements[i]);
                        break;
                    case 7:
                        TgChatId = long.Parse(elements[i]);
                        break;
                    case 8:
                        TgUserId = long.Parse(elements[i]);
                        break;
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"<color=grey>[{CreatedAt.ToString("ddd hh:mm")}]</color> ");

            if (OrganizationId < 0)
            {
                sb.Append($"<link={OrganizationId}><color=green><u>{OrganizationId}</u></color></link>►");
            }

            sb.Append($"<link={SenderId}><color=yellow><u>{SenderId}</u></color></link>: ");
            
            if (Text.Contains("!"))
            {
                sb.Append($"<size=50><b>{Text}</b></size>");
            }
            else
            {
                sb.Append($"<b>{Text}</b>");
            }

            if (CitizenId > 0)
            {
                sb.Append($" <link=?chat:{SenderId}><color=blue><b>{{</b><u>reply</u><b>}}</b></color></link>");
            }
            else if (OrganizationId < 0)
            {
                sb.Append($" <link=?chat:{SenderId}><color=blue><b>{{</b><u>dm</u><b>}}</b></color></link>");
            }

            return sb.ToString();
        }
    }
}
