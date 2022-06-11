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
        public GameObject RadialButtonPrefab;
        public Entity Entity;

        private void Awake()
        {
            GameManager.SetRadialMenuActive(true);
        }

        private void OnDisable()
        {
            GameManager.SetRadialMenuActive(false);
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                var qbrt = RadialButtonPrefab.transform.GetComponent<RectTransform>();
                var qmrt = this.transform.GetComponent<RectTransform>();
                var startX = qmrt.transform.position.x;
                var startY = qmrt.transform.position.y;
                var endX = Mouse.current.position.x.ReadValue();
                var endY = Mouse.current.position.y.ReadValue();

                var bW = qbrt.rect.width;
                var bH = qbrt.rect.height;
                var mW = qmrt.rect.width;
                var mH = qmrt.rect.height;

                var row1 = endY >= startY + bH / 2 && endY < startY + mH / 2;
                var row2 = endY >= startY - bH / 2 && endY < startY + bH / 2;
                var row3 = endY >= startY - mH / 2 && endY < startY - bH / 2;
                var col1 = endX >= startX - mW / 2 && endX < startX - bW / 2;
                var col2 = endX >= startX - bW / 2 && endX < startX + bW / 2;
                var col3 = endX >= startX + bW / 2 && endX < startX + mW / 2;

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

                Mouse.current.WarpCursorPosition(new Vector2(startX, startY));
                Destroy(this.gameObject);
            }
        }
        
        public void UpdateButtons()
        {
            this.transform.SetParent(Entity.transform);
            this.transform.position = Mouse.current.position.ReadValue();

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
