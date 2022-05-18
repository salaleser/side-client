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
        private bool _isShiftDown;
        private Camera _camera;
        private Mouse _mouse;

        private void Start()
        {
            _camera = Camera.main;
            _mouse = Mouse.current;
        }

        private void Update()
        {
            if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
            {
                _isShiftDown = true;
            }
            else if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
            {
                _isShiftDown = false;
            }

            if (_mouse.rightButton.wasPressedThisFrame)
            {
                _camera.transform.SetParent(transform);
            }
            else if (_mouse.rightButton.wasReleasedThisFrame)
            {
                _camera.transform.SetParent(null);
            }

            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                MoveLeft();
                MoveUp();
            }
            
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                MoveLeft();
                MoveDown();
            }
            
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                MoveUp();
                MoveRight();
            }
            
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                MoveDown();
                MoveRight();
            }
            
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                var room = GameManager.Instance.me.room;
                _camera.transform.localPosition = new Vector3(room.x + Mathf.Floor(room.w / 2), 0, room.y - Mathf.Floor(room.h / 2));
            }
        }

        private void MoveLeft()
        {
            _camera.transform.localPosition -= new Vector3(_isShiftDown ? 8 : 1, 0, 0);
        }

        private void MoveRight()
        {
            _camera.transform.localPosition += new Vector3(_isShiftDown ? 8 : 1, 0, 0);
        }

        private void MoveUp()
        {
            _camera.transform.localPosition += new Vector3(0, 0, _isShiftDown ? 8 : 1);
        }

        private void MoveDown()
        {
            _camera.transform.localPosition -= new Vector3(0, 0, _isShiftDown ? 8 : 1);
        }
    }
}
