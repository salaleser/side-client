using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Side;

public class Entity : MonoBehaviour
{
	public List<RadialButton> RadialButtons = new();

    public void AddButton(string text, UnityAction action, bool interactable = true)
    {
        RadialButtons.Add(interactable ? new RadialButton(text, action) : null);
    }

    private void OnMouseOver()
    {
        if (!GameManager.WindowActive && !GameManager.PopupActive)
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                NetworkManager.Instance.InstantiateRadialMenu(transform);
            }
        }
    }
}
