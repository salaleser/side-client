using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Region : Entity
{
    public RegionItem regionItem;

    private void Start()
    {
        AddButton($"Zoom in \"{regionItem.title}\"", () => NetworkManager.Instance.Region(regionItem.id));
    }
}
