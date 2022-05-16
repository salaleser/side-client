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
    public class ComputerEmailTab : MonoBehaviour
    {
        public TMP_Dropdown emails;
        public TMP_InputField emailsCount;
        public TMP_Text subject;
        public TMP_Text body;
        public TMP_Text from;
        public TMP_Text date;

        private void Start()
        {
            StartCoroutine(NetworkManager.Instance.Request("emails", $"citizen_id={GameManager.Instance.me.id}", (json) =>
            {
                var response = JsonUtility.FromJson<EmailsResponse>(json);
                emails.AddOptions(response.emails.Select(x => new TMP_Dropdown.OptionData(x.ToString())).ToList());
                GameManager.Instance.me.emails = response.emails;
            }));
        }

        private void OnEnable()
        {
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        public void LoadEmail()
        {
            foreach (var email in GameManager.Instance.me.emails)
            {
                if (email.ToString() == emails.captionText.text)
                {
                    subject.text = email.subject;
                    body.text = email.body;
                    from.text = email.from.ToString();
                    date.text = email.date.ToShortDateString();
                    break;
                }
            }
        }
    }
}
