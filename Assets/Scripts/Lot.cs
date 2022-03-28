using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lot : Entity
{
    public LotItem item;

    private void Start()
    {
        var exitButton = Instantiate(buttonPrefab, new Vector3(130, 30, 0), Quaternion.identity, NetworkManager.Instance.hud.transform);
        exitButton.SetActive(false);
        exitButton.GetComponentInChildren<Text>().text = "Exit";
        exitButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.ExitButton();
        });
        buttons.Add(exitButton);

        var openButton = Instantiate(buttonPrefab, new Vector3(130, 120, 0), Quaternion.identity, NetworkManager.Instance.hud.transform);
        openButton.SetActive(false);
        openButton.GetComponentInChildren<Text>().text = "Open";
        openButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.Inventory(item.item_id);
        });
        buttons.Add(openButton);
    }

    public void Handler()
    {
        NetworkManager.Instance.HideAllButtons();
        NetworkManager.Instance.text.text = $"{item}";
        ShowButtons();
    }   
}
