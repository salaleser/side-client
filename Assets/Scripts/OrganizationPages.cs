using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace side
{
    public class OrganizationPages : MonoBehaviour
    {
        public TMP_InputField content;

        void OnEnable()
        {
            content.onSubmit.AddListener(CreatePage);
        }

        void OnDisable()
        {
            content.onSubmit.RemoveListener(CreatePage);
        }

        public void CreatePage(string text)
        {
            NetworkManager.Instance.CreatePage(GameManager.Instance.currentOrganization.id, text, "index");
        }
    }
}
