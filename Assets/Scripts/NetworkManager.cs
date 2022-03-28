using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NetworkManager : Manager
{
	private const int Width = 8;
	private const int Height = 8;

	public GameObject buttonsCanvas;
    public List<GameObject> buttons = new();
	public GameObject marketCanvas;
	public GameObject inventoryCanvas;
	public GameObject entitiesCanvas;

	public GameObject hud;
	public Text title;
	public Text text;
	public side.ChatController chatController;
	public GameObject mainButtonPrefab;
	public GameObject itemPrefab;
	public GameObject lotPrefab;
	public GameObject addressPrefab;
	public GameObject citizenLevel3Prefab;
	public GameObject citizenLevel4Prefab;
	public GameObject citizenLevel5Prefab;
	public GameObject cityPrefab;
	public GameObject blockPrefab;
	public GameObject landLotPrefab;
	public GameObject defaultFacilityPrefab;
	public GameObject coalMineFacilityPrefab;
	public GameObject copperMineFacilityPrefab;
	public GameObject furnitureFactoryFacilityPrefab;

	public static NetworkManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		Location(GameManager.Instance.currentAddress.id);

		var marketButton = Instantiate(mainButtonPrefab, new Vector3(600, 30, 0), Quaternion.identity, buttonsCanvas.transform);
        marketButton.GetComponentInChildren<Text>().text = "M: [A]";
        marketButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.Market();
        });
        buttons.Add(marketButton);

		var exitButton = Instantiate(mainButtonPrefab, new Vector3(660, 30, 0), Quaternion.identity, buttonsCanvas.transform);
        exitButton.GetComponentInChildren<Text>().text = "Exit";
        exitButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.ExitButton();
        });
        buttons.Add(exitButton);

		var locationUpButton = Instantiate(mainButtonPrefab, new Vector3(720, 30, 0), Quaternion.identity, buttonsCanvas.transform);
        locationUpButton.GetComponentInChildren<Text>().text = "Up";
        locationUpButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.LocationUpButton();
        });
        buttons.Add(locationUpButton);
	}

	void Update() {  
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                ShowButtons(hit.transform);
            } else {
				text.text = "—";
			}
        }
    }

	public void LocationUpButton()
	{
		HideAllButtons();
		if (GameManager.Instance.currentAddress.type_id > 0)
		{
			Location(GameManager.Instance.currentAddress.parent_id);
		}
	}

	public void ExitButton()
	{
		HideAllButtons();
		Location(GameManager.Instance.currentAddress.id);
	}

	public void ShowButtons(Transform transform)
	{
		HideAllButtons();
		text.text = "";
		foreach(var address in transform.GetComponentsInChildren<Address>())
		{
			text.text += $"\n\n{address.item}";
			address.ShowButtons();
		}
		foreach(var location in transform.GetComponentsInChildren<Location>())
		{
			text.text += $"\n\n{location.item}";
			location.ShowButtons();
		}
		foreach(var citizen in transform.GetComponentsInChildren<Citizen>())
		{
			text.text += $"\n\n{citizen.item}";
			citizen.ShowButtons();
		}
	}

	public void HideAllButtons()
	{
		var objects = GameObject.FindGameObjectsWithTag("EntityButton");
		foreach(var o in objects)
		{
			o.SetActive(false);
		}
	}

	private void DestroyAll()
	{
		HideAllButtons();
        text.text = "—";
		var objects = GameObject.FindGameObjectsWithTag("Side");
		foreach(var o in objects)
		{
			foreach(var bo in o.GetComponentsInChildren<Location>())
			{
				foreach (var b in bo.buttons)
				{
					Destroy(b);
				}
			}
			foreach(var bo in o.GetComponentsInChildren<Item>())
			{
				foreach (var b in bo.buttons)
				{
					Destroy(b);
				}
			}
			foreach(var bo in o.GetComponentsInChildren<Lot>())
			{
				foreach (var b in bo.buttons)
				{
					Destroy(b);
				}
			}
			Destroy(o);
		}
	}

	IEnumerator Request(string url, Action<string> result)
	{
		string[] pages = url.Split('/');
		int page = pages.Length - 1;

		UnityWebRequest request = UnityWebRequest.Get(url);
		UnityWebRequestAsyncOperation async = request.SendWebRequest();
		while (!async.isDone)
		{
			yield return null;
		}

		switch (request.result)
		{
			case UnityWebRequest.Result.ConnectionError:
			case UnityWebRequest.Result.DataProcessingError:
				Debug.LogError(pages[page] + ": Error: " + request.error);
				result(request.error);
				break;
			case UnityWebRequest.Result.ProtocolError:
				Debug.LogError(pages[page] + ": HTTP Error: " + request.error);
				result(request.error);
				break;
			case UnityWebRequest.Result.Success:
				Debug.Log(pages[page] + ":\nReceived: " + request.downloadHandler.text);
				result(request.downloadHandler.text);
				break;
		}
	}

	public void Market()
	{
		var query = $"";
		var uri = $"http://{Host}:{Port}/market?{query}";
		StartCoroutine(Request(uri, (result) =>
		{
			DestroyAll();
			ProcessMarket(result);
		}));
	}

	public void Location(int locationId)
    {
		var query = $"location_id={locationId}";
		var uri = $"http://{Host}:{Port}/location?{query}";
		
		StartCoroutine(Request(uri, (result) =>
		{
			DestroyAll();
			ProcessLocation(result);
		}));
	}

	public void Inventory(int rootItemId)
    {
		var query = $"root_item_id={rootItemId}";
		var uri = $"http://{Host}:{Port}/inventory?{query}";
		
		StartCoroutine(Request(uri, (result) =>
		{
			DestroyAll();
			ProcessInventory(result);
		}));
	}

	public void Chat(int citizenId, int locationId, string text)
    {
		var query = $"citizen_id={citizenId}&location_id={locationId}";
		var uri = $"http://{Host}:{Port}/chat?{query}";
		
		StartCoroutine(Request(uri, (result) =>
		{
		}));
	}

	private void ProcessLocation(string json)
	{
		var response = JsonUtility.FromJson<LocationResponse>(json);

		GameManager.Instance.currentAddress = response.current_address;
		var ca = GameManager.Instance.currentAddress;
		title.text = $"{ca.type_id} — {ca.title}";

		chatController.ReplaceChat(response.chat);

		for (var i = 1; i <= Width; i++)
		{
			for (var j = 1; j <= Height; j++)
			{
				foreach(var a in response.addresses)
				{
					if (i != a.x || j != a.y)
					{
						continue;
					}

					var addressPrefabInstance = Instantiate(addressPrefab, new Vector3(i, 0, j), Quaternion.identity, entitiesCanvas.transform);
					var address = addressPrefabInstance.GetComponent<Address>();

					address.item = a;

					foreach(var l in ca.locations)
					{
						if (a.id != l.address_id)
						{
							continue;
						}

						GameObject locationPrefab = null;

						if (a.type_id == 1)
						{
							locationPrefab = cityPrefab;
						}
						else if (a.type_id == 2)
						{
							locationPrefab = blockPrefab;
						}
						else if (a.type_id == 3)
						{
							locationPrefab = landLotPrefab;
						}
						else if (a.type_id == 4)
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

						var locationPrefabInstance = Instantiate(locationPrefab, addressPrefabInstance.transform);
						var location = locationPrefabInstance.GetComponent<Location>();
				
						location.item = l;

						foreach(var c in l.citizens)
						{
							if (i != c.x && j != c.y)
							{
								continue;
							}

							GameObject citizenPrefab = null;

							if (a.type_id == 2)
							{
								citizenPrefab = citizenLevel3Prefab;
							}
							else if (a.type_id == 3)
							{
								citizenPrefab = citizenLevel4Prefab;
							}
							else if (a.type_id == 4)
							{
								citizenPrefab = citizenLevel5Prefab;
							}
							else
							{
								// TODO: житель в странной локации, не должно его тут быть
								continue;
							}

							var citizenPrefabInstance = Instantiate(citizenPrefab, addressPrefabInstance.transform);
							var citizen = citizenPrefabInstance.GetComponent<Citizen>();
							citizen.item = c;
						}
					}
				}
			}
		}
	}

    private void ProcessMarket(string json)
	{
		var response = JsonUtility.FromJson<MarketResponse>(json);
        if (response == null)
        {
            return;
        }

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.lots.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

            var lotInstance = Instantiate(lotPrefab, new Vector3(100 + 240 * col, 700 - 60 * row, 0), Quaternion.identity, marketCanvas.transform);
            var lot = lotInstance.GetComponent<Lot>();
            lot.item = response.lots[i];
            var button = lotInstance.GetComponent<Button>();
            button.GetComponentInChildren<TMP_Text>().text = $"{response.lots[i].item_type_title}: {response.lots[i].quantity}x{response.lots[i].price}₷ ({response.lots[i].owner_id})";
            button.onClick.AddListener(lot.Handler);

            row++;
        }
	}

    private void ProcessInventory(string json)
	{
		var response = JsonUtility.FromJson<InventoryResponse>(json);
        if (response == null)
        {
            return;
        }

        if (response.title != null)
        {
            title.text = $"{response.title}";
        }

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.items.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

            var itemInstance = Instantiate(itemPrefab, new Vector3(100 + 120 * col, 700 - 120 * row, 0), Quaternion.identity, inventoryCanvas.transform);
            var item = itemInstance.GetComponent<Item>();
            item.item = response.items[i];
            var button = itemInstance.GetComponent<Button>();
            button.GetComponentInChildren<TMP_Text>().text = response.items[i].type_title;
            button.onClick.AddListener(item.Handler);

            row++;
        }
	}
}
