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
    public class OrganizationMembersTab : MonoBehaviour
    {
        public GameObject MemberPrefab;
        public GameObject Members;
        public TMP_Text Description;

        public Button KickButton;

        private MemberItem _member;

        private void OnEnable()
        {
            UpdateMembers();
            UpdateButtons();
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
            }
        }

        public void UpdateButtons()
        {
            KickButton.interactable = _member != null;
        }

        public void Kick()
        {
            NetworkManager.Instance.MemberDelete(GameManager.Instance.currentOrganization.id, _member.citizen.id);
        }

        public void UpdateMembers()
        {
            var members = GameManager.Instance.currentOrganization.members;

            var col = 0;
            var row = 0;
            for (var i = 0; i < members.Count; i++)
            {
                var member = members[i];

                var instance = Instantiate(MemberPrefab);
                var rectTransform = instance.transform.GetComponent<RectTransform>();
                rectTransform.transform.SetParent(Members.transform.GetComponent<RectTransform>());
                var x = (rectTransform.rect.width * col) + rectTransform.rect.width / 2;
                var y = -(rectTransform.rect.height * row) - rectTransform.rect.height / 2;
                rectTransform.anchoredPosition = new Vector3(x, y, 0);

                var button = instance.GetComponent<Button>();
                button.GetComponentInChildren<TMP_Text>().text = $"{member.title} {member.citizen.ToCaption()}";
                button.onClick.AddListener(() => {
                    _member = member;
                    Description.text = _member.ToString();
                    UpdateButtons();
                });

                row++;
            }
        }
    }
}
