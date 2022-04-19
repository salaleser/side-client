using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class TaskResponse
    {
        public List<TaskItem> tasks = new();
        public List<PositionItem> positions = new();
    }
}
