using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Block : Entity
    {
        public BlockItem blockItem;

        private void Start()
        {
            AddButton($"Zoom in \"{blockItem.title}\"", () => NetworkManager.Instance.Block(blockItem.id));
            foreach(var block in GameManager.Instance.city.blocks)
            {
                if ((Math.Abs(blockItem.x - block.x) == 1 && blockItem.y == block.y) ||
                    (Math.Abs(blockItem.y - block.y) == 1 && blockItem.x == block.x))
                {
                    AddButton($"Expand Block", () => NetworkManager.Instance.CreateBlock(blockItem.city_id, blockItem.x, blockItem.y));
                }
            }
        }
    }
}
