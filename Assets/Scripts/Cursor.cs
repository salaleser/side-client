using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

namespace Side
{
    public class Cursor : MonoBehaviour
    {
        public TMP_Text command;

        private bool _commandMode;
        private int _commandX;
        private int _commandY;

        private bool _isShiftDown;

        private void Update() {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (GameObject.FindWithTag("QuickMenu") == null)
                {
                    var entity = hit.transform.GetComponent<Entity>();
                    if (entity != null)
                    {
                        this.transform.SetPositionAndRotation(entity.transform.position, Quaternion.identity);
                        GameManager.Instance.cursorX = (int)entity.transform.position.x;
                        GameManager.Instance.cursorY = (int)entity.transform.position.y;
                        GameManager.Instance.cursorZ = (int)entity.transform.position.z;
                    }
                }
            }

            if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
            {
                _isShiftDown = true;
            }
            else if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
            {
                _isShiftDown = false;
            }

            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                Camera.main.transform.localPosition -= new Vector3(_isShiftDown ? 8 : 1, 0, 0);
            }
            
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                Camera.main.transform.localPosition -= new Vector3(0, 0, _isShiftDown ? 8 : 1);
            }
            
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                Camera.main.transform.localPosition += new Vector3(0, 0, _isShiftDown ? 8 : 1);
            }
            
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                Camera.main.transform.localPosition += new Vector3(_isShiftDown ? 8 : 1, 0, 0);
            }
            
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                var room = GameManager.Instance.me.room;
                Camera.main.transform.localPosition = new Vector3(room.x + Mathf.Floor(room.w / 2), 0, room.y - Mathf.Floor(room.h / 2));
            }
        }
    }
}
