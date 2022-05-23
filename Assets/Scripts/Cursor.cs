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

                if (_keyboard.leftBracketKey.wasPressedThisFrame)
                {
                    _camera.transform.RotateAround(transform.position, new Vector3(0, 1, 0), 90);
                }
                else if (_keyboard.rightBracketKey.wasPressedThisFrame)
                {
                    _camera.transform.RotateAround(transform.position, new Vector3(0, -1, 0), 90);
                }

                if (_mouse.rightButton.isPressed)
                {
                    if (_mouse.position.x.ReadValue() > Screen.width / 2 + 100)
                    {
                        _camera.transform.RotateAround(transform.position, new Vector3(0, 1, 0), 100 * (_keyboard.leftShiftKey.isPressed ? M : 1) * Time.deltaTime);
                    }
                    else if (_mouse.position.x.ReadValue() < Screen.width / 2 - 100)
                    {
                        _camera.transform.RotateAround(transform.position, new Vector3(0, -1, 0), 100 * (_keyboard.leftShiftKey.isPressed ? M : 1) * Time.deltaTime);
                    }
                }

                if (_mouse.position.x.ReadValue() > Screen.width - 5)
                {
                    MoveDown(0.05f);
                    MoveRight(0.05f);
                }
                else if (_mouse.position.x.ReadValue() < 5)
                {
                    MoveLeft(0.05f);
                    MoveUp(0.05f);
                }

                if (_mouse.position.y.ReadValue() > Screen.height - 5)
                {
                    MoveUp(0.05f);
                    MoveRight(0.05f);
                }
                else if (_mouse.position.y.ReadValue() < 5)
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
