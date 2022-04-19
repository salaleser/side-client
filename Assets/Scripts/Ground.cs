using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Ground : Entity
{
    public GroundItem item;

    private void Start()
    {
        if (item.address == null)
        {
            AddButton("Build Address", () => NetworkManager.Instance.RegisterAddress(GameManager.Instance.address.id, item.x, item.y, ""));
        }
    }
}
