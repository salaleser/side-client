using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location : Entity
{
    public LocationItem item;

    private void Start()
    {
		if (item.floors_count > 0)
        {
            var floorButton = Instantiate(buttonPrefab, new Vector3(130, 330, 0), Quaternion.identity, this.transform);
            floorButton.SetActive(false);
            floorButton.GetComponentInChildren<Text>().text = $"Go to {item.title}";
            floorButton.GetComponent<Button>().onClick.AddListener(() => {
                NetworkManager.Instance.Floor(item.id, 0);
            });
            buttons.Add(floorButton);
        }
    }
}
