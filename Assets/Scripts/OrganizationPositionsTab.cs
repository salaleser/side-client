using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Entities.Items;
using Entities.Cells;
using Models;
using TMPro;

namespace Side
{
    public class OrganizationPositionsTab : MonoBehaviour
    {
        public TMP_Dropdown PositionTypes;

        public GameObject PositionPrefab;
        public GameObject Positions;
        public Button FireButton;
        public TMP_Text Description;
        public TMP_InputField Salary;

        private List<GameObject> _positions = new();
        private PositionItem _position;
        private List<PositionTypeItem> _positionTypes;

        private void OnEnable()
        {
            UpdatePositionTypes();
            UpdateButtons();
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
                {
                    PositionTypes.Hide();
                }

                if (Keyboard.current.tKey.wasPressedThisFrame)
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        PositionTypes.Show();
                    }
                    else
                    {
                        if (PositionTypes.value == PositionTypes.options.Count - 1)
                        {
                            PositionTypes.value = 0;
                        }
                        else
                        {
                            PositionTypes.value++;
                        }
                    }
                }
                else if (Keyboard.current.fKey.wasPressedThisFrame)
                {
                    Fire();
                }
            }
        }

        public void UpdateButtons()
        {
            FireButton.interactable = _position != null;
        }

        public void Fire()
        {
            NetworkManager.Instance.MemberDelete(GameManager.Instance.currentOrganization.id, _position.citizen.id);
        }

        public void SetProperties()
        {
            var args = new string[]{Salary.text};
            StartCoroutine(NetworkManager.Instance.Request("position-set-properties", args, (json) => NetworkManager.Instance.ProcessOrganization(json, "Positions")));
        }

        public void UpdatePositionTypes()
        {
            PositionTypes.ClearOptions();
            var args = new string[]{};
		    StartCoroutine(NetworkManager.Instance.Request("position-types", args, (json) => {
                _positionTypes = JsonUtility.FromJson<PositionTypesResponse>(json).position_types;
                PositionTypes.AddOptions(_positionTypes
                    .Select(x => new TMP_Dropdown.OptionData(x.ToCaption())).ToList());
                UpdatePositions();
            }));
        }

        public void UpdatePositions()
        {
            _positions.ForEach(x => Destroy(x));
            _positions = new();

            var positions = GameManager.Instance.currentOrganization.positions
                .Where(x => x.type.ToCaption() == PositionTypes.captionText.text)
                .ToList();

            var col = 0;
            var row = 0;
            for (var i = 0; i < positions.Count; i++)
            {
                var position = positions[i];

                var instance = Instantiate(PositionPrefab);
                _positions.Add(instance);
                var rectTransform = instance.transform.GetComponent<RectTransform>();
                rectTransform.transform.SetParent(Positions.transform.GetComponent<RectTransform>());
                var x = (rectTransform.rect.width * col) + rectTransform.rect.width / 2;
                var y = -(rectTransform.rect.height * row) - rectTransform.rect.height / 2;
                rectTransform.anchoredPosition = new Vector3(x, y, 0);

                var button = instance.GetComponent<Button>();
                button.GetComponentInChildren<TMP_Text>().text = $"{position.type.title} {position.citizen.ToCaption()}";
                button.onClick.AddListener(() => {
                    _position = position;
                    Description.text = _position.ToString();
                    UpdateButtons();
                    Salary.text = _position.salary.ToString();
                });

                row++;
            }
        }
    }
}
