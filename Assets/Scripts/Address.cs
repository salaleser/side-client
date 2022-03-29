using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Address : Entity
{
    public AddressItem item;

    private void Start()
    {
        if (item.x != 0 && item.type_id < 4)
        {
            var zoomInButton = Instantiate(buttonPrefab, new Vector3(130, 300, 0), Quaternion.identity, this.transform);
            zoomInButton.SetActive(false);
            zoomInButton.GetComponentInChildren<Text>().text = $"Zoom in";
            zoomInButton.GetComponent<Button>().onClick.AddListener(() => {
                NetworkManager.Instance.Address(item.id);
            });
            buttons.Add(zoomInButton);
        }

        if (item.type_id > 0)
        {
            var zoomOutButton = Instantiate(buttonPrefab, new Vector3(130, 270, 0), Quaternion.identity, this.transform);
            zoomOutButton.SetActive(false);
            zoomOutButton.GetComponentInChildren<Text>().text = $"Zoom out";
            zoomOutButton.GetComponent<Button>().onClick.AddListener(() => {
                NetworkManager.Instance.Address(GameManager.Instance.mapAddressParentId);
            });
            buttons.Add(zoomOutButton);
        }
    }
}
