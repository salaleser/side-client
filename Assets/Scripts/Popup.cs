using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class Popup : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.SetPopupActive(true);
        }

        private void OnDisable()
        {
            GameManager.SetPopupActive(false);
        }
    }
}
