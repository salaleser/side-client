using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class PollPopup : MonoBehaviour
    {
        public TMP_Text Description;
        public PollItem Poll;

        private void Start()
        {
            Description.text = Poll.ToString();
        }

        public void Vote(bool vote)
        {
            NetworkManager.Instance.Vote(Poll.id, vote);
            Destroy(this.gameObject);
        }
    }
}
