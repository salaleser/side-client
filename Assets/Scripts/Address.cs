using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Address : Entity
{
    public AddressItem item;

    private void Start()
    {
        if (item.type_id < 4)
        {
            var addressButton = Instantiate(buttonPrefab, new Vector3(130, 300, 0), Quaternion.identity, this.transform);
            addressButton.SetActive(false);
            addressButton.GetComponentInChildren<Text>().text = $"Inspect {item.title}";
            addressButton.GetComponent<Button>().onClick.AddListener(() => {
                NetworkManager.Instance.Address(item.id);
            });
            buttons.Add(addressButton);
        }
    }
}
