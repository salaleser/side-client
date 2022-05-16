using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Side
{
    public class QuickButton
    {
        public string Text;
        public UnityAction Action;

        public QuickButton(string text, UnityAction action)
        {
            Text = text;
            Action = action;
        }
    }
}
