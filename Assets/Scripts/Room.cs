using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : Entity
{
    public RoomItem item;

    private void Start()
    {
        if (item.type_id > 0 && GameManager.Instance.currentCitizen.room_id != item.id)
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
            case 0:
                var buildRoomButton = Instantiate(buttonPrefab, new Vector3(130, 270, 0), Quaternion.identity, this.transform);
                buildRoomButton.SetActive(false);
                buildRoomButton.GetComponentInChildren<Text>().text = $"Bulid Room";
                buildRoomButton.GetComponent<Button>().onClick.AddListener(() => {
                    Debug.LogWarning("Build Room");
                });
                buttons.Add(buildRoomButton);
                break;
            case 1:
                var findJobButton = Instantiate(buttonPrefab, new Vector3(130, 270, 0), Quaternion.identity, this.transform);
                findJobButton.SetActive(false);
                findJobButton.GetComponentInChildren<Text>().text = $"Get Tasks";
                findJobButton.GetComponent<Button>().onClick.AddListener(() => {
                    NetworkManager.Instance.Tasks(GameManager.Instance.currentCitizen.location_id);
                });
                buttons.Add(findJobButton);
                break;
        }
    }
}
