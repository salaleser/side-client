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
            if (blockItem.explorer_id == 0)
            {
                AddButton($"Explore Block", () => NetworkManager.Instance.BlockExplore(blockItem.id));
            }
            else
            {
                AddButton($"Zoom in \"{blockItem.title}\"", () => NetworkManager.Instance.Block(blockItem.id));
            }
        }
        
        private void OnMouseEnter()
        {
            NetworkManager.Instance.text.text = $"\n\n{blockItem}";
        }
    }
}
