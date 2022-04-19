using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Room : Entity
{
    public RoomItem item;

    private void Start()
    {
        if (GameManager.Instance.citizen.floor_id == GameManager.Instance.floor.id)
        {
            if (item.id != 0 && GameManager.Instance.citizen.room_id != item.id)
            {
                AddButton($"Go to {item.title}", () => NetworkManager.Instance.Room(item.id));
            }
        }
        else if (item.type_id == RoomTypes.Lobby)
        {
            AddButton("Move inside", () => NetworkManager.Instance.Room(item.id));
        }

        if (GameManager.Instance.citizen.room_id == item.id)
        {
            switch (item.type_id)
            {
                case RoomTypes.HrDepartment:
                    AddButton("Get Tasks", () => NetworkManager.Instance.Tasks(GameManager.Instance.citizen.location_id));
                    break;
                case RoomTypes.Bedroom:
                    AddButton("Sleep", () => Debug.LogWarning("Sleep"));
                    break;
                case RoomTypes.Office:
                    switch (item.location_type_id)
                    {
                        case LocationTypes.CityHall:
                        AddButton("Test", () => Debug.LogWarning("test"));
                        break;
                    }
                    break;
            }
        }
    }
}
