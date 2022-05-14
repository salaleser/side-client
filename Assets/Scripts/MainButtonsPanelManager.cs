using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Models;
using TMPro;

namespace Side
{
    public class MainButtonsPanelManager : MonoBehaviour
    {
        public void Reload() => NetworkManager.Instance.ReloadButton();
        public void ShowMap() => NetworkManager.Instance.ShowMapButton();
        public void CenterMe() => NetworkManager.Instance.CenterMeButton();
        public void ZoomOut() => NetworkManager.Instance.ZoomOutButton();
        public void Profile() => NetworkManager.Instance.ProfileButton();
        public void Computer() => NetworkManager.Instance.ComputerButton();
        public void CloseWindow() => Destroy(GameObject.FindWithTag("Window"));
    }
}
