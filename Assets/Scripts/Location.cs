using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    public LocationItem location;
    public GameObject buttonPrefab;
    public List<GameObject> buttons = new();

    protected const int x = 100;
    protected const int y = 300;
    protected const int z = 0;

    protected GameManager _gameManager;
    protected GameObject _hud;

    private void Start()
    {
        var gameManagerObject = GameObject.Find("GameManager");
        _gameManager = gameManagerObject.GetComponent<GameManager>();

        _hud = GameObject.Find("HUD");

		var enterButton = Instantiate(buttonPrefab, new Vector3(x, y - 30 * 0, z), Quaternion.identity, _hud.transform);
        enterButton.SetActive(false);
        enterButton.GetComponentInChildren<Text>().text = "Enter";
        enterButton.GetComponent<Button>().onClick.AddListener(() => {
            _gameManager.EnterButton(location.id);
        });
        buttons.Add(enterButton);
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
