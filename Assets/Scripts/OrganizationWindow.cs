using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class OrganizationWindow : MonoBehaviour
    {
        public List<GameObject> windows;
        public List<Button> buttons;
        public OrganizationItem organization;
        public TMP_InputField title;
        public TMP_Dropdown joinTypeId;
        public TMP_InputField membershipFee;
        public TMP_Dropdown members;
        public TMP_InputField membersCount;
        public TMP_InputField isActive;
        public Button kick;
        public Button join;

        private bool isLeftControlDown;

        public void Awake()
        {
            organization = GameManager.Instance.currentOrganization;
        }

        public void Start()
        {
            SwitchTab("Main");

            isActive.text = organization.is_active.ToString();
            isActive.GetComponent<Image>().color = organization.is_active ? Color.green : Color.red;
            title.text = organization.title;
            joinTypeId.value = organization.properties.join_type_id;
            membershipFee.text = organization.properties.membership_fee.ToString();
            members.AddOptions(organization.members.Select(x => new TMP_Dropdown.OptionData(x.citizen_id.ToString())).ToList());
            membersCount.text = organization.members.Count.ToString();
            
            join.interactable = !organization.members.Any(x => x.citizen_id == GameManager.Instance.me.id);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isLeftControlDown = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                isLeftControlDown = false;
            }

            if ((isLeftControlDown && Input.GetKeyDown(KeyCode.Q)) || Input.GetKeyDown(KeyCode.Escape))
            {
                CloseWindow();
            }

            if (isLeftControlDown && Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchTab(-1);
            }
            else if (isLeftControlDown && Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchTab(1);
            }
            else if (isLeftControlDown && Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchTab(2);
            }
            else if (isLeftControlDown && Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchTab(3);
            }
            else if (isLeftControlDown && Input.GetKeyDown(KeyCode.Alpha4))
            {
                SwitchTab(4);
            }
            else if (isLeftControlDown && Input.GetKeyDown(KeyCode.Alpha5))
            {
                SwitchTab(5);
            }
            else if (isLeftControlDown && Input.GetKeyDown(KeyCode.Alpha6))
            {
                SwitchTab(6);
            }
        }

        public void Cancel()
        {
            CloseWindow();
        }

        public void Apply()
        {
            NetworkManager.Instance.OrganizationSetTitle(GameManager.Instance.currentOrganization.id, title.text);

            OrganizationProperties properties = new();
            properties.join_type_id = joinTypeId.value;
            properties.membership_fee = int.Parse(membershipFee.text);
            NetworkManager.Instance.OrganizationSetProperties(GameManager.Instance.currentOrganization.id, properties);
        }

        public void Ok()
        {
            Apply();
            CloseWindow();
        }

        public void SetKickButtonInteractable(bool interactable) => kick.interactable = interactable;

        public void KickMember()
        {
            int memberId;
            if (int.TryParse(members.captionText.text, out memberId))
            {
                NetworkManager.Instance.MemberDelete(organization.id, memberId);
                CloseWindow();
            }
        }

        public void Join()
        {
            NetworkManager.Instance.MemberCreate(organization.id, GameManager.Instance.me.id);
            CloseWindow();
        }

        public void CloseWindow()
        {
            Destroy(this.gameObject);
            NetworkManager.Instance.CloseWindowButton();
        }

        public void SwitchTab(string name)
        {
            NetworkManager.Instance.CloseWindowButton();
            
            for (var i = 0; i < windows.Count; i++)
            {
                windows[i].SetActive(windows[i].name == name);
                buttons[i].GetComponent<Image>().color = windows[i].activeSelf ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1.0f);
            }
        }

        public void SwitchTab(int number)
        {
            if (number > windows.Count)
            {
                return;
            }

            if (number < 0)
            {
                for (var i = 0; i < windows.Count; i++)
                {
                    if (windows[i].activeSelf)
                    {
                        number = i + 1 + 1;
                        if (number > windows.Count)
                        {
                            number = 1;
                        }
                        break;
                    }
                }
            }

            NetworkManager.Instance.CloseWindowButton();
            
            for (var i = 0; i < windows.Count; i++)
            {
                windows[i].SetActive(i == number - 1);
                buttons[i].GetComponent<Image>().color = windows[i].activeSelf ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1.0f);
            }
        }
    }
}
