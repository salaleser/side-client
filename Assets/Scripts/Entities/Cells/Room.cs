using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Room : Entity
{
    public RoomItem roomItem;

    private void Start()
    {
        if (roomItem.id == 0)
        {
            AddButton($"Build Room", () => NetworkManager.Instance.RoomTypes());
        }
        else if (GameManager.Instance.citizen.room_id == roomItem.id)
        {
            switch (roomItem.type_id)
            {
                case 1:
                    AddButton("Get Tasks", () => NetworkManager.Instance.Tasks(GameManager.Instance.citizen.organization_id));
                    break;
            }
        }
        else if (roomItem.id > 0)
        {
            AddButton($"Go to \"{roomItem.description}\"", () => NetworkManager.Instance.MoveIntoRoom(roomItem.id));
        }
    }
}
