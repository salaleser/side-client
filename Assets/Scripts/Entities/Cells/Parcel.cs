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
            if (parcelItem.owner_id == 0)
            {
                AddButton($"Claim Parcel", () => NetworkManager.Instance.ParcelClaim(parcelItem.id));
            }
            else
            {
                AddButton($"Zoom in \"{parcelItem.title}\"", () => NetworkManager.Instance.Parcel(parcelItem.id));
            }
        }
    }
}
