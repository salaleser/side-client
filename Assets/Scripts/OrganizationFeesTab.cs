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
    public class OrganizationFeesTab : MonoBehaviour
    {
        public TMP_InputField MembershipFee;

        private void OnEnable()
        {
            var properties = GameManager.Instance.currentOrganization.properties;
            MembershipFee.text = properties.membership_fee.ToString();
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        public void SetProperties()
        {
            OrganizationProperties properties = GameManager.Instance.currentOrganization.properties;
            properties.membership_fee = int.Parse(MembershipFee.text);
            NetworkManager.Instance.OrganizationSetProperties(GameManager.Instance.currentOrganization.id, properties, "Fees");
        }
    }
}
