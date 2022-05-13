using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
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
        public TMP_InputField searchBar;
        public TMP_Text content;

        private void OnEnable()
        {
            addressBar.onSubmit.AddListener(LoadPage);
            searchBar.onSubmit.AddListener(Search);
        }

        private void OnDisable()
        {
            addressBar.onSubmit.RemoveListener(LoadPage);
            searchBar.onSubmit.RemoveListener(Search);
        }

        private void Update()
        {
            if (GameManager.IsShortcutsActive)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    addressBar.Select();
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    searchBar.Select();
                }
            }
        }

        public void Search(string text)
        {
            NetworkManager.Instance.Exec("search", new string[]{"q",text});
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
