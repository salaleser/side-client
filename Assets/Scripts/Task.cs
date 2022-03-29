using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : Entity
{
    public TaskItem item;

    private void Start()
    {
        if (item.is_free)
        {
            var acceptTaskButton = Instantiate(buttonPrefab, new Vector3(130, 30, 0), Quaternion.identity, NetworkManager.Instance.tasksCanvas.transform);
            acceptTaskButton.SetActive(false);
            acceptTaskButton.GetComponentInChildren<Text>().text = $"Accept {item.title}";
            acceptTaskButton.GetComponent<Button>().onClick.AddListener(() => {
                NetworkManager.Instance.TaskAccept(GameManager.Instance.currentCitizen.id, item.id);
            });
            buttons.Add(acceptTaskButton);
        }
    }

    public void Handler()
    {
        NetworkManager.Instance.HideAllButtons();
        NetworkManager.Instance.text.text = $"{item}";
        ShowButtons();
    }   
}