using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace side
{
    public class WebBrowser : MonoBehaviour
    {
        public TMP_InputField addressBar;
        public TMP_Text content;

        void OnEnable()
        {
            addressBar.onSubmit.AddListener(LoadPage);
        }

        void OnDisable()
        {
            addressBar.onSubmit.RemoveListener(LoadPage);
        }

        public void LoadPage(string text)
        {
            Debug.Log(text);
            NetworkManager.Instance.Page(text);
        }
    }
}
