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
using Side;

namespace Side
{
    public class WebBrowser : MonoBehaviour
    {
        public TMP_InputField AddressBar;
        public TMP_InputField SearchBar;
        public TMP_Text Content;

        private List<string> _history = new();
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = transform.GetComponent<RectTransform>();
        }

        private void Start()
        {
            _rectTransform.sizeDelta = new Vector2(Screen.width / 2, Screen.height);
        }

        private void OnEnable()
        {
            AddressBar.onSubmit.AddListener(LoadPage);
            SearchBar.onSubmit.AddListener(Search);
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
                if (Keyboard.current.leftCtrlKey.isPressed && Keyboard.current.lKey.wasPressedThisFrame)
                {
                    AddressBar.Select();
                }
                else if (Keyboard.current.leftCtrlKey.isPressed && Keyboard.current.fKey.wasPressedThisFrame)
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

        public void LoadPage(string internetAddress)
        {
            var (address, path) = GameManager.ParseInternetAddress(internetAddress);
            var args = new string[]{GameManager.Instance.Citizen.id.ToString(), address, path};
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

        public void Back()
        {
            _history.RemoveAt(_history.Count() - 1);
            LoadPage(_history.Last());
        }

        public void LoadPath(string path)
        {
            LoadPage($"{AddressBar.text.Split("/")[0]}/{path}");
        }
    }
}
