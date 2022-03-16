using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public Location location;
    public GameObject buttonPrefab;
    public List<GameObject> buttons = new();

    private GameManager _gameManager;
    private GameObject _hud;

    private void Start()
    {
        var gameManagerObject = GameObject.Find("GameManager");
       _gameManager = gameManagerObject.GetComponent<GameManager>();

        var _hud = GameObject.Find("HUD");

        var x = 100;
		var y = 300;
		var z = 0;

		var enterButton = Instantiate(buttonPrefab, new Vector3(x, y - 30 * 0, z), Quaternion.identity, _hud.transform);
        enterButton.SetActive(false);
        enterButton.GetComponentInChildren<Text>().text = "Enter";
        enterButton.GetComponent<Button>().onClick.AddListener(() => {
            _gameManager.EnterButton(location.id);
        });
        buttons.Add(enterButton);

        var exitButton = Instantiate(buttonPrefab, new Vector3(x, y - 30 * 1, z), Quaternion.identity, _hud.transform);
        exitButton.SetActive(false);
        exitButton.GetComponentInChildren<Text>().text = "Exit";
        exitButton.GetComponent<Button>().onClick.AddListener(() => {
            _gameManager.ExitButton(location.parent_id);
        });
        buttons.Add(exitButton);
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
