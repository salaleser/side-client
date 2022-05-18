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
        public TMP_InputField membershipFee;
        public TMP_Dropdown members;
        public TMP_InputField membersCount;
        public TMP_InputField isActive;
        public Button kick;
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
            membershipFee.text = organization.properties.membership_fee.ToString();
            members.AddOptions(organization.members.Select(x => new TMP_Dropdown.OptionData(x.citizen_id.ToString())).ToList());
            membersCount.text = organization.members.Count.ToString();
            
            join.interactable = !organization.members.Any(x => x.citizen_id == GameManager.Instance.me.id);
        }

        public void Apply()
        {
            NetworkManager.Instance.OrganizationSetTitle(GameManager.Instance.currentOrganization.id, title.text);

            OrganizationProperties properties = new();
            properties.join_type_id = joinTypeId.value;
            properties.membership_fee = int.Parse(membershipFee.text);
            NetworkManager.Instance.OrganizationSetProperties(GameManager.Instance.currentOrganization.id, properties);
        }

        public void SetKickButtonInteractable(bool interactable) => kick.interactable = interactable;

        public void KickMember()
        {
            int memberId;
            if (int.TryParse(members.captionText.text, out memberId))
            {
                NetworkManager.Instance.MemberDelete(organization.id, memberId);
            }
        }

        public void Join()
        {
            NetworkManager.Instance.MemberCreate(organization.id, GameManager.Instance.me.id);
        }
    }
}
