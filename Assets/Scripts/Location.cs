using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location : Entity
{
    public LocationItem item;

    private void Start()
    {
		if (item.floors.Count > 0)
        {
            if (item.owner_id == GameManager.Instance.citizen.id)
            {
                AddButton($"Zoom in {item.type_title}", () => NetworkManager.Instance.Location(item.id));
            }
            else
            {
                AddButton($"Move inside {item.type_title}", () => {
                    foreach(var f in item.floors)
                    {
                        if (f.number == 0)
                        {
                            foreach(var r in f.rooms)
                            {
                                if (r.type_id == RoomType.Lobby)
                                {
                                    NetworkManager.Instance.Room(r.id);
                                }
                            }
                        }
                    }
                });
            }
        }
    }
}
