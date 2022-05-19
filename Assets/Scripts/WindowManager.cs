using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class WindowManager : MonoBehaviour
    {
        public List<GameObject> windows;
        public List<Button> buttons;

        public GameObject[] _hotkeys;
        
        private void Start()
        {
            GameManager.SetShortcutsActive(true);
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (!GameManager.PopupActive)
                {
                    if (Keyboard.current.qKey.wasPressedThisFrame || Keyboard.current.escapeKey.wasPressedThisFrame)
                    {
                        CloseWindow();
                    }
                }
                
                if (Keyboard.current.hKey.wasPressedThisFrame)
                {
                    ToggleHotkeys(true);
                }
                else if (Keyboard.current.hKey.wasReleasedThisFrame)
                {
                    ToggleHotkeys(false);
                }
                
                if (Keyboard.current.tabKey.wasPressedThisFrame)
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        PreviousTab();
                    }
                    else
                    {
                        NextTab();
                    }
                }
            }
        }

        public void UpdateHotkeys(GameObject[] hotkeys)
        {
            _hotkeys = hotkeys;
            ToggleHotkeys(false);
        }

        public void ToggleHotkeys(bool isActive)
        {
            foreach (var go in _hotkeys)
            {
                go.SetActive(isActive);
                // go.GetComponent<TMP_Text>().color = isActive ?
                //     new Color(1.0f, 0.0f, 0.0f, 1.0f) : new Color(0.0f, 0.0f, 0.0f, 0.05f);
            }
        }

        public void CloseWindow()
        {
            Destroy(this.gameObject);
        }

        public void SwitchTab(string name)
        {
            for (var i = 0; i < windows.Count; i++)
            {
                windows[i].SetActive(windows[i].name == name);
                buttons[i].GetComponent<Image>().color = windows[i].activeSelf ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1.0f);
            }
        }

        public void NextTab()
        {
            var number = 0;
            for (var i = 0; i < windows.Count; i++)
            {
                if (windows[i].activeSelf)
                {
                    number = i + 1 + 1;
                    if (number > windows.Count)
                    {
                        number = 1;
                    }
                    break;
                }
            }
            SwitchTab(number);
        }

        public void PreviousTab()
        {
            var number = 0;
            for (var i = 0; i < windows.Count; i++)
            {
                if (windows[i].activeSelf)
                {
                    number = i;
                    if (number == 0)
                    {
                        number = windows.Count;
                    }
                    break;
                }
            }
            SwitchTab(number);
        }

        public void SwitchTab(int number)
        {
            if (number > windows.Count)
            {
                return;
            }

            for (var i = 0; i < windows.Count; i++)
            {
                windows[i].SetActive(i == number - 1);
                buttons[i].GetComponent<Image>().color = windows[i].activeSelf ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1.0f);
            }
        }
    }
}
