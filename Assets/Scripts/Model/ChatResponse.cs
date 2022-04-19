using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class ChatResponse
    {
        public List<MessageItem> messages = new();
    }
}
