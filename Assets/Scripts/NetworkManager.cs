﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
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
	public GameObject locationTypePrefab;
	public GameObject roomTypePrefab;
	
	public GameObject defaultGroundPrefab;
	public GameObject regionGroundPrefab;
	public GameObject cityGroundPrefab;
	public GameObject blockGroundPrefab;
	public GameObject parcelGroundPrefab;
	public GameObject floorGroundPrefab;

	public GameObject defaultAddressPrefab;
	public GameObject regionAddressPrefab;
	public GameObject cityPrefab;
	public GameObject blockPrefab;
	public GameObject parcelPrefab;
	public GameObject floorPrefab;

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
		Region(1);

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

		buildModeButton = Instantiate(mainButtonPrefab, new Vector3(960, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
		buildModeButton.SetActive(false);
        buildModeButton.GetComponentInChildren<Text>().text = "Build Mode";
        buildModeButton.GetComponent<Button>().onClick.AddListener(() => {
            HideAllButtons();
			BuildRoom(GameManager.Instance.floor.id);
        });

		closeWindowButton = Instantiate(mainButtonPrefab, new Vector3(1020, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
		closeWindowButton.SetActive(false);
        closeWindowButton.GetComponentInChildren<Text>().text = "Close Window";
        closeWindowButton.GetComponent<Button>().onClick.AddListener(() => {
            HideAllButtons();
			DestroyAll<Item>();
			DestroyAll<Lot>();
			DestroyAll<Task>();
			DestroyAll<Position>();
			DestroyAll<LocationType>();
			DestroyAll<RoomType>();
			shading.SetActive(false);
			closeWindowButton.SetActive(shading.activeSelf);
        });
	}

	public void ShowButtons(Transform transform)
	{
		HideAllButtons();
		text.text = "";
		foreach(var ground in transform.GetComponentsInChildren<Ground>())
		{
			text.text += $"{ground.item}";
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

	private void DestroyAll()
	{
		DestroyAll<Ground>();
		DestroyAll<Region>();
		DestroyAll<City>();
		DestroyAll<Block>();
		DestroyAll<Parcel>();
		DestroyAll<Room>();
		DestroyAll<Citizen>();
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<Position>();
		DestroyAll<LocationType>();
		DestroyAll<RoomType>();
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

	public void RegisterLocation(int addressId, int locationTypeId)
    {
		var query = $"address_id={addressId}&location_type_id={locationTypeId}&citizen_id={GameManager.Instance.citizen.id}";
		
		StartCoroutine(Request("location-register", query, (result) =>
		{
			ProcessAddress(result);
		}));
	}

	public void RegisterRoom(int floorId, int roomTypeId)
    {
		var query = $"floor_id={floorId}&room_type_id={roomTypeId}";
		
		StartCoroutine(Request("room-register", query, (result) =>
		{
			ProcessFloor(result);
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
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"root_item_id={rootItemId}";
		
		StartCoroutine(Request("inventory", query, (result) =>
		{
			ProcessInventory(result);
		}));
	}

	public void BuildLocation(int addressId)
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"address_id={addressId}";
		
		StartCoroutine(Request("build-location", query, (result) =>
		{
			ProcessBuildLocation(result);
		}));
	}

	public void BuildRoom(int floorId)
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"floor_id={floorId}";
		
		StartCoroutine(Request("build-room", query, (result) =>
		{
			ProcessBuildRoom(result);
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
		closeWindowButton.SetActive(shading.activeSelf);

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

	public void PositionRequest(int citizenId, int positionId)
	{
		var query = $"citizen_id={citizenId}&position_id={positionId}";
		
		StartCoroutine(Request("position-request", query, (result) =>
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
		buildModeButton.SetActive(false);

		DestroyAll<Ground>();
		DestroyAll<Address>();
		DestroyAll<Location>();
		DestroyAll<Room>();
		DestroyAll<Citizen>();
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<Position>();
		DestroyAll<LocationType>();
		DestroyAll<RoomType>();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

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
				InstantiateGround2(addressMap[i, j], i+1, j+1);
				if (addressMap[i, j] != null)
				{
					InstantiateAddress(addressMap[i, j], i+1, j+1);
				}
			}
		}
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
				ground.item.x = i+1;
				ground.item.y = j+1;
			}
		}
	}

	private void InstantiateGround2(AddressItem a, int x, int y)
	{
		GameObject groundPrefab = null;
		GroundType typeId;
		switch (GameManager.Instance.address.type_id)
		{
			case AddressTypes.World:
				typeId = GroundType.Region;
				groundPrefab = regionGroundPrefab;
				break;
			case AddressTypes.Region:
				typeId = GroundType.City;
				groundPrefab = cityGroundPrefab;
				break;
			case AddressTypes.City:
				typeId = GroundType.Block;
				groundPrefab = blockGroundPrefab;
				break;
			case AddressTypes.Block:
				typeId = GroundType.LandLot;
				groundPrefab = parcelGroundPrefab;
				break;
			case AddressTypes.LandLot:
				typeId = GroundType.Facility;
				groundPrefab = floorGroundPrefab;
				break;
			default:
				typeId = GroundType.Undefined;
				groundPrefab = defaultGroundPrefab;
				break;
		}
		
		var groundPrefabInstance = Instantiate(groundPrefab, new Vector3(x, 0, y), Quaternion.identity, entitiesCanvas.transform);
		groundPrefabInstance.name = $"Ground.{typeId} ({x}/{y})";
		var ground = groundPrefabInstance.GetComponent<Ground>();

		ground.item.x = x;
		ground.item.y = y;
		ground.item.type_id = typeId;
		ground.item.address = a;
	}

	private void InstantiateAddress(AddressItem a, int x, int y)
	{
		GameObject addressPrefab = null;
		switch (GameManager.Instance.address.type_id)
		{
			case AddressTypes.World:
				addressPrefab = regionAddressPrefab;
				break;
			case AddressTypes.Region:
				addressPrefab = cityPrefab;
				break;
			case AddressTypes.City:
				addressPrefab = blockPrefab;
				break;
			case AddressTypes.Block:
				addressPrefab = parcelPrefab;
				break;
			case AddressTypes.LandLot:
				addressPrefab = floorPrefab;
				break;
			default:
				addressPrefab = defaultAddressPrefab;
				break;
		}
		
		var addressPrefabInstance = Instantiate(addressPrefab, new Vector3(x, 0, y), Quaternion.identity, entitiesCanvas.transform);
		addressPrefabInstance.name = $"Address.{a.type_id} ({x}/{y})";
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
		locationPrefabInstance.name = $"Location.{a.location.type_id} ({x}/{y})";
		var location = locationPrefabInstance.GetComponent<Location>();

		location.item = a.location;
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

		InstantiateGround(regionGroundPrefab);
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

		InstantiateGround(cityGroundPrefab);
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

		InstantiateGround(blockGroundPrefab);
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

		foreach(var floor in parcel.floors)
		{
			var instance = Instantiate(floorPrefab, new Vector3(floor.x, 0, floor.y), Quaternion.identity, entitiesCanvas.transform);
			instance.name = $"Floor#{floor.id} ({floor.x}/{floor.y})";
			instance.GetComponent<Floor>().floorItem = floor;
		}

		InstantiateGround(floorGroundPrefab);
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

		DestroyAll<Ground>();
		DestroyAll<Address>();
		DestroyAll<Location>();
		DestroyAll<Room>();
		DestroyAll<Citizen>();
		DestroyAll<Item>();
		DestroyAll<Lot>();
		DestroyAll<Task>();
		DestroyAll<Position>();
		DestroyAll<LocationType>();
		DestroyAll<RoomType>();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		var floor = response.floor;
		GameManager.Instance.floor = floor;
		title.text = $"{GameManager.Instance.location.type_title}, {floor.number} этаж";

		buildModeButton.SetActive(GameManager.Instance.citizen.id == GameManager.Instance.location.owner_id);
		
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
		else if (r.type_id == RoomTypes.Office)
		{
			color = Color.black;
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
		DestroyAll<Position>();
		DestroyAll<LocationType>();
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
		DestroyAll<Position>();
		DestroyAll<LocationType>();
		DestroyAll<RoomType>();

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

	private void ProcessBuildLocation(string json)
	{
		var response = JsonUtility.FromJson<BuildLocationResponse>(json);
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

	private void ProcessBuildRoom(string json)
	{
		var response = JsonUtility.FromJson<BuildRoomResponse>(json);
        if (response == null)
        {
            return;
        }

		DestroyAll<RoomType>();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.room_types.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

            var roomType = InstantiateObject<RoomType>(roomTypePrefab, buildCanvas,
				response.room_types[i].title,
				100, 700, col, row);
			roomType.item = response.room_types[i];

            row++;
        }
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

        title.text = $"{GameManager.Instance.location.type_title}: HR Department";

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
			position.item = response.positions[i];

            row++;
        }
	}
}
