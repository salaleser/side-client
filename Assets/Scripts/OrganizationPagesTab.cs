using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class OrganizationPagesTab : MonoBehaviour
    {
        public TMP_InputField path;
        public TMP_InputField content;
        public TMP_Text contentPreview;
        public TMP_Dropdown pages;

        private void Start()
        {
            UpdatePages();
            LoadPage("");
        }

        private void OnEnable()
        {
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        private void Update()
        {
            if (GameManager.IsShortcutsActive)
            {
                if (Keyboard.current.pKey.wasPressedThisFrame)
                {
                    path.Select();
                }
                else if (Keyboard.current.aKey.wasPressedThisFrame)
                {
                    if (pages.value == pages.options.Count - 1)
                    {
                        pages.value = 0;
                    }
                    else
                    {
                        pages.value++;
                    }
                }
                else if (Keyboard.current.cKey.wasPressedThisFrame)
                {
                    content.Select();
                }
                else if (Keyboard.current.uKey.wasPressedThisFrame)
                {
                    CreatePage();
                }
            }
        }

        public void UpdatePreview()
        {
            contentPreview.text = content.text;
        }

        public void CreatePage()
        {
            NetworkManager.Instance.PageCreate(GameManager.Instance.currentOrganization.id, content.text, path.text);
            contentPreview.text = "(Preview)";
        }

        public void UpdatePages()
        {
            pages.AddOptions(GameManager.Instance.currentOrganization.pages.Select(x => new TMP_Dropdown.OptionData(x.path.ToString())).ToList());
        }

        public void LoadPage(string name)
        {
            if (name == "")
            {
                name = pages.captionText.text;
            }
            foreach (var page in GameManager.Instance.currentOrganization.pages)
            {
                if (page.path == name)
                {
                    path.text = page.path;
                    content.text = page.content;
                    break;
                }
            }
        }
    }
}
