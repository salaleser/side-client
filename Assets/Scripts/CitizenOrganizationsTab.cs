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
    public class CitizenOrganizationsTab : MonoBehaviour
    {
        public TMP_Dropdown Organizations;
        public TMP_Dropdown OrganizationTypes;

        private TMP_Text _description;
        private List<OrganizationTypeItem> _organizationTypes;

        private void Start()
        {
            _description = GameObject.Find("MainDescription").GetComponent<TMP_Text>();
            UpdateOrganizations();
            UpdateOrganizationTypes();
        }

        private void OnEnable()
        {
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
                {
                    Organizations.Hide();
                    OrganizationTypes.Hide();
                }

                if (Keyboard.current.oKey.wasPressedThisFrame)
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        Organizations.Show();
                    }
                    else
                    {
                        if (Organizations.value == Organizations.options.Count - 1)
                        {
                            Organizations.value = 0;
                        }
                        else
                        {
                            Organizations.value++;
                        }
                    }
                }
                else if (Keyboard.current.tKey.wasPressedThisFrame)
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        OrganizationTypes.Show();
                    }
                    else
                    {
                        if (OrganizationTypes.value == OrganizationTypes.options.Count - 1)
                        {
                            OrganizationTypes.value = 0;
                        }
                        else
                        {
                            OrganizationTypes.value++;
                        }
                    }
                }
                else if (Keyboard.current.mKey.wasPressedThisFrame)
                {
                    ManageOrganization();
                }
                else if (Keyboard.current.cKey.wasPressedThisFrame)
                {
                    CreateOrganization();
                }
            }
        }

        public void UpdateOrganizations()
        {
            Organizations.AddOptions(GameManager.Instance.me.organizations
                .Select(x => new TMP_Dropdown.OptionData(x.ToCaption()))
                .ToList());
        }

        public void UpdateOrganizationTypes()
        {
            var args = new string[]{};
            StartCoroutine(NetworkManager.Instance.Request("organization-types", args, (result) => {
                _organizationTypes = JsonUtility.FromJson<OrganizationTypesResponse>(result).organization_types;
                OrganizationTypes.AddOptions(_organizationTypes
                    .Select(x => new TMP_Dropdown.OptionData(x.ToCaption()))
                    .ToList());
            }));
        }

        public void ManageOrganization()
        {
            var organization = GameManager.Instance.me.organizations
                .Where(x => x.ToCaption() == Organizations.captionText.text)
                .FirstOrDefault();
            if (organization != null)
            {
                NetworkManager.Instance.Organization(organization.id);
                Destroy(this.gameObject);
            }
        }

        public void CreateOrganization()
        {
            var organizationTypeId = _organizationTypes
                .Where(x => x.ToCaption() == OrganizationTypes.captionText.text)
                .Select(x => x.id)
                .FirstOrDefault();
            NetworkManager.Instance.OrganizationCreate(organizationTypeId);
        }

        public void UpdateOrganizationTypeDescription()
        {
            _description.text = _organizationTypes
                .Where(x => x.ToCaption() == OrganizationTypes.captionText.text)
                .FirstOrDefault()
                ?.ToString();
        }

        public void UpdateOrganizationDescription()
        {
            _description.text = GameManager.Instance.me.organizations
                .Where(x => x.ToCaption() == Organizations.captionText.text)
                .FirstOrDefault()
                ?.ToString();
        }
    }
}
