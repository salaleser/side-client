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
    public class OrganizationMainTab : OrganizationTab
    {
        public TMP_InputField Title;
        public TMP_InputField MembersCount;
        public TMP_InputField IsActive;

        private void Awake()
        {
            _allowed_position_ids.Add(1);
        }

        private void OnEnable()
        {
            gameObject.SetActive(GameManager.Instance.currentOrganization.positions
                .Where(x => _allowed_position_ids.Contains(x.type.id))
                .Where(x => x.citizen.id == GameManager.Instance.me.id)
                .Any());
        }

        private void Start()
        {
            IsActive.text = GameManager.Instance.currentOrganization.is_active.ToString();
            IsActive.GetComponent<Image>().color = GameManager.Instance.currentOrganization.is_active ? Color.green : Color.red;
            Title.text = GameManager.Instance.currentOrganization.title;
            MembersCount.text = GameManager.Instance.currentOrganization.members.Count.ToString();
        }

        public void UpdateProperties()
        {
            NetworkManager.Instance.OrganizationSetTitle(GameManager.Instance.currentOrganization.id, Title.text);
        }
    }
}
