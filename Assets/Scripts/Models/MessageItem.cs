using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class MessageItem : Item
    {
        public CitizenSimpleItem citizen;
        public string text;
        public string created_at;

        public override string ToString() => @$"[{DateTime.Parse(created_at).ToString("dd.MM.yyyy hh:mm:ss")}] {citizen.ToCaption()}: {text}";
    }
}
