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
    public class OrganizationMembersTab : OrganizationTab
    {
        public GameObject MemberPrefab;
        public GameObject Members;
        public TMP_Dropdown JoinType;
        public Button KickButton;
        public TMP_InputField CitizenId;

        private MemberItem _member;

        private void Awake()
        {
            _allowed_position_ids.Add(2);
        }

        private void OnEnable()
        {
            // gameObject.SetActive(GameManager.Instance.currentOrganization.positions
            //     .Where(x => _allowed_position_ids.Contains(x.type.id))
            //     .Where(x => x.citizen.id == GameManager.Instance.me.id)
            //     .Any());
            // UpdateMembers();
            // UpdateButtons();
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        public void Start()
        {
            // JoinType.value = GameManager.Instance.currentOrganization.properties.join_type_id;
        }

        public void UpdateButtons()
        {
            KickButton.interactable = _member != null;
        }

        public void Kick()
        {
            // NetworkManager.Instance.MemberDelete(GameManager.Instance.currentOrganization.id, _member.citizen.id);
        }

        public void Invite()
        {
            NetworkManager.Instance.InviteCreate(GameManager.Instance.me.id, GameManager.Instance.currentOrganization.id, int.Parse(CitizenId.text));
        }

        public void SetJoinType()
        {
            // OrganizationProperties properties = GameManager.Instance.currentOrganization.properties;
            // properties.join_type_id = JoinType.value;
            // NetworkManager.Instance.OrganizationSetProperties(GameManager.Instance.currentOrganization.id, properties, "Members");
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
                    GameManager.SetDescription(_member.ToString());
                    UpdateButtons();
                });

                row++;
            }
        }
    }
}
