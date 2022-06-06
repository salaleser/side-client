using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Entities.Items;
using Entities.Cells;
using Models;
using TMPro;

namespace Side
{
    public class ComputerInternetTab : MonoBehaviour
    {
        public TMP_InputField AddressBar;
        public TMP_InputField SearchBar;
        public TMP_Text Content;

        private void OnEnable()
        {
            AddressBar.onSubmit.AddListener(LoadPage);
            SearchBar.onSubmit.AddListener(Search);
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        private void OnDisable()
        {
            AddressBar.onSubmit.RemoveListener(LoadPage);
            SearchBar.onSubmit.RemoveListener(Search);
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.aKey.wasPressedThisFrame)
                {
                    AddressBar.Select();
                }
                else if (Keyboard.current.sKey.wasPressedThisFrame)
                {
                    SearchBar.Select();
                }
            }
        }

        public void Search(string text)
        {
            var args = new string[]{text};
            StartCoroutine(NetworkManager.Instance.Request("page-search", args, (json) => {
                var searchResult = JsonUtility.FromJson<SearchResultResponse>(json).search_result;
                Content.text = searchResult;
            }));
        }

        public void LoadPage(string text)
        {
            var a = text.Split("/");

            var address = a[0];

            var path = "root";
            if (a.Length > 1)
            {
                path = a[1];
            }

            AddressBar.text = $"{address}/{path}";

            var args = new string[]{GameManager.Instance.me.id.ToString(), address, path};
            StartCoroutine(NetworkManager.Instance.Request("page", args, (json) => {
                var page = JsonUtility.FromJson<PageResponse>(json).page;
                Content.text = page.content;
                AddressBar.text = $"{page.address}/{page.path}";
            }));
        }

        public void ReloadPage()
        {
            LoadPage(AddressBar.text);
        }

        public void LoadRootPage()
        {
            LoadPath("root");
        }

        public void LoadPath(string path)
        {
            LoadPage($"{AddressBar.text.Split("/")[0]}/{path}");
        }
    }
}
