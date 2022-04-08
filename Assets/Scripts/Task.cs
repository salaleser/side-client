using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Task : Entity, IItem
{
    public TaskItem item;

    private void Start()
    {
        if (item.is_free)
        {
            AddButton($"Accept {item.title}", () => NetworkManager.Instance.TaskAccept(GameManager.Instance.citizen.id, item.id));
        }
    }

    public void Handler()
    {
        NetworkManager.Instance.HideAllButtons();
        NetworkManager.Instance.text.text = $"{item}";
        ShowButtons();
    }
}
