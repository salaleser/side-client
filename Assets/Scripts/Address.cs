using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Address : Entity
{
    new public AddressItem item;

    private void Start()
    {
        if (item.id != 0)
        {
            if (item.type_id < 4)
            {
                var zoomInButton = Instantiate(buttonPrefab, new Vector3(130, 300, 0), Quaternion.identity, this.transform);
                zoomInButton.SetActive(false);
                zoomInButton.GetComponentInChildren<Text>().text = $"Zoom in";
                zoomInButton.GetComponent<Button>().onClick.AddListener(() => {
                    NetworkManager.Instance.Address(item.id);
                });
                buttons.Add(zoomInButton);
            }

            if (item.type_id == 4 && item.location == null)
            {
                var registerLocationButton = Instantiate(buttonPrefab, new Vector3(130, 270, 0), Quaternion.identity, this.transform);
                registerLocationButton.SetActive(false);
                registerLocationButton.GetComponentInChildren<Text>().text = $"Build Location";
                registerLocationButton.GetComponent<Button>().onClick.AddListener(() => {
                    NetworkManager.Instance.Build(item.id);
                });
                buttons.Add(registerLocationButton);
            }
        }
        else
        {
            var registerAddressButton = Instantiate(buttonPrefab, new Vector3(130, 300, 0), Quaternion.identity, this.transform);
            registerAddressButton.SetActive(false);
            registerAddressButton.GetComponentInChildren<Text>().text = $"Build Address";
            registerAddressButton.GetComponent<Button>().onClick.AddListener(() => {
                NetworkManager.Instance.RegisterAddress(GameManager.Instance.mapAddressId, item.x, item.y, "");
            });
            buttons.Add(registerAddressButton);
        }
    }
}
