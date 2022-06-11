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
    public class OrganizationPositionsTab : OrganizationTab
    {
        public TMP_Dropdown PositionTypes;
        public GameObject PositionPrefab;
        public GameObject Positions;
        public Button FireButton;
        public TMP_InputField Salary;
        public TMP_InputField CitizenId;

        private List<GameObject> _positions = new();
        // private PositionItem _position;
        // private List<PositionTypeItem> _positionTypes;

        private void Awake()
        {
            _allowed_position_ids.Add(1);
            _allowed_position_ids.Add(2);
        }

        private void OnEnable()
        {
            // gameObject.SetActive(GameManager.Instance.currentOrganization.positions
            //     .Where(x => _allowed_position_ids.Contains(x.type.id))
            //     .Where(x => x.citizen.id == GameManager.Instance.me.id)
            //     .Any());
            // UpdateButtons();
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        public void Start()
        {
            UpdatePositionTypes();
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
            // FireButton.interactable = _position != null;
        }

        public void Fire()
        {
            // var args = new string[]{_position.id.ToString(), ""};
            // StartCoroutine(NetworkManager.Instance.Request("position-set-citizen", args, (json) => {
            //     NetworkManager.Instance.ProcessOrganization(json, "Positions");
            // }));
        }

        public void Offer()
        {
            // NetworkManager.Instance.OfferCreate(GameManager.Instance.me.id, _position.id, int.Parse(CitizenId.text));
        }

        public void SetProperties()
        {
            var args = new string[]{Salary.text};
            StartCoroutine(NetworkManager.Instance.Request("position-set-properties", args, null));
        }

        public void UpdatePositionTypes()
        {
            PositionTypes.ClearOptions();
            // var args = new string[]{};
		    // StartCoroutine(NetworkManager.Instance.Request("position-types", args, (json) => {
            //     _positionTypes = JsonUtility.FromJson<PositionTypesResponse>(json).position_types;
            //     PositionTypes.AddOptions(_positionTypes
            //         .Select(x => new TMP_Dropdown.OptionData(x.ToCaption())).ToList());
            //     UpdatePositions();
            // }));
        }

        public void UpdatePositions()
        {
            // _positions.ForEach(x => Destroy(x));
            // _positions = new();

            // var positions = GameManager.Instance.currentOrganization.positions
            //     .Where(x => x.type.ToCaption() == PositionTypes.captionText.text)
            //     .ToList();

            // var col = 0;
            // var row = 0;
            // for (var i = 0; i < positions.Count; i++)
            // {
            //     var position = positions[i];

            //     var instance = Instantiate(PositionPrefab);
            //     instance.GetComponent<Image>().color = position.citizen.id == 0 ? Color.grey : Color.blue;
            //     _positions.Add(instance);
            //     var rectTransform = instance.transform.GetComponent<RectTransform>();
            //     rectTransform.transform.SetParent(Positions.transform.GetComponent<RectTransform>());
            //     var x = (rectTransform.rect.width * col) + rectTransform.rect.width / 2;
            //     var y = -(rectTransform.rect.height * row) - rectTransform.rect.height / 2;
            //     rectTransform.anchoredPosition = new Vector3(x, y, 0);

            //     var button = instance.GetComponent<Button>();
            //     button.GetComponentInChildren<TMP_Text>().text = $"{position.type.title}: {(position.citizen.id == 0 ? "(vacant)" : position.citizen.ToCaption())}";
            //     button.onClick.AddListener(() => {
            //         _position = position;
            //         GameManager.SetDescription(_position.ToString());
            //         UpdateButtons();
            //         Salary.text = _position.salary.ToString();
            //     });

            //     row++;
            // }
        }
    }
}
