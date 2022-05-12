using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public GameObject buttonPrefab;
    public List<GameObject> buttons = new();

    private const int X = 130;
    private const int Y = 300;

    // private void Update()
    // {
    //     if (buttons.Count > 0 && Input.GetKeyUp(KeyCode.Alpha1))
	// 	{
    //         if (buttons[0].activeSelf)
    //         {
	// 		    buttons[0].GetComponent<Button>().onClick.Invoke();
    //         }
	// 	}
    //     else if (buttons.Count > 1 && Input.GetKeyUp(KeyCode.Alpha2))
	// 	{
    //         if (buttons[1].activeSelf)
    //         {
	// 		    buttons[1].GetComponent<Button>().onClick.Invoke();
    //         }
	// 	}
    //     else if (buttons.Count > 2 && Input.GetKeyUp(KeyCode.Alpha3))
	// 	{
    //         if (buttons[2].activeSelf)
    //         {
	// 		    buttons[2].GetComponent<Button>().onClick.Invoke();
    //         }
	// 	}
    //     else if (buttons.Count > 3 && Input.GetKeyUp(KeyCode.Alpha4))
	// 	{
    //         if (buttons[3].activeSelf)
    //         {
	// 		    buttons[3].GetComponent<Button>().onClick.Invoke();
    //         }
	// 	}
    // }

    public void AddButton(string text, UnityAction action, bool interactable = true)
    {
        var rect = buttonPrefab.transform.GetComponent<RectTransform>().rect;
        var pos = new Vector3(X, Y - rect.height * buttons.Count, 0);
        var button = Instantiate(buttonPrefab, pos, Quaternion.identity, this.transform);
        button.name = $"Button.{buttons.Count} ({text})";
        button.SetActive(false);
        button.GetComponentInChildren<Text>().text = $"[{buttons.Count + 1}] {text}";
        button.GetComponent<Button>().onClick.AddListener(action);
        button.GetComponent<Button>().interactable = interactable;
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
}
