using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

namespace Entities.Cells
{
    public class Parcel : Entity
    {
        public ParcelItem parcelItem;

        private void Start()
        {
            AddButton($"Zoom in \"{parcelItem.title}\"", () => NetworkManager.Instance.Parcel(parcelItem.id));
            AddButton($"Claim Parcel", () => NetworkManager.Instance.CreateParcel(parcelItem.block_id, parcelItem.x, parcelItem.y));
        }
    }
}
