using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location : Entity
{
    new public LocationItem item;

    private void Start()
    {
		if (item.floors.Count > 0)
        {
            if (item.id == GameManager.Instance.currentCitizen.location_id || item.owner_id == GameManager.Instance.currentCitizen.id)
            {
                var zoomInButton = Instantiate(buttonPrefab, new Vector3(130, 330, 0), Quaternion.identity, this.transform);
                zoomInButton.SetActive(false);
                zoomInButton.GetComponentInChildren<Text>().text = $"Zoom in";
                zoomInButton.GetComponent<Button>().onClick.AddListener(() => {
                    NetworkManager.Instance.Location(item.id);
                });
                buttons.Add(zoomInButton);
            }
            else
            {
                var moveInsideButton = Instantiate(buttonPrefab, new Vector3(130, 330, 0), Quaternion.identity, this.transform);
                moveInsideButton.SetActive(false);
                moveInsideButton.GetComponentInChildren<Text>().text = $"Move inside";
                moveInsideButton.GetComponent<Button>().onClick.AddListener(() => {
                    foreach(var f in item.floors)
                    {
                        if (f.number == 0)
                        {
                            foreach(var r in f.rooms)
                            {
                                if (r.type_id == 3)
                                {
                                    NetworkManager.Instance.Room(r.id);
                                }
                            }
                        }
                    }
                });
                buttons.Add(moveInsideButton);
            }
        }
    }
}
