using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Citizen : Entity
{
    public CitizenItem item;

    protected const int x = 130;
    protected const int y = 600;
    protected const int z = 0;

    private void Start()
    {
        var gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        hud = GameObject.Find("HUD");

		var enterButton = Instantiate(buttonPrefab, new Vector3(x, y - 30 * 0, z), Quaternion.identity, hud.transform);
        enterButton.SetActive(false);
        enterButton.GetComponentInChildren<Text>().text = "Inventory";
        enterButton.GetComponent<Button>().onClick.AddListener(() => {
            gameManager.InventoryButton(item.id);
        });
        buttons.Add(enterButton);
    }
}
