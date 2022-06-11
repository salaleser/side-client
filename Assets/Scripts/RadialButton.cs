using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Side
{
    public class RadialButton
    {
        public string Text;
        public UnityAction Action;

        public RadialButton(string text, UnityAction action)
        {
            Text = text;
            Action = action;
        }
    }
}
