using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class WindowManager : MonoBehaviour
    {
        public List<GameObject> windows;
        public List<Button> buttons;
        
        private bool isLeftControlDown;
        
        private void Start()
        {
            GameManager.SetShortcutsActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isLeftControlDown = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                isLeftControlDown = false;
            }

            if ((GameManager.IsShortcutsActive && Input.GetKeyDown(KeyCode.Q)) || Input.GetKeyDown(KeyCode.Escape))
            {
                CloseWindow();
            }

            if (GameManager.IsShortcutsActive && Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchTab(-1);
            }
            else if (GameManager.IsShortcutsActive && Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchTab(1);
            }
            else if (GameManager.IsShortcutsActive && Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchTab(2);
            }
            else if (GameManager.IsShortcutsActive && Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchTab(3);
            }
            else if (GameManager.IsShortcutsActive && Input.GetKeyDown(KeyCode.Alpha4))
            {
                SwitchTab(4);
            }
            else if (GameManager.IsShortcutsActive && Input.GetKeyDown(KeyCode.Alpha5))
            {
                SwitchTab(5);
            }
            else if (GameManager.IsShortcutsActive && Input.GetKeyDown(KeyCode.Alpha6))
            {
                SwitchTab(6);
            }
        }

        public void CloseWindow()
        {
            Destroy(this.gameObject);
            NetworkManager.Instance.CloseWindowButton();
        }

        public void SwitchTab(string name)
        {
            NetworkManager.Instance.CloseWindowButton();
            
            for (var i = 0; i < windows.Count; i++)
            {
                windows[i].SetActive(windows[i].name == name);
                buttons[i].GetComponent<Image>().color = windows[i].activeSelf ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1.0f);
            }
        }

        public void SwitchTab(int number)
        {
            if (number > windows.Count)
            {
                return;
            }

            if (number < 0)
            {
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
            }

            NetworkManager.Instance.CloseWindowButton();
            
            for (var i = 0; i < windows.Count; i++)
            {
                windows[i].SetActive(i == number - 1);
                buttons[i].GetComponent<Image>().color = windows[i].activeSelf ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1.0f);
            }
        }
    }
}
