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
        private const int M = 5;

        private Camera _camera;
        private Mouse _mouse;
        private Keyboard _keyboard;

        private void Start()
        {
            _camera = Camera.main;
            _mouse = Mouse.current;
            _keyboard = Keyboard.current;
        }

        private void Update()
        {
            if (!GameManager.WindowActive && !GameManager.PopupActive)
            {
                if (_mouse.rightButton.wasPressedThisFrame)
                {
                    _camera.transform.localPosition = transform.position;
                }

                if (_mouse.position.x.ReadValue() > Screen.width - 50)
                {
                    MoveDown(0.05f);
                    MoveRight(0.05f);
                }
                else if (_mouse.position.x.ReadValue() < 50)
                {
                    MoveLeft(0.05f);
                    MoveUp(0.05f);
                }

                if (_mouse.position.y.ReadValue() > Screen.height - 50)
                {
                    MoveUp(0.05f);
                    MoveRight(0.05f);
                }
                else if (_mouse.position.y.ReadValue() < 50)
                {
                    MoveLeft(0.05f);
                    MoveDown(0.05f);
                }

                if (_keyboard.leftArrowKey.wasPressedThisFrame)
                {
                    MoveLeft();
                    MoveUp();
                }
                
                if (_keyboard.downArrowKey.wasPressedThisFrame)
                {
                    MoveLeft();
                    MoveDown();
                }
                
                if (_keyboard.upArrowKey.wasPressedThisFrame)
                {
                    MoveUp();
                    MoveRight();
                }
                
                if (_keyboard.rightArrowKey.wasPressedThisFrame)
                {
                    MoveDown();
                    MoveRight();
                }
                
                if (_keyboard.spaceKey.wasPressedThisFrame)
                {
                    var room = GameManager.Instance.me.room;
                    _camera.transform.localPosition = new Vector3(room.x + Mathf.Floor(room.w / 2), 0, room.y - Mathf.Floor(room.h / 2));
                }
            }
        }

        private void MoveLeft(float m = 1.0f)
        {
            _camera.transform.localPosition -= new Vector3(_keyboard.leftShiftKey.isPressed ? M : 1, 0, 0) * m;
        }

        private void MoveRight(float m = 1.0f)
        {
            _camera.transform.localPosition += new Vector3(_keyboard.leftShiftKey.isPressed ? M : 1, 0, 0) * m;;
        }

        private void MoveUp(float m = 1.0f)
        {
            _camera.transform.localPosition += new Vector3(0, 0, _keyboard.leftShiftKey.isPressed ? M : 1) * m;;
        }

        private void MoveDown(float m = 1.0f)
        {
            _camera.transform.localPosition -= new Vector3(0, 0, _keyboard.leftShiftKey.isPressed ? M : 1) * m;;
        }
    }
}
