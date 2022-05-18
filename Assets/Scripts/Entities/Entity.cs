using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Side;

public class Entity : MonoBehaviour
{
    public List<QuickButton> QuickButtons = new();

    public void AddButton(string text, UnityAction action, bool interactable = true)
    {
        QuickButtons.Add(interactable ? new QuickButton(text, action) : null);
    }

    private void OnMouseOver()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!GameManager.WindowActive && !GameManager.PopupActive)
            {
                NetworkManager.Instance.InstantiateQuickMenu(this.transform);
            }
        }
    }
}
