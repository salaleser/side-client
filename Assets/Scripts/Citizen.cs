using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Citizen : Entity
{
    public CitizenItem item;

    protected const int x = 130;
    protected const int y = 300;
    protected const int z = 0;

    private void Start()
    {
		var inventoryButton = Instantiate(buttonPrefab, new Vector3(x, y - 30 * 0, z), Quaternion.identity, NetworkManager.Instance.hud.transform);
        inventoryButton.SetActive(false);
        inventoryButton.GetComponentInChildren<Text>().text = "Inventory";
        inventoryButton.GetComponent<Button>().onClick.AddListener(() => {
		    NetworkManager.Instance.Inventory(item.root_item_id);
        });
        buttons.Add(inventoryButton);
    }
}
