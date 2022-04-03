using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : Entity
{
    new public TaskItem item;

    private void Start()
    {
        if (item.is_free)
        {
            AddButton($"Accept {item.title}", () => NetworkManager.Instance.TaskAccept(GameManager.Instance.citizen.id, item.id));
        }
    }
}
