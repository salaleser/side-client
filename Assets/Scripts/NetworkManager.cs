﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Items;
using Models;

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
	private GameObject buildModeButton;
	private GameObject closeWindowButton;
	private GameObject organizationsButton;

	public GameObject marketCanvas;
	public GameObject inventoryCanvas;
	public GameObject buildCanvas;
	public GameObject tasksCanvas;
	public GameObject entitiesCanvas;
	public GameObject shading;

	public TMP_Text title;
	public Text text;
	public side.ChatController chatController;

	public GameObject itemPrefab;
	public GameObject taskPrefab;
	public GameObject positionPrefab;
	public GameObject lotPrefab;
	public GameObject organizationTypePrefab;
	public GameObject roomTypePrefab;
	public GameObject rentedRoomItemPrefab;
	
	public GameObject regionGroundPrefab;
	public GameObject cityGroundPrefab;
	public GameObject blockGroundPrefab;
	public GameObject parcelGroundPrefab;
	public GameObject floorGroundPrefab;

	public GameObject cityPrefab;
	public GameObject blockPrefab;
	public GameObject parcelPrefab;
	public GameObject floorPrefab;

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

		Citizen(GameManager.Instance.citizen.id);
		Parcel(GameManager.Instance.citizen.parcel_id);

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
			Region(1);
        });

		citizenButton = Instantiate(mainButtonPrefab, new Vector3(780, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        citizenButton.GetComponentInChildren<Text>().text = "Show Floor";
        citizenButton.GetComponent<Button>().onClick.AddListener(() => {
            HideAllButtons();
			Floor(GameManager.Instance.citizen.floor_id);
        });

		// zoomOutButton = Instantiate(mainButtonPrefab, new Vector3(840, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        // zoomOutButton.GetComponentInChildren<Text>().text = "Zoom Out";
        // zoomOutButton.GetComponent<Button>().onClick.AddListener(() => {
        //     HideAllButtons();
		// 	if (GameManager.Instance.state == GameManager.Floor)
		// 	{
		// 		Address(GameManager.Instance.citizen.address_parent_id);
		// 	}
		// 	else if (GameManager.Instance.state == GameManager.Map)
		// 	{
		// 		Address(GameManager.Instance.address.parent_id);
		// 	}
        // });

		inventoryButton = Instantiate(mainButtonPrefab, new Vector3(900, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        inventoryButton.GetComponentInChildren<Text>().text = "Inventory";
        inventoryButton.GetComponent<Button>().onClick.AddListener(() => {
            HideAllButtons();
			Inventory(GameManager.Instance.citizen.root_item_id);
        });

		// buildModeButton = Instantiate(mainButtonPrefab, new Vector3(960, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
		// buildModeButton.SetActive(false);
        // buildModeButton.GetComponentInChildren<Text>().text = "Build Mode";
        // buildModeButton.GetComponent<Button>().onClick.AddListener(() => {
        //     HideAllButtons();
		// 	RoomTypes(GameManager.Instance.floor.id);
        // });

		closeWindowButton = Instantiate(mainButtonPrefab, new Vector3(1020, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
		closeWindowButton.SetActive(false);
        closeWindowButton.GetComponentInChildren<Text>().text = "Close Window";
        closeWindowButton.GetComponent<Button>().onClick.AddListener(() => {
            CloseWindow();
        });

		organizationsButton = Instantiate(mainButtonPrefab, new Vector3(1080, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        organizationsButton.GetComponentInChildren<Text>().text = "Create Organization";
        organizationsButton.GetComponent<Button>().onClick.AddListener(() => {
			Organizations();
        });
	}

	private void CloseWindow()
	{
		HideAllButtons();
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<Position>();
		DestroyAll<RoomType>();
		DestroyAll<OrganizationType>();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);
	}

	public void ShowButtons(Transform transform)
	{
		HideAllButtons();
		text.text = "";
		foreach(var ground in transform.GetComponentsInChildren<Ground>())
		{
			text.text += $"{ground.groundItem}";
			ground.ShowButtons();
		}
		foreach(var region in transform.GetComponentsInChildren<Region>())
		{
			text.text += $"{region.regionItem}";
			region.ShowButtons();
		}
		foreach(var city in transform.GetComponentsInChildren<City>())
		{
			text.text += $"{city.cityItem}";
			city.ShowButtons();
		}
		foreach(var block in transform.GetComponentsInChildren<Block>())
		{
			text.text += $"{block.blockItem}";
			block.ShowButtons();
		}
		foreach(var parcel in transform.GetComponentsInChildren<Parcel>())
		{
			text.text += $"{parcel.parcelItem}";
			parcel.ShowButtons();
		}
		foreach(var floor in transform.GetComponentsInChildren<Floor>())
		{
			text.text += $"{floor.floorItem}";
			floor.ShowButtons();
		}
		foreach(var room in transform.GetComponentsInChildren<Room>())
		{
			text.text += $"{room.roomItem}";
			room.ShowButtons();
		}
		foreach(var citizen in transform.GetComponentsInChildren<Citizen>())
		{
			text.text += $"{citizen.citizenItem}";
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
		DestroyAll<Region>();
		DestroyAll<City>();
		DestroyAll<Block>();
		DestroyAll<Parcel>();
		DestroyAll<Floor>();
		DestroyAll<Ground>();
		DestroyAll<Room>();
		DestroyAll<Citizen>();
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<Position>();
		DestroyAll<RoomType>();
		DestroyAll<OrganizationType>();
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
		closeWindowButton.SetActive(shading.activeSelf);

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

	public void Region(int regionId)
    {
		var query = $"region_id={regionId}";
		
		StartCoroutine(Request("region", query, (result) =>
		{
			ProcessRegion(result);
		}));
		
		GameManager.Instance.state = GameManager.Map;
	}

	public void RegisterRegion(int continentId, int x, int y, string title)
    {
		var query = $"continent_id={continentId}&x={x}&y={y}&title={title}";
		
		StartCoroutine(Request("region-register", query, (result) =>
		{
			// ProcessRegion(result);
		}));
	}

	public void City(int cityId)
    {
		var query = $"city_id={cityId}";
		
		StartCoroutine(Request("city", query, (result) =>
		{
			ProcessCity(result);
		}));
		
		GameManager.Instance.state = GameManager.Map;
	}

	public void RegisterCity(int regionId, int x, int y, string title)
    {
		var query = $"region_id={regionId}&x={x}&y={y}&title={title}";
		
		StartCoroutine(Request("region-register", query, (result) =>
		{
			ProcessRegion(result);
		}));
	}

	public void Block(int blockId)
    {
		var query = $"block_id={blockId}";
		
		StartCoroutine(Request("block", query, (result) =>
		{
			ProcessBlock(result);
		}));
		
		GameManager.Instance.state = GameManager.Map;
	}

	public void RegisterBlock(int cityId, int x, int y, string title)
    {
		var query = $"city_id={cityId}&x={x}&y={y}&title={title}";
		
		StartCoroutine(Request("block-register", query, (result) =>
		{
			ProcessCity(result);
		}));
	}

	public void Parcel(int parcelId)
    {
		var query = $"parcel_id={parcelId}";
		
		StartCoroutine(Request("parcel", query, (result) =>
		{
			ProcessParcel(result);
		}));
		
		GameManager.Instance.state = GameManager.Map;
	}

	public void RegisterParcel(int blockId, int x, int y)
    {
		var query = $"block_id={blockId}&x={x}&y={y}";
		
		StartCoroutine(Request("parcel-register", query, (result) =>
		{
			ProcessBlock(result);
		}));
	}

	public void CreateRoom(int floorId, int roomTypeId, int x, int y, int w, int h)
    {
		var query = $"floor_id={floorId}&room_type_id={roomTypeId}&x={x}&y={y}&w={w}&h={h}";
		
		StartCoroutine(Request("create-room", query, (result) =>
		{
			ProcessFloor(result);
		}));
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

	public void MoveIntoRoom(int roomId)
    {
		var query = $"citizen_id={GameManager.Instance.citizen.id}&room_id={roomId}";
		
		StartCoroutine(Request("move-into-room", query, (result) =>
		{
			ProcessMoveIntoRoom(result);
		}));
	}

	public void Inventory(int rootItemId)
    {
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"root_item_id={rootItemId}";
		
		StartCoroutine(Request("inventory", query, (result) =>
		{
			ProcessInventory(result);
		}));
	}

	public void RoomTypes()
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		StartCoroutine(Request("room-types", "", (result) =>
		{
			ProcessRoomTypes(result);
		}));
	}

	public void Organizations(int ownerId)
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"owner_id={ownerId}";

		StartCoroutine(Request("organizations", query, (result) =>
		{
			ProcessOrganizations(result);
		}));
	}

	public void OrganizationTypes()
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		StartCoroutine(Request("organization-types", "", (result) =>
		{
			ProcessOrganizationTypes(result);
		}));
	}

	public void RentedRooms(int roomTypeId)
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"renter_id={GameManager.Instance.citizen.id}&room_type_id={roomTypeId}";

		StartCoroutine(Request("rented-rooms", query, (result) =>
		{
			ProcessRentedRooms(result);
		}));
	}

	public void CreateOrganization(int organizationTypeId)
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"organization_type_id={organizationTypeId}&owner_id={GameManager.Instance.citizen.id}";

		StartCoroutine(Request("create-organization", query, (result) =>
		{
			ProcessOrganization(result);
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

	public void Tasks(int organizationId)
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"organization_id={organizationId}";
		
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

	public void ManageOrganization(int organizationId)
	{
		var query = $"organization_id={organizationId}";
		
		StartCoroutine(Request("manage", query, (result) =>
		{
			ProcessManage(result);
		}));
	}

	public void PositionRequest(int citizenId, int positionId)
	{
		var query = $"citizen_id={citizenId}&position_id={positionId}";
		
		StartCoroutine(Request("position-request", query, (result) =>
		{
			ProcessTasks(result);
		}));
	}

	private void ProcessMoveIntoRoom(string json)
	{
		var response = JsonUtility.FromJson<MoveIntoRoomResponse>(json);
		if (response == null)
		{
			return;
		}

		GameManager.Instance.citizen = response.citizen;
		ProcessFloor(json);
	}

	private void ProcessManage(string json)
	{
		// TODO
	}

	private void InstantiateGround(GameObject prefab)
	{
		for (var i = 0; i < Width; i++)
		{
			for (var j = 0; j < Height; j++)
			{
				var instance = Instantiate(prefab, new Vector3(i+1, 0, j+1), Quaternion.identity, entitiesCanvas.transform);
				instance.name = $"Ground ({i+1}/{j+1})";
				var ground = instance.GetComponent<Ground>();
				ground.groundItem.x = i+1;
				ground.groundItem.y = j+1;
			}
		}
	}

	private void ProcessRegion(string json)
	{
		var response = JsonUtility.FromJson<RegionResponse>(json);
		if (response == null)
		{
			return;
		}

		var region = response.region;
		GameManager.Instance.region = region;
		title.text = $"Region \"{region.title}\"";

		DestroyAll();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		foreach(var city in region.cities)
		{
			var instance = Instantiate(cityPrefab, new Vector3(city.x, 0, city.y), Quaternion.identity, entitiesCanvas.transform);
			instance.name = $"City#{city.id} ({city.x}/{city.y})";
			instance.GetComponent<City>().cityItem = city;
		}

		InstantiateGround(cityGroundPrefab);
	}

	private void ProcessCity(string json)
	{
		var response = JsonUtility.FromJson<CityResponse>(json);
		if (response == null)
		{
			return;
		}

		var city = response.city;
		GameManager.Instance.city = city;
		title.text = $"City \"{city.title}\"";

		DestroyAll();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		foreach(var block in city.blocks)
		{
			var instance = Instantiate(blockPrefab, new Vector3(block.x, 0, block.y), Quaternion.identity, entitiesCanvas.transform);
			instance.name = $"Block#{block.id} ({block.x}/{block.y})";
			instance.GetComponent<Block>().blockItem = block;
		}

		InstantiateGround(blockGroundPrefab);
	}

	private void ProcessBlock(string json)
	{
		var response = JsonUtility.FromJson<BlockResponse>(json);
		if (response == null)
		{
			return;
		}

		var block = response.block;
		GameManager.Instance.block = block;
		title.text = $"Block \"{block.title}\"";

		DestroyAll();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		foreach(var parcel in block.parcels)
		{
			var instance = Instantiate(parcelPrefab, new Vector3(parcel.x, 0, parcel.y), Quaternion.identity, entitiesCanvas.transform);
			instance.name = $"Parcel#{parcel.id} ({parcel.x}/{parcel.y})";
			instance.GetComponent<Parcel>().parcelItem = parcel;
		}

		InstantiateGround(parcelGroundPrefab);
	}

	private void ProcessParcel(string json)
	{
		var response = JsonUtility.FromJson<ParcelResponse>(json);
		if (response == null)
		{
			return;
		}

		var parcel = response.parcel;
		GameManager.Instance.parcel = parcel;
		title.text = $"Parcel \"{parcel.title}\"";

		DestroyAll();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		var parcelMap = new FloorItem[Width, Height];
		foreach(var floor in parcel.floors)
		{
			for (var i = 0; i < Width; i++)
			{
				for (var j = 0; j < Height; j++)
				{
					if (i+1 >= floor.x
						&& i+1 < floor.x + floor.w
						&& j+1 <= floor.y
						&& j+1 > floor.y - floor.h)
					{
						parcelMap[i, j] = floor;
					}
				}
			}
		}

		for (var i = 0; i < Width; i++)
		{
			for (var j = 0; j < Height; j++)
			{
				var floor = parcelMap[i, j];
				if (floor != null)
				{
					var instance = Instantiate(floorPrefab, new Vector3(i+1, 0, j+1), Quaternion.identity, entitiesCanvas.transform);
					instance.name = $"Floor#{floor.id} ({floor.x}/{floor.y})";
					instance.GetComponent<Floor>().floorItem = floor;
				}
			}
		}

		InstantiateGround(floorGroundPrefab);
	}

	private void ProcessFloor(string json)
	{
		var response = JsonUtility.FromJson<FloorResponse>(json);
		if (response == null)
		{
			return;
		}

		DestroyAll();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		var floor = response.floor;
		GameManager.Instance.floor = floor;
		title.text = $"\"{floor.title}\", {floor.number} этаж";

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
				var r = floorMap[i, j];
				if (r != null)
				{
					var instance = Instantiate(r.id == GameManager.Instance.citizen.room_id ? openRoomPrefab : closedRoomPrefab,
						new Vector3(i+1, 0, j+1), Quaternion.identity, entitiesCanvas.transform);
					var room = instance.GetComponent<Room>();
					room.GetComponentInChildren<Renderer>().material.color = new Color(r.r, r.g, r.b, r.a);
					room.roomItem = r;
				}
				else
				{
					var instance = Instantiate(openRoomPrefab,
						new Vector3(i+1, 0, j+1), Quaternion.identity, entitiesCanvas.transform);
					var room = instance.GetComponent<Room>();
					room.GetComponentInChildren<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
					room.roomItem = new RoomItem();
					room.roomItem.x = i+1;
					room.roomItem.y = j+1;
					room.roomItem.w = 1;
					room.roomItem.h = 1;
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

				citizen.citizenItem = r.citizens[i];
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

		title.text = "Market";
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<Position>();
		DestroyAll<RoomType>();

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
			lot.lotItem = response.lots[i];

            row++;
        }
	}

	private void ProcessOrganizationTypes(string json)
	{
		var response = JsonUtility.FromJson<OrganizationTypesResponse>(json);
        if (response == null)
        {
            return;
        }

		title.text = "Choose organization type";
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<Position>();
		DestroyAll<RoomType>();
		DestroyAll<OrganizationType>();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.organization_types.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var organizationType = InstantiateObject<OrganizationType>(organizationTypePrefab, marketCanvas,
				$"{response.organization_types[i].title}", 100, 700, col, row);
			organizationType.organizationTypeItem = response.organization_types[i];

            row++;
        }
	}

	private void ProcessOrganizations(string json)
	{
		var response = JsonUtility.FromJson<OrganizationsResponse>(json);
        if (response == null)
        {
            return;
        }

		title.text = "Attach rooms";
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<Position>();
		DestroyAll<RoomType>();
		DestroyAll<OrganizationType>();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.organization.;.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var organizationType = InstantiateObject<OrganizationType>(organizationTypePrefab, marketCanvas,
				$"{response.organization_types[i].title}", 100, 700, col, row);
			organizationType.organizationTypeItem = response.organization_types[i];

            row++;
        }
	}

	private void ProcessOrganization(string json)
	{
		var response = JsonUtility.FromJson<OrganizationResponse>(json);
        if (response == null)
        {
            return;
        }

		title.text = "Attach rooms";
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<Position>();
		DestroyAll<RoomType>();
		DestroyAll<OrganizationType>();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.organization.;.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var organizationType = InstantiateObject<OrganizationType>(organizationTypePrefab, marketCanvas,
				$"{response.organization_types[i].title}", 100, 700, col, row);
			organizationType.organizationTypeItem = response.organization_types[i];

            row++;
        }
	}

	private void ProcessRentedRooms(string json)
	{
		var response = JsonUtility.FromJson<RentedRoomsResponse>(json);
        if (response == null)
        {
            return;
        }

		title.text = "Choose a room to attach";
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<Position>();
		DestroyAll<RoomType>();
		DestroyAll<OrganizationType>();

		InstantiateRentedRooms(response.rented_rooms);
	}

	public void InstantiateRentedRooms(List<RentedRoomItem> rentedRooms)
	{
        var col = 0;
        var row = 0;
		for (var i = 0; i < rentedRooms.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var rentedRoom = InstantiateObject<Items.RentedRoom>(rentedRoomItemPrefab, buildCanvas,
				$"{rentedRooms[i].title}", 100, 700, col, row);
			rentedRoom.rentedRoomItem = rentedRooms[i];

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
		DestroyAll<Position>();
		DestroyAll<RoomType>();
		DestroyAll<OrganizationType>();

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
			item.itemItem = response.items[i];

            row++;
        }
	}

	private void ProcessRoomTypes(string json)
	{
		var response = JsonUtility.FromJson<RoomTypesResponse>(json);
        if (response == null)
        {
            return;
        }

		InstantiateRoomTypes(response.room_types);
	}

	private T InstantiateObject<T>(GameObject prefab, GameObject parent, string title, int x, int y, int col, int row) where T : IItem
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
		DestroyAll<Position>();

        title.text = $"Tasks and positions";

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
			task.taskItem = response.tasks[i];

            row++;
        }

		col = 0;
        row = 0;
		for (var i = 0; i < response.positions.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var position = InstantiateObject<Position>(positionPrefab, tasksCanvas,
				$"{response.positions[i].title} ({(response.positions[i].is_free ? "free" : "busy")})", 500, 700, col, row);
			position.positionItem = response.positions[i];

            row++;
        }
	}

	public void InstantiateRoomTypes(List<RoomTypeItem> roomTypes)
	{
		DestroyAll<RoomType>();
		DestroyAll<OrganizationType>();

        var col = 0;
        var row = 0;
		for (var i = 0; i < roomTypes.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

            var roomType = NetworkManager.Instance.InstantiateObject<RoomType>(roomTypePrefab, buildCanvas,
				roomTypes[i].title,
				100, 700, col, row);
			roomType.roomTypeItem = roomTypes[i];

            row++;
        }
	}
}
