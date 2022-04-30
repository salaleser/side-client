using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Continent : Entity
{
    public ContinentItem continentItem;

    private void Start()
    {
        AddButton($"Zoom in \"{continentItem.title}\"", () => NetworkManager.Instance.Continent(continentItem.id));
    }
}
