using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public GameObject buttonPrefab;
    public List<GameObject> buttons = new();

    protected object item;

    private const int X = 130;
    private const int Y = 300;

    public void AddButton(string text, UnityAction action)
    {
        var rect = buttonPrefab.transform.GetComponent<RectTransform>().rect;
        var pos = new Vector3(X, Y - rect.height * buttons.Count, 0);
        var button = Instantiate(buttonPrefab, pos, Quaternion.identity, this.transform);
        button.SetActive(false);
        button.GetComponentInChildren<Text>().text = text;
        button.GetComponent<Button>().onClick.AddListener(action);
        buttons.Add(button);
    }

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
