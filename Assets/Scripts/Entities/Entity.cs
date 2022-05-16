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

    private void OnMouseEnter()
    {
        // NetworkManager.Instance.text.text = $"{this}";
    }

    private void OnMouseOver()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (GameObject.FindWithTag("Window") == null && GameObject.FindWithTag("Popup") == null)
            {
                NetworkManager.Instance.InstantiateQuickMenu(this.transform);
            }
        }
    }
}
