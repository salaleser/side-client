using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class ComputerWindow : Window
    {
        private void Awake()
        {
            GameManager.SetWindowActive(true);
        }
    }
}
