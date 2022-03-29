using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Citizen : Entity
{
    public CitizenItem item;

    private void Start()
    {
		var inventoryButton = Instantiate(buttonPrefab, new Vector3(130, 270, 0), Quaternion.identity, this.transform);
        inventoryButton.SetActive(false);
        inventoryButton.GetComponentInChildren<Text>().text = $"Inventory ({item.name})";
        inventoryButton.GetComponent<Button>().onClick.AddListener(() => {
		    NetworkManager.Instance.Inventory(item.root_item_id);
        });
        buttons.Add(inventoryButton);
    }
}
