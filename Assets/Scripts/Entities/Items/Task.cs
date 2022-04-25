using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Items
{
    public class Task : Entity, IItem
    {
        public TaskItem taskItem;

        private void Start()
        {
            if (taskItem.is_free)
            {
                AddButton($"Accept {taskItem.title}", () => NetworkManager.Instance.TaskAccept(GameManager.Instance.citizen.id, taskItem.id));
            }
        }

        public void Handler()
        {
            NetworkManager.Instance.HideAllButtons();
            NetworkManager.Instance.text.text = $"{taskItem}";
            ShowButtons();
        }
    }
}
