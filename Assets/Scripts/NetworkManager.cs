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

	public GameObject mainButtonsPanel;
    public List<GameObject> buttons = new();
	public GameObject marketCanvas;
	public GameObject inventoryCanvas;
	public GameObject tasksCanvas;
	public GameObject entitiesCanvas;
	public GameObject shading;

	public Text title;
	public Text text;
	public side.ChatController chatController;
	public GameObject mainButtonPrefab;
	public GameObject itemPrefab;
	public GameObject taskPrefab;
	public GameObject lotPrefab;
	public GameObject addressPrefab;
	public GameObject hrDepartmentRoomPrefab;
	public GameObject workshopRoomPrefab;
	public GameObject lobbyRoomPrefab;
	public GameObject storageRoomPrefab;
	public GameObject citizenPrefab;
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
		Citizen(263);

		var marketButton = Instantiate(mainButtonPrefab, new Vector3(600, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        marketButton.GetComponentInChildren<Text>().text = "Mrkt";
        marketButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.Market();
        });
        buttons.Add(marketButton);

		var closeButton = Instantiate(mainButtonPrefab, new Vector3(660, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        closeButton.GetComponentInChildren<Text>().text = "Close";
        closeButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.CloseButton();
        });
        buttons.Add(closeButton);

		var mapButton = Instantiate(mainButtonPrefab, new Vector3(720, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        mapButton.GetComponentInChildren<Text>().text = "Map";
        mapButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.MapButton();
        });
        buttons.Add(mapButton);

		var citizenButton = Instantiate(mainButtonPrefab, new Vector3(780, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        citizenButton.GetComponentInChildren<Text>().text = "Ctzn";
        citizenButton.GetComponent<Button>().onClick.AddListener(() => {
            NetworkManager.Instance.CitizenButton();
        });
        buttons.Add(citizenButton);
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

	public void MapButton()
	{
		HideAllButtons();
		shading.SetActive(false);
		Address(1);
	}

	public void CitizenButton()
	{
		HideAllButtons();
		shading.SetActive(false);
		Citizen(GameManager.Instance.currentCitizen.id);
	}

	public void CloseButton()
	{
		HideAllButtons();
		shading.SetActive(false);
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
	}

	public void ShowButtons(Transform transform)
	{
		HideAllButtons();
		text.text = "";
		foreach(var address in transform.GetComponentsInChildren<Address>())
		{
			text.text += $"{address.item}";
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
		foreach(var room in transform.GetComponentsInChildren<Room>())
		{
			text.text += $"\n\n{room.item}";
			room.ShowButtons();
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

	private void DestroyAll<T>() where T : Entity
	{
        text.text = "—";
		var objects = GameObject.FindGameObjectsWithTag("Side");
		foreach(var o in objects)
		{
			foreach(var bo in o.GetComponentsInChildren<T>())
			{
				foreach (var b in bo.buttons)
				{
					Destroy(b);
				}
				Destroy(o);
			}
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
		shading.SetActive(true);

		var query = $"";
		var uri = $"http://{Host}:{Port}/market?{query}";
		StartCoroutine(Request(uri, (result) =>
		{
			ProcessMarket(result);
		}));
	}

	public void Citizen(int citizenId)
    {
		var query = $"citizen_id={citizenId}";
		var uri = $"http://{Host}:{Port}/citizen?{query}";
		
		StartCoroutine(Request(uri, (result) =>
		{
			ProcessCitizen(result);
		}));
	}

	public void Address(int addressId)
    {
		var query = $"address_id={addressId}";
		var uri = $"http://{Host}:{Port}/address?{query}";
		
		StartCoroutine(Request(uri, (result) =>
		{
			ProcessAddress(result);
		}));
	}

	public void Floor(int locationId, int floorNumber)
    {
		var query = $"location_id={locationId}&floor_number={floorNumber}";
		var uri = $"http://{Host}:{Port}/floor?{query}";
		
		StartCoroutine(Request(uri, (result) =>
		{
			ProcessFloor(result);
		}));
	}

	public void Room(int roomId)
    {
		var query = $"citizen_id={GameManager.Instance.currentCitizen.id}&room_id={roomId}";
		var uri = $"http://{Host}:{Port}/room?{query}";
		
		StartCoroutine(Request(uri, (result) =>
		{
			Floor(GameManager.Instance.currentCitizen.location_id, GameManager.Instance.currentCitizen.floor_number);
		}));
	}

	public void Inventory(int rootItemId)
    {
		shading.SetActive(true);

		var query = $"root_item_id={rootItemId}";
		var uri = $"http://{Host}:{Port}/inventory?{query}";
		
		StartCoroutine(Request(uri, (result) =>
		{
			ProcessInventory(result);
		}));
	}

	public void Chat(int citizenId, int roomId, string text)
    {
		text = UnityWebRequest.EscapeURL(text);

		var query = $"citizen_id={citizenId}&room_id={roomId}&text={text}";
		var uri = $"http://{Host}:{Port}/chat?{query}";
		
		StartCoroutine(Request(uri, (result) =>
		{
			var c = JsonUtility.FromJson<ChatResponse>(result);
			chatController.ReplaceChat(c.messages);
		}));
	}

	public void Tasks(int locationId)
	{
		shading.SetActive(true);

		var query = $"location_id={locationId}";
		var uri = $"http://{Host}:{Port}/tasks?{query}";
		
		StartCoroutine(Request(uri, (result) =>
		{
			ProcessTasks(result);
		}));
	}

	public void TaskAccept(int citizenId, int taskId)
	{
		var query = $"citizen_id={citizenId}&task_id={taskId}";
		var uri = $"http://{Host}:{Port}/task-accept?{query}";
		
		StartCoroutine(Request(uri, (result) =>
		{
			ProcessTasks(result);
		}));
	}

	private void ProcessAddress(string json)
	{
		var response = JsonUtility.FromJson<AddressResponse>(json);

		DestroyAll<Room>();
		DestroyAll<Address>();

		var ca = response.current_address;
		title.text = $"{ca.type_id} — {ca.title}";

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
					}
				}
			}
		}
	}

	private void ProcessFloor(string json)
	{
		var response = JsonUtility.FromJson<FloorResponse>(json);

		DestroyAll<Room>();
		DestroyAll<Address>();

		var floor = response.floor;
		title.text = $"{floor.number} — {floor.rooms.Count}";

		foreach(var r in floor.rooms)
		{
			var isCitizensInstantiated = false;
			for (var i = 1; i <= Width; i++)
			{
				for (var j = 1; j <= Height; j++)
				{
					if (i >= r.x && i < r.x + r.w && j <= r.y && j > r.y - r.h)
					{
						isCitizensInstantiated = InstantiateRoom(r, i, j, isCitizensInstantiated);
					}
				}
			}
		}
	}

	private bool InstantiateRoom(RoomItem r, int x, int y, bool isCitizensInstantiated)
	{
		GameObject roomPrefab = null;
		if (r.type_id == 1)
		{
			roomPrefab = hrDepartmentRoomPrefab;
		}
		else if (r.type_id == 2)
		{
			roomPrefab = workshopRoomPrefab;
		}
		else if (r.type_id == 3)
		{
			roomPrefab = lobbyRoomPrefab;
		}
		else if (r.type_id == 4)
		{
			roomPrefab = storageRoomPrefab;
		}

		var roomPrefabInstance = Instantiate(roomPrefab, new Vector3(x, 0, y), Quaternion.identity, entitiesCanvas.transform);
		var room = roomPrefabInstance.GetComponent<Room>();

		room.item = r;

		if (!isCitizensInstantiated)
		{
			// TODO: расположить по клеткам
			foreach(var c in r.citizens)
			{
				var citizenPrefabInstance = Instantiate(citizenPrefab, roomPrefabInstance.transform);
				var citizen = citizenPrefabInstance.GetComponent<Citizen>();
				citizen.item = c;
				
				if (c.id == GameManager.Instance.currentCitizen.id)
				{
					GameManager.Instance.currentCitizen = c;
					chatController.ReplaceChat(r.messages);
				}
			}
			isCitizensInstantiated = true;
		}

		return isCitizensInstantiated;
	}

    private void ProcessMarket(string json)
	{
		var response = JsonUtility.FromJson<MarketResponse>(json);
        if (response == null)
        {
            return;
        }

		DestroyAll<Lot>();

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

	private void ProcessCitizen(string json)
	{
		var response = JsonUtility.FromJson<CitizenResponse>(json);
        if (response == null)
        {
            return;
        }

		GameManager.Instance.currentCitizen = response.citizen;
		Floor(GameManager.Instance.currentCitizen.location_id, GameManager.Instance.currentCitizen.floor_number);
	}

    private void ProcessInventory(string json)
	{
		var response = JsonUtility.FromJson<InventoryResponse>(json);
        if (response == null)
        {
            return;
        }

		DestroyAll<Item>();

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

	private void ProcessTasks(string json)
	{
		var response = JsonUtility.FromJson<TaskResponse>(json);
        if (response == null)
        {
            return;
        }

		DestroyAll<Task>();

        title.text = $"Tasks (Location ID {GameManager.Instance.currentCitizen.location_id})";

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.tasks.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

            var taskInstance = Instantiate(taskPrefab, new Vector3(100 + 120 * col, 700 - 60 * row, 0), Quaternion.identity, tasksCanvas.transform);
            var task = taskInstance.GetComponent<Task>();
            task.item = response.tasks[i];
            var button = taskInstance.GetComponent<Button>();
            button.GetComponentInChildren<TMP_Text>().text = $"{task.item.title} ({(task.item.is_free ? "free" : "busy")})";
            button.onClick.AddListener(task.Handler);

            row++;
        }
	}
}
