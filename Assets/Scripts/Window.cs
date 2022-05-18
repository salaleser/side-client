using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class Window : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.SetWindowActive(true);
        }

        private void OnDisable()
        {
            GameManager.SetWindowActive(false);
        }
    }
}
