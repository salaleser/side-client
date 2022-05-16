using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class Task : Entity
    {
        public TaskItem taskItem;

        private void Start()
        {
            if (taskItem.is_free)
            {
                AddButton($"Accept {taskItem.title}", () => NetworkManager.Instance.TaskAccept(GameManager.Instance.me.id, taskItem.id));
            }
        }
    }
}
