using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

	public GameObject mainButtonsPanelPrefab;
	public GameObject dealPopupPrefab;
	public GameObject noticePopupPrefab;
	public GameObject loginPopupPrefab;
	public GameObject chatPanelPrefab;
	public GameObject organizationWindowPrefab;
	public GameObject citizenWindowPrefab;
	public GameObject computerWindowPrefab;

	public GameObject uiCanvas;
	public GameObject mapCanvas;

	public Text text;

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
		var dir = "";
		switch (Application.platform)
		{
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.WindowsEditor:
			case RuntimePlatform.LinuxEditor:
				dir = "/StreamingAssets";
				break;
			case RuntimePlatform.OSXPlayer:
				dir = "/Resources/Data/StreamingAssets";
				break;
			case RuntimePlatform.WindowsPlayer:
				dir = "/side_Data/side_Data/StreamingAssets";
				break;
		}

		try
		{
			LoadConfig($"{dir}/config.txt");
		}
		catch (Exception e)
		{
			InstantiateNoticePopup("ERROR", $"{e}");
		}
		
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

	private void LoadConfig(string filepath)
	{
		var path = Application.dataPath + filepath;
		using (StreamReader reader = new StreamReader(path))
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
	}

	private void Start()
	{
		Instantiate(chatPanelPrefab, uiCanvas.transform);
		Instantiate(mainButtonsPanelPrefab, uiCanvas.transform);
		InstantiateLoginPopup();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			CloseWindowButton();
		}
		
		if (GameManager.IsShortcutsActive)
		{
			if (Input.GetKeyDown(KeyCode.M))
			{
				ShowMapButton();
			}
			else if (Input.GetKeyDown(KeyCode.O))
			{
				OrganizationsButton();
			}
			else if (Input.GetKeyDown(KeyCode.W))
			{
				InternetButton();
			}
			else if (Input.GetKeyDown(KeyCode.I))
			{
				ItemsButton();
			}
			else if (Input.GetKeyDown(KeyCode.R))
			{
				ReloadButton();
			}
			else if (Input.GetKeyDown(KeyCode.Equals))
			{
				CenterMeButton();
			}
			else if (Input.GetKeyDown(KeyCode.Minus))
			{
				ZoomOutButton();
			}
			else if (Input.GetKeyDown(KeyCode.P))
			{
				ProfileButton();
			}
			else if (Input.GetKeyDown(KeyCode.C))
			{
				ComputerButton();
			}
		}
	}

	public void ProfileButton()
	{
		InstantiateCitizen(GameManager.Instance.me);
	}

	public void ItemsButton()
	{
		InstantiateCitizen(GameManager.Instance.me, "Items");
	}

	public void InternetButton()
	{
		InstantiateComputer("Internet");
	}

	public void ComputerButton()
	{
		InstantiateComputer();
	}

	public void ZoomOutButton()
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
			default:
				Parcel(GameManager.Instance.currentFloor.parcel_id);
				break;
		}
	}

	public void ShowMapButton()
	{
		Universe();
	}

	public void CloseWindowButton()
	{
		HideAllButtons();
		DestroyItems();
	}

	public void OrganizationsButton()
	{
		InstantiateCitizen(GameManager.Instance.me, "Organizations");
	}

	public void ReloadButton()
	{
		Me();
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
			default:
				Floor(GameManager.Instance.currentFloor.id);
				break;
		}
	}

	public void CenterMeButton()
	{
		Floor(GameManager.Instance.me.floor_id);
	}

	public void ShowButtons(Transform transform)
	{
		HideAllButtons();
		text.text = "";
		foreach(var ground in transform.GetComponentsInChildren<Entities.Cells.Ground>())
		{
			text.text += $"\n{ground.groundItem}";
			ground.ShowButtons();
		}
		foreach(var galaxy in transform.GetComponentsInChildren<Entities.Cells.Galaxy>())
		{
			text.text += $"\n{galaxy.galaxyItem}";
			galaxy.ShowButtons();
		}
		foreach(var system in transform.GetComponentsInChildren<Entities.Cells.System>())
		{
			text.text += $"\n{system.systemItem}";
			system.ShowButtons();
		}
		foreach(var planet in transform.GetComponentsInChildren<Entities.Cells.Planet>())
		{
			text.text += $"\n{planet.planetItem}";
			planet.ShowButtons();
		}
		foreach(var continent in transform.GetComponentsInChildren<Entities.Cells.Continent>())
		{
			text.text += $"\n{continent.continentItem}";
			continent.ShowButtons();
		}
		foreach(var region in transform.GetComponentsInChildren<Entities.Cells.Region>())
		{
			text.text += $"\n{region.regionItem}";
			region.ShowButtons();
		}
		foreach(var city in transform.GetComponentsInChildren<Entities.Cells.City>())
		{
			text.text += $"\n{city.cityItem}";
			city.ShowButtons();
		}
		foreach(var block in transform.GetComponentsInChildren<Entities.Cells.Block>())
		{
			text.text += $"\n{block.blockItem}";
			block.ShowButtons();
		}
		foreach(var parcel in transform.GetComponentsInChildren<Entities.Cells.Parcel>())
		{
			text.text += $"\n{parcel.parcelItem}";
			parcel.ShowButtons();
		}
		foreach(var floor in transform.GetComponentsInChildren<Entities.Cells.Floor>())
		{
			text.text += $"\n{floor.floorItem}";
			floor.ShowButtons();
		}
		foreach(var room in transform.GetComponentsInChildren<Entities.Cells.Room>())
		{
			text.text += $"\n{room.roomItem}";
			room.ShowButtons();
		}
		foreach(var citizen in transform.GetComponentsInChildren<Entities.Citizen>())
		{
			text.text += $"\n{citizen.citizenItem}";
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

	private void DestroyWindows()
	{
		foreach (var go in GameObject.FindGameObjectsWithTag("Window"))
		{
			Destroy(go);
		}
	}

	public IEnumerator Request(string endpoint, string query, Action<string> result)
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
				InstantiateNoticePopup("ERROR", request.error);
				if (result != null)
				{
					result(request.error);
				}
				break;
			case UnityWebRequest.Result.ProtocolError:
				Debug.LogError(pages[page] + ": HTTP Error: " + request.error);
				InstantiateNoticePopup("ERROR", request.error);
				if (result != null)
				{
					result(request.error);
				}
				break;
			case UnityWebRequest.Result.Success:
				Debug.Log(pages[page] + ":\nReceived: " + request.downloadHandler.text);
				if (result != null)
				{
					try
					{
						result(request.downloadHandler.text);
					}
					catch (Exception e)
					{
						InstantiateNoticePopup("ERROR", $"{e}");
					}
				}
				break;
		}
	}

	public void Login(int citizenId)
	{
		var query = $"citizen_id={citizenId}";
		StartCoroutine(Request("citizen", query, (result) => {
			ProcessMe(result);
			Floor(GameManager.Instance.me.floor_id);
		}));
	}

	public void Exec(string command, string[] parameters)
	{
		var query = $"citizen_id={GameManager.Instance.me.id}&command={command}&parameters={string.Join(",", parameters)}";
		StartCoroutine(Request("exec", query, ProcessExec));
	}

	public void OrganizationPagesItemsPublish(int organizationId, int roomId)
	{
		var query = $"organization_id={organizationId}&room_id={roomId}";
		StartCoroutine(Request("organization-pages-items-publish", query, ProcessOrganization));
	}

	// Если price=0, то снимает с продажи
	public void ItemSell(int itemId, int price)
	{
		var query = $"item_id={itemId}&price={(price == 0 ? "" : price)}";
		StartCoroutine(Request("item-sell", query, ProcessInventory));
	}

	public void ItemSplit(int itemId, int quantity)
	{
		var query = $"item_id={itemId}&quantity={quantity}";
		StartCoroutine(Request("item-split", query, ProcessInventory));
	}

	public void ItemStack(int itemId)
	{
		var query = $"item_id={itemId}";
		StartCoroutine(Request("item-stack", query, ProcessInventory));
	}

	public void Me()
    {
		var query = $"citizen_id={GameManager.Instance.me.id}";
		StartCoroutine(Request("citizen", query, ProcessMe));
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

	public void OrganizationAttachRoom(int organizationId, int roomId)
    {
		var query = $"organization_id={organizationId}&room_id={roomId}";
		StartCoroutine(Request("organization-attach-room", query, ProcessOrganization));
	}

	public void Floor(int floorId)
    {
		var query = $"floor_id={floorId}";
		StartCoroutine(Request("floor", query, ProcessFloor));
	}

	public void MemberCreate(int organizationId, int citizenId)
    {
		var query = $"organization_id={organizationId}&citizen_id={citizenId}";
		StartCoroutine(Request("member-create", query, ProcessOrganization));
	}
	
	public void MemberDelete(int organizationId, int citizenId)
    {
		var query = $"organization_id={organizationId}&citizen_id={citizenId}";
		StartCoroutine(Request("member-delete", query, ProcessOrganization));
	}

	public void MoveIntoRoom(int roomId)
    {
		var query = $"citizen_id={GameManager.Instance.me.id}&room_id={roomId}";
		StartCoroutine(Request("move-into-room", query, ProcessMoveIntoRoom));
	}

	public void OrganizationSetProperties(int organizationId, OrganizationProperties properties)
    {
		var query = $"organization_id={organizationId}&properties={Escape(JsonUtility.ToJson(properties))}";
		StartCoroutine(Request("organization-set-properties", query, null));
	}

	public void OrganizationSetTitle(int organizationId, string title)
    {
		var query = $"organization_id={organizationId}&title={Escape(title)}";
		StartCoroutine(Request("organization-set-title", query, null));
	}

	public void Page(string address, string path)
    {
		int organizationId;
		if (!int.TryParse(address, out organizationId))
		{
			Debug.LogWarning("Текстовые адреса пока не поддерживаются");
			return;
		}

		var query = $"organization_id={organizationId}&path={path}";
		StartCoroutine(Request("page", query, ProcessPage));
	}

	public void ProcessPage(string json)
	{
		var response = JsonUtility.FromJson<PageResponse>(json);
		if (response == null)
		{
			return;
		}

		var content = response.page.content;
		if (content == "")
		{
			content = "Page not found";
		}

		GameManager.Instance.currentPage = response.page;
		GameObject.Find("ComputerWindow(Clone)")
			.GetComponentInChildren<Side.ComputerInternetTab>()
			.content.text = content;
	}

	public void PageCreate(int organizationId, string content, string path)
    {
		var query = $"organization_id={organizationId}&content={Escape(content)}&path={path}";

		StartCoroutine(Request("page-create", query, ProcessOrganization));
	}

	public void Inventory(int rootItemId)
    {
		var query = $"root_item_id={rootItemId}";
		StartCoroutine(Request("inventory", query, ProcessInventory));
	}

	public void RoomTypes()
	{
		StartCoroutine(Request("room-types", "", ProcessRoomTypes));
	}

	public void Organizations(int ownerId)
	{
		var query = $"owner_id={ownerId}";
		StartCoroutine(Request("organizations", query, ProcessOrganizations));
	}

	public void OrganizationTypes()
	{
		StartCoroutine(Request("organization-types", "", ProcessOrganizationTypes));
	}

	public void RentedRooms(int roomTypeId)
	{
		var query = $"renter_id={GameManager.Instance.me.id}&room_type_id={roomTypeId}";
		StartCoroutine(Request("rented-rooms", query, ProcessRentedRooms));
	}

	public void OrganizationDetachRoom(int organizationId, int roomId)
	{
		var query = $"organization_id={organizationId}&room_id={roomId}";
		StartCoroutine(Request("organization-detach-room", query, ProcessOrganization));
	}

	public void OrganizationCreate(int organizationTypeId)
	{
		var query = $"organization_type_id={organizationTypeId}&owner_id={GameManager.Instance.me.id}";
		StartCoroutine(Request("organization-create", query, ProcessOrganization));
	}

	public void DealCreate(int marketId, int buyerAccountId, int deliveryAddress, int sellerAccountId, int itemId, int price)
    {
		var query = $"market_id={marketId}&buyer_account_id={buyerAccountId}&delivery_address={deliveryAddress}&seller_account_id={sellerAccountId}&item_id={itemId}&price={price}";
		StartCoroutine(Request("deal-create", query, ProcessDealCreate));
	}

	public void DealAccept(int dealId, int quantity)
    {
		var query = $"deal_id={dealId}&quantity={quantity}";
		StartCoroutine(Request("deal-accept", query, ProcessDealAccept));
	}

	public void DealDecline(int dealId)
    {
		var query = $"deal_id={dealId}";
		StartCoroutine(Request("deal-decline", query, (result) => {}));
	}

	private void ProcessDealCreate(string json)
	{
		var response = JsonUtility.FromJson<DealCreateResponse>(json);
		if (response == null)
		{
			return;
		}

		if (response.error != null)
		{
			InstantiateNoticePopup("ERROR", response.error);
		}
		else
		{
			InstantiateDealPopup(response.deal);
		}

		if (response.warning != null)
		{
			InstantiateNoticePopup("WARNING", response.warning);
		}
	}

	private void InstantiateDealPopup(DealItem deal)
	{
		var dealPopup = Instantiate(dealPopupPrefab, uiCanvas.transform)
			.GetComponent<Side.DealPopup>();
		dealPopup.caption.text = "DEAL";
		dealPopup.description.text = $"Buy item?";
		dealPopup.deal = deal;
	}

	private void ProcessDealAccept(string json)
	{
		var response = JsonUtility.FromJson<DealAcceptResponse>(json);
		if (response == null)
		{
			return;
		}

		InstantiateNoticePopup("STATUS", response.status);
	}

	public void InstantiateNoticePopup(string caption, string description)
	{
		var noticePopup = Instantiate(noticePopupPrefab, uiCanvas.transform)
			.GetComponent<Side.NoticePopup>();
		noticePopup.caption.text = caption;
		noticePopup.description.text = description;

		Color borderColor = new();
		Color backgroundColor = new();
		switch (caption)
		{
			case "ERROR":
				borderColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
				backgroundColor = new Color(1.0f, 0.8f, 0.8f, 1.0f);
				break;
			case "WARNING":
				borderColor = new Color(1.0f, 0.5f, 0.0f, 1.0f);
				backgroundColor = new Color(1.0f, 0.8f, 0.1f, 1.0f);
				break;
		}
		foreach (var component in noticePopup.GetComponentsInChildren<Image>())
		{
			if (component.name == "Border")
			{
				component.color = borderColor;
			}
			else if (component.name == "Background")
			{
				component.color = backgroundColor;
			}
		}
	}

	public void InstantiateLoginPopup()
	{
		Instantiate(loginPopupPrefab, uiCanvas.transform);
	}

	public void Chat(int citizenId, int roomId, string text)
    {
		var query = $"citizen_id={citizenId}&room_id={roomId}&text={Escape(text)}";
		StartCoroutine(Request("chat", query, (result) =>
		{
			var c = JsonUtility.FromJson<ChatResponse>(result);
			GameObject.Find("Chat(Clone)").GetComponentInChildren<Side.ChatController>()
				.ReplaceChat(c.messages);
		}));
	}

	public void Tasks(List<int> organizationIds)
	{
		var value = "";
		foreach (var organizationId in organizationIds)
		{
			value += $",{organizationId}";
		}

		var query = $"organization_id={value.Substring(1)}";
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
				var instance = Instantiate(prefab, new Vector3(i+1, 0, j+1), Quaternion.identity, mapCanvas.transform);
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

		HideAllButtons();
		DestroyAll();

		foreach(var galaxy in universe.galaxies)
		{
			var x = galaxy.number * 2;

			var prefab = galaxy.systems_count == 0 ? galaxyUnknownPrefab : galaxyExploredPrefab;

			var instance = Instantiate(prefab, new Vector3(x, 0, 5), Quaternion.identity, mapCanvas.transform);
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

		HideAllButtons();
		DestroyAll();

		foreach(var system in galaxy.systems)
		{
			var x = system.number * 2;

			var prefab = system.planets_count == 0 ? systemUnknownPrefab : systemExploredPrefab;

			var instance = Instantiate(prefab, new Vector3(x, 0, 8), Quaternion.identity, mapCanvas.transform);
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

		HideAllButtons();
		DestroyAll();

		var starInstance = Instantiate(systemExploredPrefab, new Vector3(0, 4f, 0), Quaternion.identity, mapCanvas.transform);
		starInstance.transform.localScale = Vector3.one * 8;

		foreach(var planet in system.planets)
		{
			var x = -planet.number * 2 - 4;

			var prefab = planet.continents_count == 0 ? planetUnknownPrefab : planetExploredPrefab;

			var instance = Instantiate(prefab, new Vector3(x, 0, 0), Quaternion.identity, mapCanvas.transform);
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

		HideAllButtons();
		DestroyAll();

		foreach(var continent in planet.continents)
		{
			var z = -continent.number * 3 + 8;

			var continentPrefab = continent.regions_count == 0 ? continentUnknownPrefab : continentExploredPrefab;

			var continentInstance = Instantiate(continentPrefab, new Vector3(0, z, 0), Quaternion.identity, mapCanvas.transform);
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

		HideAllButtons();
		DestroyAll();

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
			
			var instance = Instantiate(prefab, new Vector3(region.x, 0, region.y), Quaternion.identity, mapCanvas.transform);
			instance.name = $"Region#{region.id} ({region.x}/{region.y})";
			instance.GetComponent<Entities.Cells.Region>().regionItem = region;
			if (region.cities.Count == 0)
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

		HideAllButtons();
		DestroyAll();

		foreach(var city in region.cities)
		{
			var prefab = city.blocks_count == 0 ? cityUnknownPrefab : cityExploredPrefab;
			var instance = Instantiate(prefab, new Vector3(city.x, 0, city.y), Quaternion.identity, mapCanvas.transform);
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

		HideAllButtons();
		DestroyAll();

		foreach(var block in city.blocks)
		{
			var prefab = block.parcels_count == 0 ? blockUnknownPrefab : blockExploredPrefab;
			var instance = Instantiate(prefab, new Vector3(block.x, 0, block.y), Quaternion.identity, mapCanvas.transform);
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

		HideAllButtons();
		DestroyAll();

		foreach(var parcel in block.parcels)
		{
			var prefab = parcel.owner_id == 0 ? parcelUnknownPrefab : parcelExploredPrefab;
			var instance = Instantiate(prefab, new Vector3(parcel.x, 0, parcel.y), Quaternion.identity, mapCanvas.transform);
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

		HideAllButtons();
		DestroyAll();

		const int Z = 8;
		var parcelMap = new FloorItem[Width, Height, Z];
		foreach(var floor in parcel.floors)
		{
			for (var i = 0; i < Width; i++)
			{
				for (var j = 0; j < Height; j++)
				{
					for (var k = 0; k < Z; k++)
					{
						if (i+1 >= floor.x
							&& i+1 < floor.x + floor.w
							&& j+1 <= floor.y
							&& j+1 > floor.y - floor.h
							&& k == floor.z)
						{
							parcelMap[i, j, k] = floor;
						}
					}
				}
			}
		}

		for (var i = 0; i < Width; i++)
		{
			for (var j = 0; j < Height; j++)
			{
				for (var k = 0; k < Z; k++)
				{
					var floor = parcelMap[i, j, k];
					if (floor != null)
					{
						var instance = Instantiate(floorPrefab, new Vector3(i+1, k, j+1), Quaternion.identity, mapCanvas.transform);
						instance.name = $"Floor#{floor.id} ({floor.x}/{floor.y}/{floor.z})";
						instance.GetComponent<Entities.Cells.Floor>().floorItem = floor;
					}
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

		var floor = response.floor;
		GameManager.Instance.currentFloor = floor;
		GameManager.Instance.state = GameManager.Floor;

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
						new Vector3(i+1, 0, j+1), Quaternion.identity, mapCanvas.transform);
					var room = instance.GetComponent<Entities.Cells.Room>();
					room.GetComponentInChildren<Renderer>().material.color = new Color(r.r, r.g, r.b, r.a);
					room.roomItem = r;
				}
				else
				{
					var instance = Instantiate(openRoomPrefab,
						new Vector3(i+1, 0, j+1), Quaternion.identity, mapCanvas.transform);
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

			GameObject.Find("Chat(Clone)").GetComponentInChildren<Side.ChatController>().ReplaceChat(r.messages);

			for (var i = 0; i < r.citizens.Count; i++)
			{
				var citizenPrefabInstance = Instantiate(citizenPrefab, new Vector3(i+3, 0, 11), Quaternion.identity, mapCanvas.transform);
				var citizen = citizenPrefabInstance.GetComponent<Entities.Citizen>();

				if (r.citizens[i].id == GameManager.Instance.me.id)
				{
					citizen.GetComponentInChildren<Renderer>().material.color = Color.green;
					citizen.citizenItem = GameManager.Instance.me;
				}
				else
				{
					citizen.citizenItem = r.citizens[i];
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

		DestroyItems();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.lots.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var lot = InstantiateObject<Lot>(lotPrefab, uiCanvas,
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

		DestroyItems();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.organization_types.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var organizationType = InstantiateObject<OrganizationType>(organizationTypePrefab, uiCanvas,
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

		DestroyItems();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.organizations.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			List<RequiredRoomTypeItem> requiredRoomTypes = new();
			foreach(var attachedRoom in response.organizations[i].attached_rooms)
			{
				foreach(var roomType in response.organizations[i].type.requirements.room_types)
				{
					if (roomType.id == attachedRoom.type_id)
					{
						RequiredRoomTypeItem requiredRoomType = new();
						requiredRoomType.is_attached = true;
						requiredRoomTypes.Add(requiredRoomType);
						break;
					}
				}
			}

			var requiredRoomTypeAttachedCount = requiredRoomTypes.Count(x => x.is_attached);

			var isAttached = requiredRoomTypeAttachedCount == response.organizations[i].type.requirements.room_types.Count;

			var color = Color.red;
			if (isAttached)
			{
				color = Color.green;
			}

			var organization = InstantiateObject<Organization>(organizationPrefab, uiCanvas,
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

		DestroyItems();

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

			var rentedRoom = InstantiateObject<RentedRoom>(rentedRoomPrefab, uiCanvas,
				$"{rentedRooms[i].title}", 100, 700, col, row);
			rentedRoom.rentedRoomItem = rentedRooms[i];

            row++;
        }
	}

	public void InstantiateRequiredRoomTypes(OrganizationItem organizationItem)
	{
		List<RequiredRoomTypeItem> requiredRoomTypes = new();
		foreach(var roomType in organizationItem.type.requirements.room_types)
		{
			RequiredRoomTypeItem requiredRoomType = new();
			requiredRoomType.title = roomType.title;
			requiredRoomType.room_type_id = roomType.id;
			requiredRoomType.w = roomType.w;
			requiredRoomType.h = roomType.h;
			requiredRoomType.organization_id = organizationItem.id;
			foreach (var attachedRoom in organizationItem.attached_rooms.Where(x => x.type_id == roomType.id))
			{
				requiredRoomType.attached_room = attachedRoom;
			}
			requiredRoomTypes.Add(requiredRoomType);
		}
		
		DestroyItems();

        var col = 0;
        var row = 0;
		for (var i = 0; i < requiredRoomTypes.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var requiredRoomType = InstantiateObject<RequiredRoomType>(requiredRoomTypePrefab, uiCanvas,
				$"{requiredRoomTypes[i].title} ({requiredRoomTypes[i].attached_room != null})", 600, 400, col, row);
			requiredRoomType.requiredRoomTypeItem = requiredRoomTypes[i];
			requiredRoomType.GetComponent<Image>().color = requiredRoomTypes[i].attached_room != null ? Color.green : Color.red;;

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

		InstantiateOrganization(response.organization);
	}

	public void InstantiateOrganization(OrganizationItem organization, string tabName = "Main")
	{
		DestroyWindows();
		GameManager.Instance.currentOrganization = organization;
		Instantiate(organizationWindowPrefab, uiCanvas.transform)
			.GetComponent<Side.WindowManager>().SwitchTab(tabName);
	}

	public void InstantiateCitizen(CitizenItem citizen, string tabName = "Main")
	{
		GameManager.Instance.currentCitizen = citizen;
		DestroyWindows();
		Instantiate(citizenWindowPrefab, uiCanvas.transform)
			.GetComponent<Side.WindowManager>().SwitchTab(tabName);
	}

	public void InstantiateComputer(string tabName = "Main")
	{
		DestroyWindows();
		Instantiate(computerWindowPrefab, uiCanvas.transform)
			.GetComponent<Side.WindowManager>().SwitchTab(tabName);
	}

	private void ProcessExec(string json)
	{
		var response = JsonUtility.FromJson<ExecResponse>(json);
		if (response == null)
		{
			return;
		}

		GameObject.Find("ComputerWindow(Clone)").GetComponentInChildren<Side.ComputerInternetTab>().content.text = response.result;
	}

	private void ProcessCitizen(string json)
	{
		var response = JsonUtility.FromJson<CitizenResponse>(json);
        if (response == null)
        {
            return;
        }

		GameManager.Instance.currentCitizen = response.citizen;
	}

	private void ProcessMe(string json)
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

		GameManager.Instance.currentItem = response.item;
		GameManager.Instance.state = GameManager.Inventory;
        
		InstantiateInventory(response.children);
	}

	public void InstantiateInventory(List<ItemItem> items)
	{
		HideAllButtons();
		DestroyItems();

        var col = 0;
        var row = 0;
		for (var i = 0; i < items.Count; i++)
        {
            if (i % 4 == 0)
            {
                col++;
                row = 0;
            }

            var item = InstantiateObject<Entities.Items.Item>(itemPrefab, uiCanvas,
				$"{items[i].type_title} {items[i].quantity}", 380, 700, col, row);
			item.GetComponent<Image>().color = items[i].price == 0 ? Color.white : Color.blue;
			item.itemItem = items[i];

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

	public T InstantiateObject<T>(GameObject prefab, GameObject parent, string title, int x, int y, int col, int row) where T : IItem
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

		DestroyItems();

        var col = 0;
        var row = 0;
		for (var i = 0; i < response.tasks.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

			var task = InstantiateObject<Entities.Items.Task>(taskPrefab, uiCanvas,
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

			var position = InstantiateObject<Position>(positionPrefab, uiCanvas,
				$"{response.positions[i].title} ({(response.positions[i].is_free ? "free" : "busy")})", 500, 700, col, row);
			position.positionItem = response.positions[i];

            row++;
        }
	}

	public void InstantiateRoomTypes(List<RoomTypeItem> roomTypes)
	{
		DestroyItems();

        var col = 0;
        var row = 0;
		for (var i = 0; i < roomTypes.Count; i++)
        {
            if (i % 5 == 0)
            {
                col++;
                row = 0;
            }

            var roomType = NetworkManager.Instance.InstantiateObject<RoomType>(roomTypePrefab, uiCanvas,
				roomTypes[i].title,
				100, 700, col, row);
			roomType.roomTypeItem = roomTypes[i];

            row++;
        }
	}

	private string Escape(string text)
	{
		text = text.Replace("'", "''");
		text = UnityWebRequest.EscapeURL(text);
		return text;
	}
}
