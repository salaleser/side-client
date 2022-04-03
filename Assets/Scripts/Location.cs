using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location : Entity
{
    new public LocationItem item;

    private void Start()
    {
		if (item.floors.Count > 0)
        {
            if (item.owner_id == GameManager.Instance.citizen.id)
            {
                AddButton("Zoom in", () => NetworkManager.Instance.Location(item.id));
            }
            else
            {
                AddButton("Move inside", () => {
                    foreach(var f in item.floors)
                    {
                        if (f.number == 0)
                        {
                            foreach(var r in f.rooms)
                            {
                                if (r.type_id == RoomTypes.Lobbys)
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
