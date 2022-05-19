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
        public TMP_InputField Path;
        public TMP_InputField Content;
        public TMP_Text ContentPreview;
        public TMP_Dropdown Pages;

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
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
                {
                    Pages.Hide();
                }

                if (Keyboard.current.pKey.wasPressedThisFrame)
                {
                    Path.Select();
                }
                else if (Keyboard.current.aKey.wasPressedThisFrame)
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        Pages.Show();
                    }
                    else
                    {
                        if (Pages.value == Pages.options.Count - 1)
                        {
                            Pages.value = 0;
                        }
                        else
                        {
                            Pages.value++;
                        }
                    }
                }
                else if (Keyboard.current.cKey.wasPressedThisFrame)
                {
                    Content.Select();
                }
                else if (Keyboard.current.uKey.wasPressedThisFrame)
                {
                    CreatePage();
                }
            }
        }

        public void UpdatePreview()
        {
            ContentPreview.text = Content.text;
        }

        public void CreatePage()
        {
            NetworkManager.Instance.PageCreate(GameManager.Instance.currentOrganization.id, Content.text, Path.text);
            ContentPreview.text = "(Preview)";
        }

        public void UpdatePages()
        {
            Pages.AddOptions(GameManager.Instance.currentOrganization.pages.Select(x => new TMP_Dropdown.OptionData(x.path.ToString())).ToList());
        }

        public void LoadPage(string name)
        {
            if (name == "")
            {
                name = Pages.captionText.text;
            }
            foreach (var page in GameManager.Instance.currentOrganization.pages)
            {
                if (page.path == name)
                {
                    Path.text = page.path;
                    Content.text = page.content;
                    break;
                }
            }
        }
    }
}
