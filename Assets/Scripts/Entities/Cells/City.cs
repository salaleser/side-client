using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class City : Entity
{
    public CityItem cityItem;

    private void Start()
    {
        AddButton($"Zoom in \"{cityItem.title}\"", () => NetworkManager.Instance.City(cityItem.id));
    }
}
