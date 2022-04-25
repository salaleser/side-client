using System;
using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class TaskResponse
    {
        public List<TaskItem> tasks;
        public List<PositionItem> positions;
    }
}
