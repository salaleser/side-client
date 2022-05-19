using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class OrganizationWindow : Window
    {
        public OrganizationItem organization;
        public TMP_InputField title;
        public TMP_Dropdown joinTypeId;
        public TMP_InputField membersCount;
        public TMP_InputField isActive;
        public Button join;

        public void Awake()
        {
            GameManager.SetWindowActive(true);
            organization = GameManager.Instance.currentOrganization;
        }

        public void Start()
        {
            isActive.text = organization.is_active.ToString();
            isActive.GetComponent<Image>().color = organization.is_active ? Color.green : Color.red;
            title.text = organization.title;
            joinTypeId.value = organization.properties.join_type_id;
            membersCount.text = organization.members.Count.ToString();
            join.interactable = !organization.members.Any(x => x.citizen.id == GameManager.Instance.me.id);
        }

        public void Apply()
        {
            NetworkManager.Instance.OrganizationSetTitle(GameManager.Instance.currentOrganization.id, title.text);

            OrganizationProperties properties = GameManager.Instance.currentOrganization.properties;
            properties.join_type_id = joinTypeId.value;
            NetworkManager.Instance.OrganizationSetProperties(GameManager.Instance.currentOrganization.id, properties);
        }

        public void Join()
        {
            NetworkManager.Instance.MemberCreate(organization.id, GameManager.Instance.me.id);
        }
    }
}
