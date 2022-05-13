using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class PopupManager : MonoBehaviour
    {
        public void ClosePopup()
        {
            Destroy(this.gameObject);
        }
    }
}
