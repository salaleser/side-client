using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NetworkManager : Manager
{
	private const int Width = 8;
	private const int Height = 8;

	private string Host;
	private string Port;

	public GameObject mainButtonsPanel;
	public GameObject mainButtonPrefab;
	private GameObject marketButton;
	private GameObject inventoryButton;
	private GameObject mapButton;
	private GameObject citizenButton;
	private GameObject zoomOutButton;

	public GameObject marketCanvas;
	public GameObject inventoryCanvas;
	public GameObject buildCanvas;
	public GameObject tasksCanvas;
	public GameObject entitiesCanvas;
	public GameObject shading;
	public GameObject cursor;
	private GameObject _cursor;

	public TMP_Text title;
	public Text text;
	public side.ChatController chatController;
	public GameObject itemPrefab;
	public GameObject taskPrefab;
	public GameObject lotPrefab;
	public GameObject locationTypePrefab;
	
	public GameObject emptyAddressPrefab;
	public GameObject defaultAddressPrefab;
	public GameObject regionAddressPrefab;
	public GameObject cityAddressPrefab;
	public GameObject blockAddressPrefab;
	public GameObject landLotAddressPrefab;

	public GameObject defaultLocationPrefab;
	public GameObject coalMineLocationPrefab;
	public GameObject copperMineLocationPrefab;
	public GameObject furnitureFactoryLocationPrefab;

	public GameObject openRoomPrefab;
	public GameObject closedRoomPrefab;

	public GameObject citizenPrefab;

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

	private void LoadConfig()
	{
		var path = Application.dataPath + "/StreamingAssets/config.txt";
		using (StreamReader reader = new StreamReader(path))
		{
			try
			{
				var config = reader.ReadToEnd();
				foreach (var line in config.Split("\n"))
				{
					var pair = line.Split("=");
					var key = pair[0];
					var value = pair[1];
					if (key == "Host")
					{
						Host = value;
					}
					else if (key == "Port")
					{
						Port = value;
					}
				}
			}
			catch (System.Exception e)
			{
				Debug.Log(e);
			}
		}
	}

	private void Start()
	{
		LoadConfig();

		Citizen(263);
		Address(13);

		marketButton = Instantiate(mainButtonPrefab, new Vector3(600, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        marketButton.GetComponentInChildren<Text>().text = "Market";
        marketButton.GetComponent<Button>().onClick.AddListener(() => {
			HideAllButtons();
			Market();
        });

		// closeButton = Instantiate(mainButtonPrefab, new Vector3(660, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        // closeButton.GetComponentInChildren<Text>().text = "-";
        // closeButton.GetComponent<Button>().onClick.AddListener(() => {

        // });

		mapButton = Instantiate(mainButtonPrefab, new Vector3(720, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        mapButton.GetComponentInChildren<Text>().text = "Show Map";
        mapButton.GetComponent<Button>().onClick.AddListener(() => {
            HideAllButtons();
			Address(1);
        });

		citizenButton = Instantiate(mainButtonPrefab, new Vector3(780, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        citizenButton.GetComponentInChildren<Text>().text = "Show Floor";
        citizenButton.GetComponent<Button>().onClick.AddListener(() => {
            HideAllButtons();
			Floor(GameManager.Instance.citizen.floor_id);
        });

		zoomOutButton = Instantiate(mainButtonPrefab, new Vector3(840, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        zoomOutButton.GetComponentInChildren<Text>().text = "Zoom Out";
        zoomOutButton.GetComponent<Button>().onClick.AddListener(() => {
            HideAllButtons();
			if (GameManager.Instance.state == GameManager.Floor)
			{
				Address(GameManager.Instance.citizen.address_parent_id);
			}
			else if (GameManager.Instance.state == GameManager.Map)
			{
				Address(GameManager.Instance.address.parent_id);
			}
        });

		inventoryButton = Instantiate(mainButtonPrefab, new Vector3(900, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        inventoryButton.GetComponentInChildren<Text>().text = "Inventory";
        inventoryButton.GetComponent<Button>().onClick.AddListener(() => {
            HideAllButtons();
			Inventory(GameManager.Instance.citizen.root_item_id);
        });
	}

	void Update() {  
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                ShowButtons(hit.transform);
				Destroy(_cursor);
				_cursor = Instantiate(cursor, hit.transform);
            } else {
				Destroy(_cursor);
				text.text = "—";
			}
        }
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

	IEnumerator Request(string endpoint, string query, Action<string> result)
	{
		var url = $"http://{Host}:{Port}/{endpoint}?{query}";

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

		StartCoroutine(Request("market", "", (result) =>
		{
			ProcessMarket(result);
		}));
	}

	public void Citizen(int citizenId)
    {
		var query = $"citizen_id={citizenId}";
		
		StartCoroutine(Request("citizen", query, (result) =>
		{
			ProcessCitizen(result);
		}));
	}

	public void Address(int addressId)
    {
		var query = $"address_id={addressId}";
		
		StartCoroutine(Request("address", query, (result) =>
		{
			ProcessAddress(result);
		}));
		
		GameManager.Instance.state = GameManager.Map;
	}

	public void RegisterAddress(int parentId, int x, int y, string title)
    {
		var query = $"parent_id={parentId}&x={x}&y={y}&title={title}";
		
		StartCoroutine(Request("address-register", query, (result) =>
		{
			ProcessAddress(result);
		}));
	}

	public void RegisterLocation(int addressId, int locationTypeId)
    {
		var query = $"address_id={addressId}&location_type_id={locationTypeId}&citizen_id={GameManager.Instance.citizen.id}";
		
		StartCoroutine(Request("location-register", query, (result) =>
		{
			ProcessAddress(result);
		}));
	}

	public void Location(int locationId)
    {
		var query = $"location_id={locationId}";
		
		StartCoroutine(Request("location", query, (result) =>
		{
			ProcessLocation(result);
		}));
	
		GameManager.Instance.state = GameManager.Floor;
	}

	public void Floor(int floorId)
    {
		var query = $"floor_id={floorId}";
		
		StartCoroutine(Request("floor", query, (result) =>
		{
			ProcessFloor(result);
		}));
	
		GameManager.Instance.state = GameManager.Floor;
	}

	public void Room(int roomId)
    {
		var query = $"citizen_id={GameManager.Instance.citizen.id}&room_id={roomId}";
		
		StartCoroutine(Request("room", query, (result) =>
		{
			ProcessRoom(result);
		}));
	}

	public void Inventory(int rootItemId)
    {
		shading.SetActive(true);

		var query = $"root_item_id={rootItemId}";
		
		StartCoroutine(Request("inventory", query, (result) =>
		{
			ProcessInventory(result);
		}));
	}

	public void Build(int addressId)
	{
		shading.SetActive(true);

		var query = $"address_id={addressId}";
		
		StartCoroutine(Request("build", query, (result) =>
		{
			ProcessBuild(result);
		}));
	}

	public void Chat(int citizenId, int roomId, string text)
    {
		text = UnityWebRequest.EscapeURL(text);

		var query = $"citizen_id={citizenId}&room_id={roomId}&text={text}";
		
		StartCoroutine(Request("chat", query, (result) =>
		{
			var c = JsonUtility.FromJson<ChatResponse>(result);
			chatController.ReplaceChat(c.messages);
		}));
	}

	public void Tasks(int locationId)
	{
		shading.SetActive(true);

		var query = $"location_id={locationId}";
		
		StartCoroutine(Request("tasks", query, (result) =>
		{
			ProcessTasks(result);
		}));
	}

	public void TaskAccept(int citizenId, int taskId)
	{
		var query = $"citizen_id={citizenId}&task_id={taskId}";
		
		StartCoroutine(Request("task-accept", query, (result) =>
		{
			ProcessTasks(result);
		}));
	}

	private void ProcessRoom(string json)
	{
		var response = JsonUtility.FromJson<RoomResponse>(json);
		if (response == null)
		{
			return;
		}

		GameManager.Instance.citizen = response.citizen;
		ProcessLocation(json);
	}

	private void ProcessAddress(string json)
	{
		var response = JsonUtility.FromJson<AddressResponse>(json);
		if (response == null)
		{
			return;
		}

		var address = response.address;
		GameManager.Instance.address = address;
		title.text = $"{response.address.type_title} {response.address.title}";

		// FIXME: исключение для региона Side
		zoomOutButton.SetActive(GameManager.Instance.address.id != 1);

		DestroyAll<Room>();
		DestroyAll<Address>();
		DestroyAll<Citizen>();
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<LocationType>();
		shading.SetActive(false);

		var addressMap = new AddressItem[Width, Height];
		foreach(var a in response.addresses)
		{
			if (a.location.id == 0)
			{
				a.location = null;
			}
			for (var i = 0; i < Width; i++)
			{
				for (var j = 0; j < Height; j++)
				{
					if (i+1 == a.x && j+1 == a.y)
					{
						addressMap[i, j] = a;
					}
				}
			}
		}

		for (var i = 0; i < Width; i++)
		{
			for (var j = 0; j < Height; j++)
			{
				if (addressMap[i, j] == null)
				{
					var emptyAddressPrefabInstance = Instantiate(emptyAddressPrefab,
						new Vector3(i+1, 0, j+1), Quaternion.identity, entitiesCanvas.transform);
					var a = emptyAddressPrefabInstance.GetComponent<Address>();
					var item = new AddressItem();
					item.x = i+1;
					item.y = j+1;
					a.item = item;
				}
				else
				{
					InstantiateAddress(addressMap[i, j], i+1, j+1);
				}
			}
		}
	}

	private void InstantiateAddress(AddressItem a, int x, int y)
	{
		GameObject addressPrefab = null;
		switch (a.type_id)
		{
			case AddressTypes.Region:
				addressPrefab = regionAddressPrefab;
				break;
			case AddressTypes.City:
				addressPrefab = cityAddressPrefab;
				break;
			case AddressTypes.Block:
				addressPrefab = blockAddressPrefab;
				break;
			case AddressTypes.LandLot:
				addressPrefab = landLotAddressPrefab;
				break;
			default:
				addressPrefab = defaultAddressPrefab;
				break;
		}
		
		var addressPrefabInstance = Instantiate(addressPrefab, new Vector3(x, 0, y), Quaternion.identity, entitiesCanvas.transform);
		var address = addressPrefabInstance.GetComponent<Address>();

		address.item = a;

		if (a.location == null)
		{
			return;
		}

		GameObject locationPrefab = null;
		switch (a.location.type_id)
		{
			case 1:
				locationPrefab = coalMineLocationPrefab;
				break;
			case 2:
				locationPrefab = copperMineLocationPrefab;
				break;
			case 3:
				locationPrefab = furnitureFactoryLocationPrefab;
				break;
			default:
				locationPrefab = defaultLocationPrefab;
				break;
		}

		var locationPrefabInstance = Instantiate(locationPrefab, addressPrefabInstance.transform);
		var location = locationPrefabInstance.GetComponent<Location>();

		location.item = a.location;
	}

	private void ProcessLocation(string json)
	{
		var response = JsonUtility.FromJson<LocationResponse>(json);
		if (response == null)
		{
			return;
		}

		var location = response.location;
		GameManager.Instance.location = location;

		var floorResponse = new FloorResponse();
		foreach(var f in location.floors)
		{
			if (f.number == 0)
			{
				floorResponse.floor = f;
				break;
			}
		}

		ProcessFloor(JsonUtility.ToJson(floorResponse));
	}

	private void ProcessFloor(string json)
	{
		var response = JsonUtility.FromJson<FloorResponse>(json);
		if (response == null)
		{
			return;
		}

		DestroyAll<Room>();
		DestroyAll<Address>();
		DestroyAll<Citizen>();
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<LocationType>();
		shading.SetActive(false);

		var floor = response.floor;
		GameManager.Instance.floor = floor;
		title.text = $"{GameManager.Instance.location.type_title}, {floor.number} этаж";
		
		var floorMap = new RoomItem[Width, Height];
		foreach(var r in floor.rooms)
		{
			for (var i = 0; i < Width; i++)
			{
				for (var j = 0; j < Height; j++)
				{
					if (i+1 >= r.x && i+1 < r.x + r.w && j+1 <= r.y && j+1 > r.y - r.h)
					{
						floorMap[i, j] = r;
					}
				}
			}
		}

		for (var i = 0; i < Width; i++)
		{
			for (var j = 0; j < Height; j++)
			{
				if (floorMap[i, j] == null)
				{
					Instantiate(openRoomPrefab, new Vector3(i+1, 0, j+1), Quaternion.identity, entitiesCanvas.transform);
				}
				else
				{
					InstantiateRoom(floorMap[i, j], i+1, j+1);
				}
			}
		}

		foreach(var r in floor.rooms)
		{
			if (r.id != GameManager.Instance.citizen.room_id)
			{
				continue;
			}

			chatController.ReplaceChat(r.messages);

			for (var i = 0; i < r.citizens.Count; i++)
			{
				var citizenPrefabInstance = Instantiate(citizenPrefab, new Vector3(i+3, 0, 11), Quaternion.identity, entitiesCanvas.transform);
				var citizen = citizenPrefabInstance.GetComponent<Citizen>();

				if (r.citizens[i].id == GameManager.Instance.citizen.id)
				{
					citizen.GetComponentInChildren<Renderer>().material.color = Color.green;
				}

				citizen.item = r.citizens[i];
			}
		}
	}

	private void InstantiateRoom(RoomItem r, int x, int y)
	{
		var roomPrefabInstance = Instantiate(r.id == GameManager.Instance.citizen.room_id ? openRoomPrefab : closedRoomPrefab,
			new Vector3(x, 0, y), Quaternion.identity, entitiesCanvas.transform);
		var room = roomPrefabInstance.GetComponent<Room>();

		Color color = Color.white;
		if (r.type_id == RoomTypes.HrDepartment)
		{
			color = Color.blue;
		}
		else if (r.type_id == RoomTypes.Workshop)
		{
			color = Color.red;
		}
		else if (r.type_id == RoomTypes.Lobby)
		{
			color = Color.yellow;
		}
		else if (r.type_id == RoomTypes.Storage)
		{
			color = Color.gray;
		}
		else if (r.type_id == RoomTypes.Bedroom)
		{
			color = Color.cyan;
		}
		else if (r.type_id == RoomTypes.Reception)
		{
			color = Color.magenta;
		}

		room.GetComponentInChildren<Renderer>().material.color = color;

		room.item = r;
	}

    private void ProcessMarket(string json)
	{
		var response = JsonUtility.FromJson<MarketResponse>(json);
        if (response == null)
        {
            return;
        }

		title.text = "Market";
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<LocationType>();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.lots.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var lot = InstantiateObject<Lot>(lotPrefab, marketCanvas,
				$"{response.lots[i].item_type_title}: {response.lots[i].quantity}x{response.lots[i].price}₷ ({response.lots[i].owner_id})", 100, 700, col, row);
			lot.item = response.lots[i];

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

		GameManager.Instance.citizen = response.citizen;
	}

    private void ProcessInventory(string json)
	{
		var response = JsonUtility.FromJson<InventoryResponse>(json);
        if (response == null)
        {
            return;
        }

		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<LocationType>();

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

            var item = InstantiateObject<Item>(itemPrefab, inventoryCanvas,
				response.items[i].type_title, 100, 700, col, row);
			item.item = response.items[i];

            row++;
        }
	}

	private void ProcessBuild(string json)
	{
		var response = JsonUtility.FromJson<BuildResponse>(json);
        if (response == null)
        {
            return;
        }

		DestroyAll<LocationType>();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.location_types.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

            var locationType = InstantiateObject<LocationType>(locationTypePrefab, buildCanvas,
				response.location_types[i].title,
				100, 700, col, row);
			locationType.item = response.location_types[i];

            row++;
        }
	}

	private T InstantiateObject<T>(GameObject prefab, GameObject parent, string title, int x, int y, int col, int row) where T : Entity
	{
		var rect = prefab.transform.GetComponent<RectTransform>().rect;
		var instance = Instantiate(prefab, new Vector3(x + rect.width * col, y - rect.height * row, 0), Quaternion.identity, parent.transform);
		var component = instance.GetComponent<T>();
		var button = instance.GetComponent<Button>();
		button.GetComponentInChildren<TMP_Text>().text = title;
		button.onClick.AddListener(component.Handler);
		return component;
	}

	private void ProcessTasks(string json)
	{
		var response = JsonUtility.FromJson<TaskResponse>(json);
        if (response == null)
        {
            return;
        }

		DestroyAll<Task>();

        title.text = $"Tasks (Location ID {GameManager.Instance.citizen.location_id})";

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.tasks.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var task = InstantiateObject<Task>(taskPrefab, tasksCanvas,
				$"{response.tasks[i].title} ({(response.tasks[i].is_free ? "free" : "busy")})", 100, 700, col, row);
			task.item = response.tasks[i];

            row++;
        }
	}
}
