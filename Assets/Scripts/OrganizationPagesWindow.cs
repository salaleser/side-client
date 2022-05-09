using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace side
{
    public class OrganizationPagesWindow : MonoBehaviour
    {
        public TMP_InputField path;
        public TMP_InputField content;
        public TMP_Text contentPreview;

        void OnEnable()
        {
            content.onSubmit.AddListener(UpdatePreview);
        }

        void OnDisable()
        {
            content.onSubmit.RemoveListener(UpdatePreview);
        }

        private void UpdatePreview(string text)
        {
            contentPreview.text = text;
        }

        public void CreatePage()
        {
            NetworkManager.Instance.CreatePage(GameManager.Instance.currentOrganization.id, content.text, path.text);
            contentPreview.text = "(Preview)";
        }
    }
}
