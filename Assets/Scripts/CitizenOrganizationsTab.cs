using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        public TMP_InputField organizationsCount;

        public void Start()
        {
            UpdateOrganizations();
        }

        public void UpdateOrganizations()
        {
            var options = GameManager.Instance.currentCitizen.organizations.Select(x => new TMP_Dropdown.OptionData(x.title)).ToList();
            organizations.AddOptions(options);
            organizationsCount.text = options.Count.ToString();
        }
    }
}
