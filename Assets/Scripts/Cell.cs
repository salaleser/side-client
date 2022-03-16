using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : Location
{
    private Text _text;

    private void Start()
    {
        var textObject = GameObject.Find("Text");
        _text = textObject.GetComponent<Text>();

        var gameManagerObject = GameObject.Find("GameManager");
        _gameManager = gameManagerObject.GetComponent<GameManager>();

        _hud = GameObject.Find("HUD");

        var exitButton = Instantiate(buttonPrefab, new Vector3(x, y - 30 * 1, z), Quaternion.identity, _hud.transform);
        exitButton.SetActive(false);
        exitButton.GetComponentInChildren<Text>().text = "Exit";
        exitButton.GetComponent<Button>().onClick.AddListener(() => {
            _gameManager.ExitButton();
        });
        buttons.Add(exitButton);
    }

    private void HideAllButtons()
	{
		var objects = GameObject.FindGameObjectsWithTag("Button");
		foreach(var o in objects)
		{
			o.SetActive(false);
		}
	}

    void Update() {  
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                foreach(var cell in hit.transform.GetComponentsInChildren<Cell>())
                {
                    _text.text = cell.ToString();
                    HideAllButtons();
                }
                foreach(var location in hit.transform.GetComponentsInChildren<Location>())
                {
                    _text.text = location.location.ToString();
                    location.ShowButtons();
                }
            }
        }
    }

    public override string ToString() => @$"Cell:
    X: {x}
    Y: {y}";
}
