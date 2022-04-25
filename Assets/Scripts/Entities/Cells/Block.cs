using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class Block : Entity
{
    public BlockItem blockItem;

    private void Start()
    {
        AddButton($"Zoom in \"{blockItem.title}\"", () => NetworkManager.Instance.Block(blockItem.id));
    }
}
