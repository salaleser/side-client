using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : Entity
{
    new public RoomItem item;

    private void Start()
    {
        if (GameManager.Instance.citizen.id == GameManager.Instance.location.owner_id)
        {
            if (item.id == 0)
            {
                AddButton("Bulid Room", () => Debug.LogWarning("Build Room"));
            }
            else
            {
                AddButton("Change Room", () => Debug.LogWarning("Change Room"));
            }
        }
        
        if (GameManager.Instance.citizen.floor_id == GameManager.Instance.floor.id)
        {
            if (item.id != 0 && GameManager.Instance.citizen.room_id != item.id)
            {
                AddButton($"Go to {item.title}", () => NetworkManager.Instance.Room(item.id));
            }
        }
        else if (item.type_id == 3)
        {
            AddButton("Move inside", () => NetworkManager.Instance.Room(item.id));
        }

        if (GameManager.Instance.citizen.room_id == item.id)
        {
            switch (item.type_id)
            {
                case 1:
                    AddButton("Get Tasks", () => NetworkManager.Instance.Tasks(GameManager.Instance.citizen.location_id));
                    break;
                case 5:
                    AddButton("Sleep", () => Debug.LogWarning("Sleep"));
                    break;
            }
        }
    }
}
