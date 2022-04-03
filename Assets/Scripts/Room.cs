using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : Entity
{
    new public RoomItem item;

    private void Start()
    {
        if (item.id == null)
        {
            var buildRoomButton = Instantiate(buttonPrefab, new Vector3(130, 270, 0), Quaternion.identity, this.transform);
            buildRoomButton.SetActive(false);
            buildRoomButton.GetComponentInChildren<Text>().text = $"Bulid Room";
            buildRoomButton.GetComponent<Button>().onClick.AddListener(() => {
                Debug.LogWarning("Build Room");
            });
            buttons.Add(buildRoomButton);
        }
        else if (GameManager.Instance.currentCitizen.floor_id == GameManager.Instance.mapFloorId && GameManager.Instance.currentCitizen.room_id != item.id)
        {
            var enterRoomButton = Instantiate(buttonPrefab, new Vector3(130, 300, 0), Quaternion.identity, this.transform);
            enterRoomButton.SetActive(false);
            enterRoomButton.GetComponentInChildren<Text>().text = $"Go to {item.title}";
            enterRoomButton.GetComponent<Button>().onClick.AddListener(() => {
                NetworkManager.Instance.Room(item.id);
            });
            buttons.Add(enterRoomButton);
        }

        switch (item.type_id)
        {
            case 1:
                var findJobButton = Instantiate(buttonPrefab, new Vector3(130, 270, 0), Quaternion.identity, this.transform);
                findJobButton.SetActive(false);
                findJobButton.GetComponentInChildren<Text>().text = $"Get Tasks";
                findJobButton.GetComponent<Button>().onClick.AddListener(() => {
                    NetworkManager.Instance.Tasks(GameManager.Instance.currentCitizen.location_id);
                });
                buttons.Add(findJobButton);
                break;
            case 3:
                var moveInsideButton = Instantiate(buttonPrefab, new Vector3(130, 270, 0), Quaternion.identity, this.transform);
                moveInsideButton.SetActive(false);
                moveInsideButton.GetComponentInChildren<Text>().text = $"Move inside";
                moveInsideButton.GetComponent<Button>().onClick.AddListener(() => {
                    NetworkManager.Instance.Room(item.id);
                });
                buttons.Add(moveInsideButton);
                break;
            case 5:
                var sleepButton = Instantiate(buttonPrefab, new Vector3(130, 270, 0), Quaternion.identity, this.transform);
                sleepButton.SetActive(false);
                sleepButton.GetComponentInChildren<Text>().text = $"Sleep";
                sleepButton.GetComponent<Button>().onClick.AddListener(() => {
                    NetworkManager.Instance.Tasks(GameManager.Instance.currentCitizen.location_id);
                });
                buttons.Add(sleepButton);
                break;
        }
    }
}
