using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using Entities;
using TMPro;

namespace Side
{
    public class RadialMenuController : MonoBehaviour
    {
        private const int RotateSpeed = 5;
        private const float ZoomSpeed = 5f;
        private const float ZoomMin = 1f;
        private const float ZoomMax = 32f;

        public GameObject RadialButtonPrefab;
        public Entity Entity;

        private Camera _camera;
        private Mouse _mouse;
        private Keyboard _keyboard;

        private RectTransform _qbrt;
        private RectTransform _qmrt;
        private float _startX;
        private float _startY;
        private float _bW;
        private float _bH;
        private float _mW;
        private float _mH;
        private float _targetZoom;

        private void Awake()
        {
            GameManager.SetRadialMenuActive(true);
        }

        private void OnDisable()
        {
            GameManager.SetRadialMenuActive(false);
        }

        private void Start()
        {
            _camera = Camera.main;
            _mouse = Mouse.current;
            _keyboard = Keyboard.current;

            _startX = _mouse.position.x.ReadValue();
            _startY = _mouse.position.y.ReadValue();

            _qbrt = RadialButtonPrefab.transform.GetComponent<RectTransform>();
            _qmrt = transform.GetComponent<RectTransform>();

            _bW = _qbrt.rect.width;
            _bH = _qbrt.rect.height;
            _mW = _qmrt.rect.width;
            _mH = _qmrt.rect.height;

            UpdateButtons();
        }

        private void Update()
        {
            if (_mouse.rightButton.isPressed)
            {
                if (_mouse.position.x.ReadValue() < _startX - 200)
                {
                    _camera.transform.RotateAround(GameManager.Instance.Cursor.transform.position, new Vector3(0, -1, 0), 30 * (_keyboard.leftShiftKey.isPressed ? RotateSpeed : 1) * Time.deltaTime);
                }
                else if (_mouse.position.x.ReadValue() > _startX + 200)
                {
                    _camera.transform.RotateAround(GameManager.Instance.Cursor.transform.position, new Vector3(0, 1, 0), 30 * (_keyboard.leftShiftKey.isPressed ? RotateSpeed : 1) * Time.deltaTime);
                }
                
                if (_mouse.position.y.ReadValue() < _startY - 200)
                {
                    _targetZoom -= _mouse.position.y.ReadValue() + _startY + 200 * ZoomSpeed;
                    _targetZoom = Mathf.Clamp(_targetZoom, ZoomMax, ZoomMin);
                    _camera.orthographicSize = Mathf.MoveTowards(_camera.orthographicSize, _targetZoom, ZoomSpeed * Time.deltaTime);
                }
                else if (_mouse.position.y.ReadValue() > _startY + 200)
                {
                    _targetZoom += _mouse.position.y.ReadValue() + _startY + 200 * ZoomSpeed;
                    _targetZoom = Mathf.Clamp(_targetZoom, ZoomMax, ZoomMin);
                    _camera.orthographicSize = Mathf.MoveTowards(_camera.orthographicSize, _targetZoom, ZoomSpeed * Time.deltaTime);
                }
            }

            if (_mouse.rightButton.wasReleasedThisFrame)
            {
                var endX = _mouse.position.x.ReadValue();
                var endY = _mouse.position.y.ReadValue();

                var row1 = endY >= _startY + _bH / 2 && endY < _startY + _mH / 2;
                var row2 = endY >= _startY - _bH / 2 && endY < _startY + _bH / 2;
                var row3 = endY >= _startY - _mH / 2 && endY < _startY - _bH / 2;
                var col1 = endX >= _startX - _mW / 2 && endX < _startX - _bW / 2;
                var col2 = endX >= _startX - _bW / 2 && endX < _startX + _bW / 2;
                var col3 = endX >= _startX + _bW / 2 && endX < _startX + _mW / 2;

                var num1 = row1 && col1;
                var num2 = row1 && col2;
                var num3 = row1 && col3;
                var num4 = row2 && col1;
                var num5 = row2 && col2;
                var num6 = row2 && col3;
                var num7 = row3 && col1;
                var num8 = row3 && col2;
                var num9 = row3 && col3;

                var buttonNumber = num1 ? 1
                    : num2 ? 2
                    : num3 ? 3
                    : num4 ? 4
                    : num5 ? 5
                    : num6 ? 6
                    : num7 ? 7
                    : num8 ? 8
                    : num9 ? 9 : 0;

                if (buttonNumber > 0 && buttonNumber <= Entity.RadialButtons.Count)
                {
                    Entity.RadialButtons[buttonNumber-1]?.Action();
                }

                _mouse.WarpCursorPosition(new Vector2(_startX, _startY));
                Destroy(gameObject);
            }
        }
        
        public void UpdateButtons()
        {
            transform.SetParent(Entity.transform);
            transform.position = _mouse.position.ReadValue();

            var col = 0;
            var row = 0;
            for (var i = 0; i < Entity.RadialButtons.Count; i++)
            {
                if (i > 0 && i % 3 == 0)
                {
                    row++;
                    col = 0;
                }

                if (Entity.RadialButtons[i] == null)
                {
                    col++;
                    continue;
                }

                var radialButtonInstance = Instantiate(RadialButtonPrefab);
                var button = radialButtonInstance.GetComponent<Button>();
                button.GetComponentInChildren<Text>().text = Entity.RadialButtons[i].Text;
                button.onClick.AddListener(Entity.RadialButtons[i].Action);
                var rectTransform = radialButtonInstance.transform.GetComponent<RectTransform>();
                radialButtonInstance.transform.SetParent(this.transform);

                var x = (rectTransform.rect.width * col) + rectTransform.rect.width / 2;
                var y = -(rectTransform.rect.height * row) - rectTransform.rect.height / 2;
                rectTransform.anchoredPosition = new Vector3(x, y, 0);

                col++;
            }
        }
    }
}
