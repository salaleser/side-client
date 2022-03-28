using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Address : Entity
{
    public AddressItem item;

    private void Start()
    {
        var enterButton = Instantiate(buttonPrefab, new Vector3(130, 300, 0), Quaternion.identity, this.transform);
        enterButton.SetActive(false);
        enterButton.GetComponentInChildren<Text>().text = "Enter";
        enterButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.Location(item.id);
        });
        buttons.Add(enterButton);
    }
}
