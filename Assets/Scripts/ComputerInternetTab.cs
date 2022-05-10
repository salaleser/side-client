using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Entities.Items;
using Entities.Cells;
using Models;
using TMPro;

namespace Side
{
    public class ComputerInternetTab : MonoBehaviour
    {
        public TMP_InputField addressBar;
        public TMP_Text content;

        void OnEnable()
        {
            addressBar.onSubmit.AddListener(LoadPage);
        }

        void OnDisable()
        {
            addressBar.onSubmit.RemoveListener(LoadPage);
        }

        public void LoadPage(string text)
        {
            var a = text.Split("/");

            var address = a[0];

            var path = "";
            if (a.Length > 1)
            {
                path = a[1];
            }

            NetworkManager.Instance.Page(address, path);
        }
    }
}
