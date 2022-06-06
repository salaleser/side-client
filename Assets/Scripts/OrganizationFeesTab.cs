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
    public class OrganizationFeesTab : OrganizationTab
    {
        public TMP_InputField MembershipFee;

        private void Awake()
        {
            _allowed_position_ids.Add(3);
        }

        private void OnEnable()
        {
            // gameObject.SetActive(GameManager.Instance.currentOrganization.positions
            //     .Where(x => _allowed_position_ids.Contains(x.type.id))
            //     .Where(x => x.citizen.id == GameManager.Instance.me.id)
            //     .Any());
            // MembershipFee.text = GameManager.Instance.currentOrganization.properties.membership_fee.ToString();
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
