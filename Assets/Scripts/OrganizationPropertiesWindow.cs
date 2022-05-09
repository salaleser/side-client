using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace side
{
    public class OrganizationPropertiesWindow : MonoBehaviour
    {
        public TMP_InputField title;
        public TMP_Dropdown joinTypeId;

        public void Cancel()
        {
            NetworkManager.Instance.CloseWindowButton();
        }

        public void Apply()
        {
            NetworkManager.Instance.OrganizationSetTitle(GameManager.Instance.currentOrganization.id, title.text);
            NetworkManager.Instance.OrganizationSetJoin(GameManager.Instance.currentOrganization.id, joinTypeId.value);
        }

        public void Ok()
        {
            Apply();
            NetworkManager.Instance.CloseWindowButton();
        }
    }
}
