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
        public TMP_Dropdown organizations;
        public TMP_Dropdown organizationTypes;
        public TMP_Text description;

        private List<OrganizationTypeItem> _organizationTypes;

        private void Start()
        {
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
                    organizations.Hide();
                    organizationTypes.Hide();
                }

                if (Keyboard.current.oKey.wasPressedThisFrame)
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        organizations.Show();
                    }
                    else
                    {
                        if (organizations.value == organizations.options.Count - 1)
                        {
                            organizations.value = 0;
                        }
                        else
                        {
                            organizations.value++;
                        }
                    }
                }
                else if (Keyboard.current.tKey.wasPressedThisFrame)
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        organizationTypes.Show();
                    }
                    else
                    {
                        if (organizationTypes.value == organizationTypes.options.Count - 1)
                        {
                            organizationTypes.value = 0;
                        }
                        else
                        {
                            organizationTypes.value++;
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
            organizations.AddOptions(GameManager.Instance.me.organizations
                .Select(x => new TMP_Dropdown.OptionData(x.ToCaption()))
                .ToList());
        }

        public void UpdateOrganizationTypes()
        {
            var args = new string[]{};
            StartCoroutine(NetworkManager.Instance.Request("organization-types", args, (result) => {
                _organizationTypes = JsonUtility.FromJson<OrganizationTypesResponse>(result).organization_types;
                organizationTypes.AddOptions(_organizationTypes
                    .Select(x => new TMP_Dropdown.OptionData(x.ToCaption()))
                    .ToList());
            }));
        }

        public void ManageOrganization()
        {
            var organization = GameManager.Instance.me.organizations
                .Where(x => x.ToCaption() == organizations.captionText.text)
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
                .Where(x => x.ToCaption() == organizationTypes.captionText.text)
                .Select(x => x.id)
                .FirstOrDefault();
            NetworkManager.Instance.OrganizationCreate(organizationTypeId);
        }

        public void UpdateOrganizationTypeDescription()
        {
            description.text = _organizationTypes
                .Where(x => x.ToCaption() == organizationTypes.captionText.text)
                .FirstOrDefault()
                ?.ToString();
        }

        public void UpdateOrganizationDescription()
        {
            description.text = GameManager.Instance.me.organizations
                .Where(x => x.ToCaption() == organizations.captionText.text)
                .FirstOrDefault()
                ?.ToString();
        }
    }
}
