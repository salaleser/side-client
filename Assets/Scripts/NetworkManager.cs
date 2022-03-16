using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    private const string Host = "65.21.107.192";
    private const string Port = "65278";

	private const int Width = 8;
	private const int Height = 8;

	public Text title;
	public Text text;
	public GameManager gameManager;
	public GameObject cellPrefab;
	public GameObject cityPrefab;
	public GameObject blockPrefab;
	public GameObject landLotPrefab;
	public GameObject defaultFacilityPrefab;
	public GameObject coalMineFacilityPrefab;
	public GameObject copperMineFacilityPrefab;
	public GameObject furnitureFactoryFacilityPrefab;

	private readonly List<GameObject> _buttonsList = new();

	private void DestroyAll()
	{
        text.text = "—";
		var objects = GameObject.FindGameObjectsWithTag("Side");
		foreach(var o in objects)
		{
			foreach(var bo in o.GetComponentsInChildren<City>())
			{
				foreach (var b in bo.buttons)
				{
					Destroy(b);
				}
			}
			foreach(var bo in o.GetComponentsInChildren<Block>())
			{
				foreach (var b in bo.buttons)
				{
					Destroy(b);
				}
			}
			foreach(var bo in o.GetComponentsInChildren<LandLot>())
			{
				foreach (var b in bo.buttons)
				{
					Destroy(b);
				}
			}
			foreach(var bo in o.GetComponentsInChildren<Facility>())
			{
				foreach (var b in bo.buttons)
				{
					Destroy(b);
				}
			}
			Destroy(o);
		}
	}

	public IEnumerator Location(int locationId)
    {
		DestroyAll();

		var query = $"location_id={locationId}";
		var uri = $"http://{Host}:{Port}/location?{query}";
		
		using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
			yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
					Process(webRequest.downloadHandler.text);
                    break;
            }
        }
	}

	private void Process(string json)
	{
		var response = JsonUtility.FromJson<LocationResponse>(json);

		var cl = response.current_location;
		gameManager.currentLocation = cl;
		title.text = $"{cl.type_id} — {cl.address.title}";

		for (var i = 0; i < Width; i++)
		{
			for (var j = 0; j < Height; j++)
			{
				var cellPrefabInstance = Instantiate(cellPrefab, new Vector3(i, 0, j), Quaternion.identity);
				var cell = cellPrefabInstance.GetComponent<Cell>();
				cell.x = i;
				cell.y = j;
				foreach(var l in response.locations)
				{
					if (i == l.x && j == l.y)
					{
						GameObject locationPrefab = null;

						if (cl.address.type_id == 0)
						{
							locationPrefab = cityPrefab;
						}
						else if (cl.address.type_id == 1)
						{
							locationPrefab = blockPrefab;
						}
						else if (cl.address.type_id == 2)
						{
							locationPrefab = landLotPrefab;
						}
						else if (cl.address.type_id == 3)
						{
							switch (l.type_id)
							{
								case 1:
									locationPrefab = coalMineFacilityPrefab;
									break;
								case 2:
									locationPrefab = copperMineFacilityPrefab;
									break;
								case 3:
									locationPrefab = furnitureFactoryFacilityPrefab;
									break;
								default:
									locationPrefab = defaultFacilityPrefab;
									break;
							}
						}

						var locationPrefabInstance = Instantiate(locationPrefab, cellPrefabInstance.transform);
						switch (cl.address.type_id)
						{
							case 0:
								var city = locationPrefabInstance.GetComponent<City>();
								if (city != null)
								{
									city.location = l;
								}
								break;
							case 1:
								var block = locationPrefabInstance.GetComponent<Block>();
								if (block != null)
								{
									block.location = l;
								}
								break;
							case 2:
								var landLot = locationPrefabInstance.GetComponent<LandLot>();
								if (landLot != null)
								{
									landLot.location = l;
								}
								break;
							case 3:
								var facility = locationPrefabInstance.GetComponent<Facility>();
								if (facility != null)
								{
									facility.location = l;
								}
								break;
						}
					}
				}
			}
		}
	}
}
