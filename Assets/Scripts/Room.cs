using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : Entity
{
    public RoomItem item;

    private void Start()
    {
        if (GameManager.Instance.currentCitizen.room_id != item.id)
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
        }
    }
}
