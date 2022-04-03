using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public object item;
    public GameObject buttonPrefab;
    public List<GameObject> buttons = new();

    public void ShowButtons()
    {
        foreach(var b in buttons)
        {
            b.SetActive(true);
        }
    }

    public void HideButtons()
    {
        foreach(var b in buttons)
        {
            b.SetActive(false);
        }
    }

    public void Handler()
    {
        NetworkManager.Instance.HideAllButtons();
        NetworkManager.Instance.text.text = $"{item}";
        ShowButtons();
    }
}
