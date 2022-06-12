using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Side;

namespace Side
{
    public class Application : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.SetWindowActive(true);
        }

        private void OnDisable()
        {
            GameManager.SetWindowActive(false);
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (!GameManager.PopupActive)
                {
                    if (Keyboard.current.qKey.wasPressedThisFrame
                        || Keyboard.current.escapeKey.wasPressedThisFrame)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
