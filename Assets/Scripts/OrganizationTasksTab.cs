using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Entities.Items;
using Entities.Cells;
using Models;
using TMPro;

namespace Side
{
    public class OrganizationTasksTab : MonoBehaviour
    {
        public GameObject TaskPrefab;
        public GameObject Tasks;
        public TMP_InputField Wage;
        public TMP_Text Description;

        public TaskItem _task;

        private void OnEnable()
        {
            UpdateTasks();
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        public void SetProperties()
        {
            var args = new string[]{_task.id.ToString(), Wage.text};
            StartCoroutine(NetworkManager.Instance.Request("task-set-properties", args, (result) => {
                GameManager.Instance.currentOrganization = JsonUtility.FromJson<OrganizationResponse>(result).organization;
                UpdateTasks();
            }));
        }

        public void UpdateTasks()
        {
            var ts = GameManager.Instance.currentOrganization.tasks;
            var tasksRectTransform = Tasks.transform.GetComponent<RectTransform>();

            var col = 0;
            var row = 0;
            for (var i = 0; i < ts.Count; i++)
            {
                var task = ts[i];

                var instance = Instantiate(TaskPrefab);
                instance.GetComponent<Image>().color = task.is_free ? Color.grey : Color.blue;
                var rectTransform = instance.transform.GetComponent<RectTransform>();
                rectTransform.transform.SetParent(tasksRectTransform);
                var x = (rectTransform.rect.width * col) + rectTransform.rect.width / 2;
                var y = -(rectTransform.rect.height * row) - rectTransform.rect.height / 2;
                rectTransform.anchoredPosition = new Vector3(x, y, 0);

                var button = instance.GetComponent<Button>();
                button.GetComponentInChildren<TMP_Text>().text = $"\"{task.title}\" ({task.organization_id}-{task.room_id}) ={task.wage} [{(task.is_free ? "O" : "X")}]";
                button.onClick.AddListener(() => {
                    _task = task;
                    Description.text = _task.ToString();
                });

                row++;
            }
        }
    }
}
