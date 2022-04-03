using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationType : Entity
{
    new public LocationTypeItem item;

    private void Start()
    {
        var acceptTaskButton = Instantiate(buttonPrefab, new Vector3(130, 300, 0), Quaternion.identity, NetworkManager.Instance.buildCanvas.transform);
        acceptTaskButton.SetActive(false);
        acceptTaskButton.GetComponentInChildren<Text>().text = $"Build {item.title}";
        acceptTaskButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.RegisterLocation(item.address_id, item.id);
        });
        buttons.Add(acceptTaskButton);
    }
}
