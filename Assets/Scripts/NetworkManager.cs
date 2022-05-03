using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Entities.Items;
using Models;

public class NetworkManager : Manager
{
	public const int Width = 8;
	public const int Height = 8;

	private string Host;
	private string Port;

	public GameObject mainButtonsPanel;
	public GameObject mainButtonPrefab;
	private GameObject marketButton;
	private GameObject inventoryButton;
	private GameObject reloadButton;
	private GameObject mapButton;
	private GameObject citizenButton;
	private GameObject zoomOutButton;
	private GameObject buildModeButton;
	private GameObject closeWindowButton;
	private GameObject organizationsButton;
	private GameObject createOrganizationButton;

	public GameObject itemSellWindow;

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
	public GameObject organizationPrefab;
	public GameObject roomTypePrefab;
	public GameObject rentedRoomPrefab;
	public GameObject requiredRoomTypePrefab;
	
	public GameObject galaxyExploredPrefab;
	public GameObject galaxyUnknownPrefab;
	public GameObject systemExploredPrefab;
	public GameObject systemUnknownPrefab;
	public GameObject planetExploredPrefab;
	public GameObject planetUnknownPrefab;
	public GameObject continentExploredPrefab;
	public GameObject continentUnknownPrefab;
	public GameObject regionOceanPrefab;
	public GameObject regionCoastPrefab;
	public GameObject regionPlainPrefab;
	public GameObject regionMountPrefab;
	public GameObject regionUnknownPrefab;
	public GameObject cityExploredPrefab;
	public GameObject cityUnknownPrefab;
	public GameObject blockExploredPrefab;
	public GameObject blockUnknownPrefab;
	public GameObject parcelExploredPrefab;
	public GameObject parcelUnknownPrefab;
	public GameObject floorGroundPrefab;

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

		Citizen(GameManager.Instance.me.id);

		marketButton = Instantiate(mainButtonPrefab, new Vector3(600, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        marketButton.GetComponentInChildren<Text>().text = "[A] Market";
        marketButton.GetComponent<Button>().onClick.AddListener(MarketButton);

		reloadButton = Instantiate(mainButtonPrefab, new Vector3(660, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        reloadButton.GetComponentInChildren<Text>().text = "[R] Reload";
        reloadButton.GetComponent<Button>().onClick.AddListener(ReloadButton);

		mapButton = Instantiate(mainButtonPrefab, new Vector3(720, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        mapButton.GetComponentInChildren<Text>().text = "[M] Show Map";
        mapButton.GetComponent<Button>().onClick.AddListener(MapButton);

		citizenButton = Instantiate(mainButtonPrefab, new Vector3(780, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        citizenButton.GetComponentInChildren<Text>().text = "[C] Show Floor";
        citizenButton.GetComponent<Button>().onClick.AddListener(CitizenButton);

		zoomOutButton = Instantiate(mainButtonPrefab, new Vector3(840, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        zoomOutButton.GetComponentInChildren<Text>().text = "[-] Zoom Out";
        zoomOutButton.GetComponent<Button>().onClick.AddListener(ZoomOutButton);

		inventoryButton = Instantiate(mainButtonPrefab, new Vector3(900, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        inventoryButton.GetComponentInChildren<Text>().text = "[I] Inventory";
        inventoryButton.GetComponent<Button>().onClick.AddListener(InventoryButton);

		// buildModeButton = Instantiate(mainButtonPrefab, new Vector3(960, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
		// buildModeButton.SetActive(false);
        // buildModeButton.GetComponentInChildren<Text>().text = "Build Mode";
        // buildModeButton.GetComponent<Button>().onClick.AddListener(() => {
		// 	RoomTypes(GameManager.Instance.floor.id);
        // });

		closeWindowButton = Instantiate(mainButtonPrefab, new Vector3(1020, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
		closeWindowButton.SetActive(false);
        closeWindowButton.GetComponentInChildren<Text>().text = "[Q] Close Window";
        closeWindowButton.GetComponent<Button>().onClick.AddListener(CloseWindowButton);

		organizationsButton = Instantiate(mainButtonPrefab, new Vector3(1080, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
        organizationsButton.GetComponentInChildren<Text>().text = "[O] Organizations";
        organizationsButton.GetComponent<Button>().onClick.AddListener(OrganizationsButton);

		createOrganizationButton = Instantiate(mainButtonPrefab, new Vector3(1080, 30, 0), Quaternion.identity, mainButtonsPanel.transform);
		createOrganizationButton.SetActive(false);
        createOrganizationButton.GetComponentInChildren<Text>().text = "Create Organization";
        createOrganizationButton.GetComponent<Button>().onClick.AddListener(OrganizationTypes);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			InventoryButton();
		}
		else if (Input.GetKeyDown(KeyCode.M))
		{
			MapButton();
		}
		else if (Input.GetKeyDown(KeyCode.Q))
		{
			CloseWindowButton();
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
			MarketButton();
		}
		else if (Input.GetKeyDown(KeyCode.O))
		{
			OrganizationsButton();
		}
		else if (Input.GetKeyDown(KeyCode.R))
		{
			ReloadButton();
		}
		else if (Input.GetKeyDown(KeyCode.C))
		{
			CitizenButton();
		}
		else if (Input.GetKeyDown(KeyCode.Minus))
		{
			ZoomOutButton();
		}
	}

	private void InventoryButton()
	{
		Inventory(GameManager.Instance.me.root_item_id);
	}

	private void ZoomOutButton()
	{
		switch (GameManager.Instance.state)
		{
			case 1:
				break;
			case 2:
				Universe();
				break;
			case 3:
				Galaxy(GameManager.Instance.currentSystem.galaxy_id);
				break;
			case 4:
				System(GameManager.Instance.currentPlanet.system_id);
				break;
			case 5:
				Planet(GameManager.Instance.currentContinent.planet_id);
				break;
			case 6:
				Continent(GameManager.Instance.currentRegion.continent_id);
				break;
			case 7:
				Region(GameManager.Instance.currentCity.region_id);
				break;
			case 8:
				City(GameManager.Instance.currentBlock.city_id);
				break;
			case 9:
				Block(GameManager.Instance.currentParcel.block_id);
				break;
			case 10:
				Parcel(GameManager.Instance.currentFloor.parcel_id);
				break;
		}
	}

	private void ReloadButton()
	{
		Reload();
	}

	private void MapButton()
	{
		Universe();
	}

	private void MarketButton()
	{
		Market();
	}

	private void CloseWindowButton()
	{
		Reload();
	}

	private void OrganizationsButton()
	{
		Organizations(GameManager.Instance.me.id);
	}

	private void Reload()
	{
		switch (GameManager.Instance.state)
		{
			case 1:
				Universe();
				break;
			case 2:
				Galaxy(GameManager.Instance.currentGalaxy.id);
				break;
			case 3:
				System(GameManager.Instance.currentSystem.id);
				break;
			case 4:
				Planet(GameManager.Instance.currentPlanet.id);
				break;
			case 5:
				Continent(GameManager.Instance.currentContinent.id);
				break;
			case 6:
				Region(GameManager.Instance.currentRegion.id);
				break;
			case 7:
				City(GameManager.Instance.currentCity.id);
				break;
			case 8:
				Block(GameManager.Instance.currentBlock.id);
				break;
			case 9:
				Parcel(GameManager.Instance.currentParcel.id);
				break;
			case 10:
				Floor(GameManager.Instance.currentFloor.id);
				break;
		}
	}

	private void CitizenButton()
	{
		Floor(GameManager.Instance.me.floor_id);
	}

	public void ShowButtons(Transform transform)
	{
		HideAllButtons();
		text.text = "";
		foreach(var ground in transform.GetComponentsInChildren<Entities.Cells.Ground>())
		{
			text.text += $"{ground.groundItem}";
			ground.ShowButtons();
		}
		foreach(var galaxy in transform.GetComponentsInChildren<Entities.Cells.Galaxy>())
		{
			text.text += $"{galaxy.galaxyItem}";
			galaxy.ShowButtons();
		}
		foreach(var system in transform.GetComponentsInChildren<Entities.Cells.System>())
		{
			text.text += $"{system.systemItem}";
			system.ShowButtons();
		}
		foreach(var planet in transform.GetComponentsInChildren<Entities.Cells.Planet>())
		{
			text.text += $"{planet.planetItem}";
			planet.ShowButtons();
		}
		foreach(var continent in transform.GetComponentsInChildren<Entities.Cells.Continent>())
		{
			text.text += $"{continent.continentItem}";
			continent.ShowButtons();
		}
		foreach(var region in transform.GetComponentsInChildren<Entities.Cells.Region>())
		{
			text.text += $"{region.regionItem}";
			region.ShowButtons();
		}
		foreach(var city in transform.GetComponentsInChildren<Entities.Cells.City>())
		{
			text.text += $"{city.cityItem}";
			city.ShowButtons();
		}
		foreach(var block in transform.GetComponentsInChildren<Entities.Cells.Block>())
		{
			text.text += $"{block.blockItem}";
			block.ShowButtons();
		}
		foreach(var parcel in transform.GetComponentsInChildren<Entities.Cells.Parcel>())
		{
			text.text += $"{parcel.parcelItem}";
			parcel.ShowButtons();
		}
		foreach(var floor in transform.GetComponentsInChildren<Entities.Cells.Floor>())
		{
			text.text += $"{floor.floorItem}";
			floor.ShowButtons();
		}
		foreach(var room in transform.GetComponentsInChildren<Entities.Cells.Room>())
		{
			text.text += $"{room.roomItem}";
			room.ShowButtons();
		}
		foreach(var citizen in transform.GetComponentsInChildren<Entities.Citizen>())
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

	public void HideWindows()
	{
		var objects = GameObject.FindGameObjectsWithTag("Window");
		foreach(var o in objects)
		{
			o.SetActive(false);
		}
	}

	private void DestroyItems()
	{
		DestroyAll<Entities.Items.Item>();
		DestroyAll<Entities.Items.Lot>();
		DestroyAll<Entities.Items.Task>();
		DestroyAll<Entities.Items.Position>();
		DestroyAll<Entities.Items.RoomType>();
		DestroyAll<Entities.Items.RentedRoom>();
		DestroyAll<Entities.Items.RequiredRoomType>();
		DestroyAll<Entities.Items.Organization>();
		DestroyAll<Entities.Items.OrganizationType>();
	}

	private void DestroyCells()
	{
		DestroyAll<Entities.Cells.Galaxy>();
		DestroyAll<Entities.Cells.System>();
		DestroyAll<Entities.Cells.Planet>();
		DestroyAll<Entities.Cells.Continent>();
		DestroyAll<Entities.Cells.Region>();
		DestroyAll<Entities.Cells.City>();
		DestroyAll<Entities.Cells.Block>();
		DestroyAll<Entities.Cells.Parcel>();
		DestroyAll<Entities.Cells.Floor>();
		DestroyAll<Entities.Cells.Ground>();
		DestroyAll<Entities.Cells.Room>();
		
		DestroyAll<Entities.Citizen>();
	}

	private void DestroyAll()
	{
		DestroyItems();
        HideWindows();
		DestroyCells();
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

		StartCoroutine(Request("market", "", ProcessMarket));
	}

	public void Citizen(int citizenId)
    {
		var query = $"citizen_id={citizenId}";
		
		StartCoroutine(Request("citizen", query, ProcessCitizen));
	}

	public void Universe()
    {
		StartCoroutine(Request("universe", "", ProcessUniverse));
	}

	public void Galaxy(int galaxyId)
    {
		var query = $"galaxy_id={galaxyId}";
		
		StartCoroutine(Request("galaxy", query, ProcessGalaxy));
	}

	public void GalaxyExplore(int galaxyId)
    {
		var query = $"galaxy_id={galaxyId}&explorer_id={GameManager.Instance.me.id}";
		
		StartCoroutine(Request("galaxy-explore", query, ProcessUniverse));
	}

	public void System(int systemId)
    {
		var query = $"system_id={systemId}";
		
		StartCoroutine(Request("system", query, ProcessSystem));
	}

	public void SystemExplore(int systemId)
    {
		var query = $"system_id={systemId}&explorer_id={GameManager.Instance.me.id}";
		
		StartCoroutine(Request("system-explore", query, ProcessGalaxy));
	}

	public void Planet(int planetId)
    {
		var query = $"planet_id={planetId}";
		
		StartCoroutine(Request("planet", query, ProcessPlanet));
	}

	public void PlanetExplore(int planetId)
    {
		var query = $"planet_id={planetId}&explorer_id={GameManager.Instance.me.id}";
		
		StartCoroutine(Request("planet-explore", query, ProcessSystem));
	}

	public void Continent(int continentId)
    {
		var query = $"continent_id={continentId}";
		
		StartCoroutine(Request("continent", query, ProcessContinent));
	}

	public void ContinentExplore(int continentId)
    {
		var query = $"continent_id={continentId}&explorer_id={GameManager.Instance.me.id}";
		
		StartCoroutine(Request("continent-explore", query, ProcessPlanet));
	}

	public void Region(int regionId)
    {
		var query = $"region_id={regionId}";
		
		StartCoroutine(Request("region", query, ProcessRegion));
	}

	public void RegionExplore(int regionId)
    {
		var query = $"region_id={regionId}&explorer_id={GameManager.Instance.me.id}";
		
		StartCoroutine(Request("region-explore", query, ProcessContinent));
	}

	public void City(int cityId)
    {
		var query = $"city_id={cityId}";
		
		StartCoroutine(Request("city", query, ProcessCity));
	}

	public void Block(int blockId)
    {
		var query = $"block_id={blockId}";
		
		StartCoroutine(Request("block", query, ProcessBlock));
	}

	public void Parcel(int parcelId)
    {
		var query = $"parcel_id={parcelId}";
		
		StartCoroutine(Request("parcel", query, ProcessParcel));
	}

	public void CityExplore(int cityId)
    {
		var query = $"city_id={cityId}&explorer_id={GameManager.Instance.me.id}";
		
		StartCoroutine(Request("city-explore", query, ProcessRegion));
	}

	public void BlockExplore(int blockId)
    {
		var query = $"block_id={blockId}&explorer_id={GameManager.Instance.me.id}";
		
		StartCoroutine(Request("block-explore", query, ProcessCity));
	}

	public void ParcelClaim(int parcelId)
    {
		var query = $"parcel_id={parcelId}&owner_id={GameManager.Instance.me.id}";
		
		StartCoroutine(Request("parcel-claim", query, ProcessBlock));
	}

	public void CreateFloor(int parcelId, int floorTypeId, int x, int y, int z, int w, int h)
    {
		var query = $"parcel_id={parcelId}&floor_type_id={floorTypeId}&x={x}&y={y}&z={z}&w={w}&h={h}";
		
		StartCoroutine(Request("floor-create", query, ProcessParcel));
	}

	public void CreateRoom(int floorId, int roomTypeId, int x, int y, int w, int h)
    {
		var query = $"floor_id={floorId}&room_type_id={roomTypeId}&x={x}&y={y}&w={w}&h={h}";
		
		StartCoroutine(Request("room-create", query, ProcessFloor));
	}

	public void AttachRoomToOrganization(int organizationId, int roomId)
    {
		var query = $"organization_id={organizationId}&room_id={roomId}";
		
		StartCoroutine(Request("attach-room-to-organization", query, (result) =>
		{
			var o = JsonUtility.FromJson<OrganizationResponse>(result);
			NetworkManager.Instance.InstantiateRequiredRoomTypes(o.organization);
		}));
	}

	public void Floor(int floorId)
    {
		var query = $"floor_id={floorId}";
		
		StartCoroutine(Request("floor", query, ProcessFloor));
	}

	public void MoveIntoRoom(int roomId)
    {
		var query = $"citizen_id={GameManager.Instance.me.id}&room_id={roomId}";
		
		StartCoroutine(Request("move-into-room", query, ProcessMoveIntoRoom));
	}

	public void Inventory(int rootItemId)
    {
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"root_item_id={rootItemId}";
		
		StartCoroutine(Request("inventory", query, ProcessInventory));
	}

	public void RoomTypes()
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		StartCoroutine(Request("room-types", "", ProcessRoomTypes));
	}

	public void Organizations(int ownerId)
	{
		organizationsButton.SetActive(false);
		createOrganizationButton.SetActive(true);
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"owner_id={ownerId}";

		StartCoroutine(Request("organizations", query, ProcessOrganizations));
	}

	public void OrganizationTypes()
	{
		createOrganizationButton.SetActive(false);
		organizationsButton.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		StartCoroutine(Request("organization-types", "", ProcessOrganizationTypes));
	}

	public void RentedRooms(int roomTypeId)
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"renter_id={GameManager.Instance.me.id}&room_type_id={roomTypeId}";

		StartCoroutine(Request("rented-rooms", query, ProcessRentedRooms));
	}

	public void DetachRoom(int roomId)
	{
		var query = $"room_id={roomId}";

		StartCoroutine(Request("detach-room", query, (result) =>
		{
			var o = JsonUtility.FromJson<OrganizationResponse>(result);
			NetworkManager.Instance.InstantiateRequiredRoomTypes(o.organization);
		}));
	}

	public void OrganizationCreate(int organizationTypeId)
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"organization_type_id={organizationTypeId}&owner_id={GameManager.Instance.me.id}";

		StartCoroutine(Request("organization-create", query, ProcessOrganizations));
	}

	public void ItemSell(ItemItem item)
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		title.text = $"Sell item \"{item.type_title}\"";
		HideAllButtons();
		DestroyItems();
        HideWindows();

		GameManager.Instance.newLot = new();
		GameManager.Instance.newLot.item_id = item.id;
		GameManager.Instance.newLot.item_type_id = item.type_id;
		GameManager.Instance.newLot.item_type_title = item.type_title;
		GameManager.Instance.newLot.owner_id = item.owner_id;
        itemSellWindow.SetActive(true);
	}

	public void LotCreate(LotItem lot)
	{
		shading.SetActive(true);
		closeWindowButton.SetActive(shading.activeSelf);

		var query = $"owner_id={lot.owner_id}&item_id={lot.item_id}&quantity={lot.quantity}&price={lot.price}";

		StartCoroutine(Request("lot-create", query, ProcessMarket));
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
		
		StartCoroutine(Request("tasks", query, ProcessTasks));
	}

	public void TaskAccept(int citizenId, int taskId)
	{
		var query = $"citizen_id={citizenId}&task_id={taskId}";
		
		StartCoroutine(Request("task-accept", query, ProcessTasks));
	}

	public void PositionRequest(int citizenId, int positionId)
	{
		var query = $"citizen_id={citizenId}&position_id={positionId}";
		
		StartCoroutine(Request("position-request", query, ProcessTasks));
	}

	private void ProcessMoveIntoRoom(string json)
	{
		var response = JsonUtility.FromJson<MoveIntoRoomResponse>(json);
		if (response == null)
		{
			return;
		}

		GameManager.Instance.me = response.citizen;
		ProcessFloor(json);
	}

	private void InstantiateGround(GameObject prefab)
	{
		for (var i = 0; i < Width; i++)
		{
			for (var j = 0; j < Height; j++)
			{
				var instance = Instantiate(prefab, new Vector3(i+1, 0, j+1), Quaternion.identity, entitiesCanvas.transform);
				instance.name = $"Ground ({i+1}/{j+1})";
				var ground = instance.GetComponent<Entities.Cells.Ground>();
				ground.groundItem.x = i+1;
				ground.groundItem.y = j+1;
			}
		}
	}

	private void ProcessUniverse(string json)
	{
		var response = JsonUtility.FromJson<UniverseResponse>(json);
		if (response == null)
		{
			return;
		}

		var universe = response.universe;
		GameManager.Instance.currentUniverse = universe;
		GameManager.Instance.state = GameManager.Universe;
		title.text = $"Universe \"{universe.title}\"";

		HideAllButtons();
		DestroyAll();
        HideWindows();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		foreach(var galaxy in universe.galaxies)
		{
			var x = galaxy.number * 2;

			var prefab = galaxy.systems.Count == 0 ? galaxyUnknownPrefab : galaxyExploredPrefab;

			var instance = Instantiate(prefab, new Vector3(x, 0, 5), Quaternion.identity, entitiesCanvas.transform);
			instance.name = $"Galaxy#{galaxy.id} ({galaxy.number})";
			instance.GetComponent<Entities.Cells.Galaxy>().galaxyItem = galaxy;
		}
	}

	private void ProcessGalaxy(string json)
	{
		var response = JsonUtility.FromJson<GalaxyResponse>(json);
		if (response == null)
		{
			return;
		}

		var galaxy = response.galaxy;
		GameManager.Instance.currentGalaxy = galaxy;
		GameManager.Instance.state = GameManager.Galaxy;
		title.text = $"Galaxy \"{galaxy.title}\"";

		HideAllButtons();
		DestroyAll();
        HideWindows();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		foreach(var system in galaxy.systems)
		{
			var x = system.number * 2;

			var prefab = system.planets.Count == 0 ? systemUnknownPrefab : systemExploredPrefab;

			var instance = Instantiate(prefab, new Vector3(x, 0, 8), Quaternion.identity, entitiesCanvas.transform);
			instance.name = $"System#{system.id} ({system.number})";
			instance.GetComponent<Entities.Cells.System>().systemItem = system;
		}
	}

	private void ProcessSystem(string json)
	{
		var response = JsonUtility.FromJson<SystemResponse>(json);
		if (response == null)
		{
			return;
		}

		var system = response.system;
		GameManager.Instance.currentSystem = system;
		GameManager.Instance.state = GameManager.System;
		title.text = $"System \"{system.title}\"";

		HideAllButtons();
		DestroyAll();
        HideWindows();
HideWindows();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		var starInstance = Instantiate(systemExploredPrefab, new Vector3(0, 4f, 0), Quaternion.identity, entitiesCanvas.transform);
		starInstance.transform.localScale = Vector3.one * 8;

		foreach(var planet in system.planets)
		{
			var x = -planet.number * 2 - 4;

			var prefab = planet.continents.Count == 0 ? planetUnknownPrefab : planetExploredPrefab;

			var instance = Instantiate(prefab, new Vector3(x, 0, 0), Quaternion.identity, entitiesCanvas.transform);
			instance.name = $"Planet#{planet.id} ({planet.number})";
			instance.GetComponent<Entities.Cells.Planet>().planetItem = planet;
		}
	}

	private void ProcessPlanet(string json)
	{
		var response = JsonUtility.FromJson<PlanetResponse>(json);
		if (response == null)
		{
			return;
		}

		var planet = response.planet;
		GameManager.Instance.currentPlanet = planet;
		GameManager.Instance.state = GameManager.Planet;
		title.text = $"Planet \"{planet.title}\"";

		HideAllButtons();
		DestroyAll();
        HideWindows();
        HideWindows();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		foreach(var continent in planet.continents)
		{
			var z = -continent.number * 3 + 8;

			var continentPrefab = continent.regions.Count == 0 ? continentUnknownPrefab : continentExploredPrefab;

			var continentInstance = Instantiate(continentPrefab, new Vector3(0, z, 0), Quaternion.identity, entitiesCanvas.transform);
			continentInstance.name = $"Continent#{continent.id} ({continent.number})";
			continentInstance.GetComponent<Entities.Cells.Continent>().continentItem = continent;

			if (continent.regions.Count == 0)
			{
				for (var i = 0; i < planet.size; i++)
				{
					for (var j = 0; j < planet.size; j++)
					{
						var regionInstance = Instantiate(regionUnknownPrefab, new Vector3(i+1, z, j+1), Quaternion.identity, continentInstance.transform);
						Destroy(regionInstance.GetComponent<BoxCollider>());
					}
				}
			}
			else
			{
				foreach(var region in continent.regions)
				{
					var regionInstance = Instantiate(regionUnknownPrefab, new Vector3(region.x, z, region.y), Quaternion.identity, continentInstance.transform);
					var color = region.z > 0 ? new Color(0.5f, 1.0f, 0.5f) : new Color(0.0f, 0.5f, 1.0f);
					regionInstance.GetComponentInChildren<Renderer>().material.color = color;
					Destroy(regionInstance.GetComponent<BoxCollider>());
				}
			}
		}
	}

	private void ProcessContinent(string json)
	{
		var response = JsonUtility.FromJson<ContinentResponse>(json);
		if (response == null)
		{
			return;
		}

		var continent = response.continent;
		GameManager.Instance.currentContinent = continent;
		GameManager.Instance.state = GameManager.Continent;
		title.text = $"Continent \"{continent.title}\"";

		HideAllButtons();
		DestroyAll();
        HideWindows();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		foreach(var region in continent.regions)
		{
			GameObject prefab = null;
			if (region.cities_count == 0)
			{
				prefab = regionUnknownPrefab;
			}
			else
			{
				switch (region.z)
				{
					case -1:
						prefab = regionOceanPrefab;
						break;
					case 0:
						prefab = regionCoastPrefab;
						break;
					case 1:
						prefab = regionPlainPrefab;
						break;
					case 2:
						prefab = regionMountPrefab;
						break;
				}
			}
			
			var instance = Instantiate(prefab, new Vector3(region.x, 0, region.y), Quaternion.identity, entitiesCanvas.transform);
			instance.name = $"Region#{region.id} ({region.x}/{region.y})";
			instance.GetComponent<Entities.Cells.Region>().regionItem = region;
			if (region.cities_count == 0)
			{
				var color = region.z > 0 ? new Color(0.5f, 1.0f, 0.5f) : new Color(0.0f, 0.5f, 1.0f);
				instance.GetComponentInChildren<Renderer>().material.color = color;
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
		GameManager.Instance.currentRegion = region;
		GameManager.Instance.state = GameManager.Region;
		title.text = $"Region \"{region.title}\"";

		HideAllButtons();
		DestroyAll();
        HideWindows();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		foreach(var city in response.cities)
		{
			var prefab = city.blocks.Count == 0 ? cityUnknownPrefab : cityExploredPrefab;
			var instance = Instantiate(prefab, new Vector3(city.x, 0, city.y), Quaternion.identity, entitiesCanvas.transform);
			instance.name = $"City#{city.id} ({city.x}/{city.y})";
			instance.GetComponent<Entities.Cells.City>().cityItem = city;
		}
	}

	private void ProcessCity(string json)
	{
		var response = JsonUtility.FromJson<CityResponse>(json);
		if (response == null)
		{
			return;
		}

		var city = response.city;
		GameManager.Instance.currentCity = city;
		GameManager.Instance.state = GameManager.City;
		title.text = $"City \"{city.title}\"";

		HideAllButtons();
		DestroyAll();
        HideWindows();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		foreach(var block in city.blocks)
		{
			var prefab = block.parcels.Count == 0 ? blockUnknownPrefab : blockExploredPrefab;
			var instance = Instantiate(prefab, new Vector3(block.x, 0, block.y), Quaternion.identity, entitiesCanvas.transform);
			instance.name = $"Block#{block.id} ({block.x}/{block.y})";
			instance.GetComponent<Entities.Cells.Block>().blockItem = block;
		}
	}

	private void ProcessBlock(string json)
	{
		var response = JsonUtility.FromJson<BlockResponse>(json);
		if (response == null)
		{
			return;
		}

		var block = response.block;
		GameManager.Instance.currentBlock = block;
		GameManager.Instance.state = GameManager.Block;
		title.text = $"Block \"{block.title}\"";

		HideAllButtons();
		DestroyAll();
        HideWindows();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		foreach(var parcel in block.parcels)
		{
			var prefab = parcel.owner_id == 0 ? parcelUnknownPrefab : parcelExploredPrefab;
			var instance = Instantiate(prefab, new Vector3(parcel.x, 0, parcel.y), Quaternion.identity, entitiesCanvas.transform);
			instance.name = $"Parcel#{parcel.id} ({parcel.x}/{parcel.y})";
			instance.GetComponent<Entities.Cells.Parcel>().parcelItem = parcel;
		}
	}

	private void ProcessParcel(string json)
	{
		var response = JsonUtility.FromJson<ParcelResponse>(json);
		if (response == null)
		{
			return;
		}

		var parcel = response.parcel;
		GameManager.Instance.currentParcel = parcel;
		GameManager.Instance.state = GameManager.Parcel;
		title.text = $"Parcel \"{parcel.title}\"";

		HideAllButtons();
		DestroyAll();
        HideWindows();
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
					instance.GetComponent<Entities.Cells.Floor>().floorItem = floor;
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

		HideAllButtons();
		DestroyAll();
        HideWindows();
		shading.SetActive(false);
		closeWindowButton.SetActive(shading.activeSelf);

		var floor = response.floor;
		GameManager.Instance.currentFloor = floor;
		GameManager.Instance.state = GameManager.Floor;
		title.text = $"\"{floor.title}\", {floor.z} этаж";

		var width = Width * floor.w;
		var height = Height * floor.h;

		var floorMap = new RoomItem[width, height];
		foreach(var r in floor.rooms)
		{
			for (var i = 0; i < width; i++)
			{
				for (var j = 0; j < height; j++)
				{
					if (i+1 >= r.x && i+1 < r.x + r.w && j+1 <= r.y && j+1 > r.y - r.h)
					{
						floorMap[i, j] = r;
					}
				}
			}
		}

		for (var i = 0; i < width; i++)
		{
			for (var j = 0; j < height; j++)
			{
				var r = floorMap[i, j];
				if (r != null)
				{
					var instance = Instantiate(r.id == GameManager.Instance.me.room_id ? openRoomPrefab : closedRoomPrefab,
						new Vector3(i+1, 0, j+1), Quaternion.identity, entitiesCanvas.transform);
					var room = instance.GetComponent<Entities.Cells.Room>();
					room.GetComponentInChildren<Renderer>().material.color = new Color(r.r, r.g, r.b, r.a);
					room.roomItem = r;
				}
				else
				{
					var instance = Instantiate(openRoomPrefab,
						new Vector3(i+1, 0, j+1), Quaternion.identity, entitiesCanvas.transform);
					var room = instance.GetComponent<Entities.Cells.Room>();
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
			if (r.id != GameManager.Instance.me.room_id)
			{
				continue;
			}

			chatController.ReplaceChat(r.messages);

			for (var i = 0; i < r.citizens.Count; i++)
			{
				var citizenPrefabInstance = Instantiate(citizenPrefab, new Vector3(i+3, 0, 11), Quaternion.identity, entitiesCanvas.transform);
				var citizen = citizenPrefabInstance.GetComponent<Entities.Citizen>();

				if (r.citizens[i].id == GameManager.Instance.me.id)
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
		DestroyItems();
        HideWindows();

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
		DestroyItems();
        HideWindows();

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

		title.text = "Manage organizations";
		DestroyItems();
        HideWindows();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.organizations.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			foreach(var attachedRoom in response.organizations[i].attached_rooms)
			{
				foreach(var requiredRoomType in response.organizations[i].type.requirements.room_types)
				{
					if (requiredRoomType.id == attachedRoom.type_id)
					{
						requiredRoomType.is_attached = true;
						break;
					}
				}
			}

			var requiredRoomTypeAttachedCount = 0;
			foreach(var requiredRoomType in response.organizations[i].type.requirements.room_types)
			{
				if (requiredRoomType.is_attached)
				{
					requiredRoomTypeAttachedCount++;
				}
			}

			var isAttached = requiredRoomTypeAttachedCount == response.organizations[i].type.requirements.room_types.Count;

			var color = Color.red;
			if (isAttached)
			{
				color = Color.green;
			}

			var organization = InstantiateObject<Organization>(organizationPrefab, buildCanvas,
				$"{response.organizations[i].title} ({isAttached})", 100, 700, col, row);
			organization.organizationItem = response.organizations[i];
			organization.GetComponent<Image>().color = color;

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
		DestroyItems();
        HideWindows();

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

			var rentedRoom = InstantiateObject<RentedRoom>(rentedRoomPrefab, buildCanvas,
				$"{rentedRooms[i].title}", 100, 700, col, row);
			rentedRoom.rentedRoomItem = rentedRooms[i];

            row++;
        }
	}

	public void InstantiateRequiredRoomTypes(OrganizationItem organizationItem)
	{
		foreach(var attachedRoom in organizationItem.attached_rooms)
		{
			foreach(var requiredRoomType in organizationItem.type.requirements.room_types)
			{
				if (requiredRoomType.id == attachedRoom.type_id)
				{
					requiredRoomType.is_attached = true;
					requiredRoomType.attached_room_id = attachedRoom.id;
					break;
				}
			}
		}
		var requiredRoomTypes = organizationItem.type.requirements.room_types;
		
		title.text = "Choose a required room";
		DestroyItems();
        HideWindows();

        var col = 0;
        var row = 0;
		for (var i = 0; i < requiredRoomTypes.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var requiredRoomType = InstantiateObject<RequiredRoomType>(requiredRoomTypePrefab, buildCanvas,
				$"{requiredRoomTypes[i].title} ({requiredRoomTypes[i].is_attached})", 100, 700, col, row);
			requiredRoomType.requiredRoomTypeItem = requiredRoomTypes[i];

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

		GameManager.Instance.me = response.citizen;
	}

    private void ProcessInventory(string json)
	{
		var response = JsonUtility.FromJson<InventoryResponse>(json);
        if (response == null)
        {
            return;
        }

        title.text = $"{response.title}";
		HideWindows();
		HideAllButtons();
		DestroyItems();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.items.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

            var item = InstantiateObject<Entities.Items.Item>(itemPrefab, inventoryCanvas,
				$"{response.items[i].type_title} {response.items[i].quantity}", 100, 700, col, row);
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

        title.text = $"Tasks and positions";
		DestroyItems();
        HideWindows();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.tasks.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var task = InstantiateObject<Entities.Items.Task>(taskPrefab, tasksCanvas,
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
		DestroyItems();
        HideWindows();

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
