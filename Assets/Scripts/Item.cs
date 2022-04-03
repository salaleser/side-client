using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : Entity
{
    new public ItemItem item;

    private void Start()
    {
        var marketButton = Instantiate(buttonPrefab, new Vector3(130, 300, 0), Quaternion.identity, NetworkManager.Instance.inventoryCanvas.transform);
        marketButton.SetActive(false);
        marketButton.GetComponentInChildren<Text>().text = "Market";
        marketButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.Market();
        });
        buttons.Add(marketButton);

        var openButton = Instantiate(buttonPrefab, new Vector3(130, 270, 0), Quaternion.identity, NetworkManager.Instance.inventoryCanvas.transform);
        openButton.SetActive(false);
        openButton.GetComponentInChildren<Text>().text = "Open";
        openButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.Inventory(item.id);
        });
        buttons.Add(openButton);
    }
}
