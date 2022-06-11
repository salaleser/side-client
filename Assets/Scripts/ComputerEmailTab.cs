﻿using System.Collections.Generic;
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
    public class ComputerEmailTab : MonoBehaviour
    {
        public TMP_Dropdown Emails;
        public TMP_InputField EmailsCount;
        public TMP_Text Subject;
        public TMP_Text Body;
        public TMP_Text From;
        public TMP_Text Date;

        private void Update()
        {
            if (GameManager.ShortcutsActive)
            {
                if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
                {
                    Emails.Hide();
                }

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    if (Keyboard.current.leftShiftKey.isPressed)
                    {
                        Emails.Show();
                    }
                    else
                    {
                        if (Emails.value == Emails.options.Count - 1)
                        {
                            Emails.value = 0;
                        }
                        else
                        {
                            Emails.value++;
                        }
                    }
                }
                else if (Keyboard.current.dKey.wasPressedThisFrame)
                {
                    DeleteEmail();
                }
                else if (Keyboard.current.rKey.wasPressedThisFrame)
                {
                    UpdateEmails();
                }
            }
        }

        private void OnEnable()
        {
            UpdateEmails();
            this.GetComponentInParent<WindowManager>()
                .UpdateHotkeys(GameObject.FindGameObjectsWithTag("Hotkey"));
        }

        public void DeleteEmail()
        {
            var email = GameManager.Instance.Me.emails
                .Where(x => x.ToCaption() == Emails.captionText.text)
                .FirstOrDefault();
            if (email == null)
            {
                return;
            }
            var args = new string[]{email.id.ToString()};
            StartCoroutine(NetworkManager.Instance.Request("email-delete", args, (json) =>
            {
                UpdateEmails();
            }));
        }

        public void UpdateEmails()
        {
            var args = new string[]{GameManager.Instance.Me.id.ToString()};
            StartCoroutine(NetworkManager.Instance.Request("emails", args, (json) =>
            {
                var emails = JsonUtility.FromJson<EmailsResponse>(json).emails;
                Emails.ClearOptions();
                Emails.AddOptions(emails.Select(x => new TMP_Dropdown.OptionData(x.ToCaption())).ToList());
                GameManager.Instance.Me.emails = emails;
                EmailsCount.text = Emails.options.Count.ToString();
                LoadEmail();
            }));
        }

        public void LoadEmail()
        {
            Subject.text = "—";
            Body.text = "—";
            From.text = "—";
            Date.text = "—";
            foreach (var email in GameManager.Instance.Me.emails)
            {
                if (email.ToCaption() == Emails.captionText.text)
                {
                    Subject.text = email.subject;
                    Body.text = email.body;
                    From.text = $"{email.from_name}({email.from})";
                    Date.text = email.date_formatted;
                    break;
                }
            }
        }
    }
}
