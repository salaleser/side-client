using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Entities.Items;
using Models;
using WebSocketSharp;

public class NetworkManager : Manager
{
	public const int Width = 8;
	public const int Height = 8;
	public const int Depth = 8;

	private string Host;
	private string Port;

	public GameObject mainButtonsPanelPrefab;
	public GameObject quickMenuPrefab;
	public GameObject dealPopupPrefab;
	public GameObject InputFieldPopupPrefab;
	public GameObject InvitePopupPrefab;
	public GameObject OfferPopupPrefab;
	public GameObject PollPopupPrefab;
	public GameObject EnterPasswordPopupPrefab;
	public GameObject createRoomPopupPrefab;
	public GameObject noticePopupPrefab;
	public GameObject loginPopupPrefab;
	public GameObject organizationWindowPrefab;
	public GameObject citizenWindowPrefab;
	public GameObject computerWindowPrefab;

	public GameObject UiCanvas;
	public GameObject GroundCanvas;
	public GameObject RoomCanvas;
	public GameObject CitizenCanvas;

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
	public GameObject RoomGroundPrefab;

	public GameObject openRoomPrefab;
	public GameObject closedRoomPrefab;

	public GameObject CitizenPrefab;

	private WebSocket _ws;

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
				dir = "/StreamingAssets";
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
		// Instantiate(mainButtonsPanelPrefab, UiCanvas.transform);
		Instantiate(loginPopupPrefab, UiCanvas.transform);
		StartCoroutine(ConnectChat());
	}

	private void Update()
	{
		if (GameManager.ShortcutsActive && !GameManager.WindowActive && !GameManager.PopupActive)
		{
			if (Keyboard.current.mKey.wasPressedThisFrame)
			{
				ShowMapButton();
			}
			else if (Keyboard.current.oKey.wasPressedThisFrame)
			{
				OrganizationsButton();
			}
			else if (Keyboard.current.nKey.wasPressedThisFrame)
			{
				InternetButton();
			}
			else if (Keyboard.current.iKey.wasPressedThisFrame)
			{
				ItemsButton();
			}
			else if (Keyboard.current.rKey.wasPressedThisFrame)
			{
				ReloadButton();
			}
			else if (Keyboard.current.equalsKey.wasPressedThisFrame)
			{
				CenterMeButton();
			}
			else if (Keyboard.current.minusKey.wasPressedThisFrame)
			{
				ZoomOutButton();
			}
			else if (Keyboard.current.tabKey.wasPressedThisFrame)
			{
				ProfileButton();
			}
			else if (Keyboard.current.cKey.wasPressedThisFrame)
			{
				ComputerButton();
			}
			else if (Keyboard.current.escapeKey.wasPressedThisFrame)
			{
				CenterCameraButton();
			}
		}
	}

	public void ProfileButton()
	{
		InstantiateCitizen();
	}

	public void CenterCameraButton()
	{
		switch (GameManager.Instance.state)
		{
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
				Camera.main.transform.localPosition = Vector3.zero;
				Camera.main.transform.localRotation = Quaternion.Euler(20, 45, 0);
				break;
			case 9:
				Camera.main.transform.localPosition = new Vector3(GameManager.Instance.me.x, GameManager.Instance.me.z, GameManager.Instance.me.y);
				Camera.main.transform.localRotation = Quaternion.Euler(20, 45, 0);
				break;
		}
	}

	public void ItemsButton()
	{
		InstantiateCitizen("Items");
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
		}
	}

	public void ShowMapButton()
	{
		Universe();
	}

	public void OrganizationsButton()
	{
		InstantiateCitizen("Organizations");
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
		}
	}

	public void CenterMeButton()
	{
		Parcel(GameManager.Instance.me.parcel_id);
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
		DestroyAll<Entities.Cells.Ground>();
		DestroyAll<Entities.Cells.Room>();
		
		DestroyAll<Entities.Citizen>();
	}

	private void DestroyAll()
	{
		DestroyCells();
	}

	private void DestroyAll<T>() where T : Entity
	{
		var objects = GameObject.FindGameObjectsWithTag("Side");
		foreach(var o in objects)
		{
			foreach(var bo in o.GetComponentsInChildren<T>())
			{
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

	public IEnumerator ConnectChat()
	{
		_ws = new WebSocket($"ws://{Host}:65279");

		_ws.OnMessage += (sender, ev) => {
			try {
				var data = ev.Data.Split("◊");
				var chat = new Chat();
				chat.CitizenId = int.Parse(data[0]);
				chat.RoomId = int.Parse(data[1]);
				chat.Text = data[2];
				chat.CreatedAt = data[3];

				GameManager.Instance.ChatMessages.Enqueue(chat.ToString());
			}
			catch (Exception e)
			{
				Debug.LogError($"OnMessage: {e}");
			}
		};

		_ws.Connect();

		while (true)
		{
			yield return null;
      	}
	}

	public IEnumerator Request(string endpoint, string[] args, Action<string> result)
	{
		var url = $"http://{Host}:{Port}/{endpoint}?args={string.Join("◊", args)}";
		Debug.Log(url);

		var pages = url.Split('/');
		var page = pages.Length - 1;

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
				InstantiateNoticePopup("ERROR", $"Error: {request.error}");
				break;
			case UnityWebRequest.Result.ProtocolError:
				Debug.LogError($"{pages[page]} {request.downloadHandler.text}");
				InstantiateNoticePopup("ERROR", $"{pages[page]} {request.downloadHandler.text}");
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
						InstantiateNoticePopup("ERROR", $"{request.downloadHandler.text}\n\n{e}");
						throw;
					}
				}
				break;
		}
	}

	public void Login(int citizenId)
	{
		var args = new string[]{citizenId.ToString()};
		StartCoroutine(Request("citizen", args, (result) => {
			ProcessMe(result);
			CenterMeButton();
		}));
	}

	public void Exec(string command, string[] parameters)
	{
		var args = new string[]{GameManager.Instance.me.id.ToString(), command, string.Join(",", parameters)};
		StartCoroutine(Request("exec", args, ProcessExec));
	}

	public void OrganizationPagesItemsPublish(int organizationId, int roomId)
	{
		var args = new string[]{organizationId.ToString(), roomId.ToString()};
		StartCoroutine(Request("organization-pages-items-publish", args, (json) => ProcessOrganization(json, "Pages")));
	}

	public void Me()
    {
		var args = new string[]{GameManager.Instance.me.id.ToString()};
		StartCoroutine(Request("citizen", args, ProcessMe));
	}

	public void Universe()
    {
		var args = new string[]{};
		StartCoroutine(Request("universe", args, ProcessUniverse));
	}

	public void Galaxy(int galaxyId)
    {
		var args = new string[]{galaxyId.ToString()};
		StartCoroutine(Request("galaxy", args, ProcessGalaxy));
	}

	public void GalaxyExplore(int galaxyId)
    {
		var args = new string[]{galaxyId.ToString(), GameManager.Instance.me.id.ToString()};
		StartCoroutine(Request("galaxy-explore", args, ProcessUniverse));
	}

	public void System(int systemId)
    {
		var args = new string[]{systemId.ToString()};
		StartCoroutine(Request("system", args, ProcessSystem));
	}

	public void SystemExplore(int systemId)
    {
		var args = new string[]{systemId.ToString(), GameManager.Instance.me.id.ToString()};
		StartCoroutine(Request("system-explore", args, ProcessGalaxy));
	}

	public void Planet(int planetId)
    {
		var args = new string[]{planetId.ToString()};
		StartCoroutine(Request("planet", args, ProcessPlanet));
	}

	public void PlanetExplore(int planetId)
    {
		var args = new string[]{planetId.ToString(), GameManager.Instance.me.id.ToString()};
		StartCoroutine(Request("planet-explore", args, ProcessSystem));
	}

	public void Continent(int continentId)
    {
		var args = new string[]{continentId.ToString()};
		StartCoroutine(Request("continent", args, ProcessContinent));
	}

	public void ContinentExplore(int continentId)
    {
		var args = new string[]{continentId.ToString(), GameManager.Instance.me.id.ToString()};
		StartCoroutine(Request("continent-explore", args, ProcessPlanet));
	}

	public void Region(int regionId)
    {
		var args = new string[]{regionId.ToString()};
		StartCoroutine(Request("region", args, ProcessRegion));
	}

	public void RegionExplore(int regionId)
    {
		var args = new string[]{regionId.ToString(), GameManager.Instance.me.id.ToString()};
		StartCoroutine(Request("region-explore", args, ProcessContinent));
	}

	public void City(int cityId)
    {
		var args = new string[]{cityId.ToString()};
		StartCoroutine(Request("city", args, ProcessCity));
	}

	public void Block(int blockId)
    {
		var args = new string[]{blockId.ToString()};
		StartCoroutine(Request("block", args, ProcessBlock));
	}

	public void Parcel(int parcelId)
    {
		var args = new string[]{parcelId.ToString()};
		StartCoroutine(Request("parcel", args, ProcessParcel));
	}

	public void CityExplore(int cityId)
    {
		var args = new string[]{cityId.ToString(), GameManager.Instance.me.id.ToString()};
		StartCoroutine(Request("city-explore", args, ProcessRegion));
	}

	public void BlockExplore(int blockId)
    {
		var args = new string[]{blockId.ToString(), GameManager.Instance.me.id.ToString()};
		StartCoroutine(Request("block-explore", args, ProcessCity));
	}

	public void CreateRoom(int parcelId, int typeId, int x, int y, int z, int w, int h, int constructionOrganizationId, int creatorId, string title)
    {
		var args = new string[]{parcelId.ToString(), typeId.ToString(), x.ToString(), y.ToString(), z.ToString(), w.ToString(), h.ToString(), constructionOrganizationId.ToString(), creatorId.ToString(), title};
		StartCoroutine(Request("room-create", args, ProcessParcel));
	}

	public void Organization(int organizationId, string tabName = "Main")
    {
		var args = new string[]{organizationId.ToString()};
		StartCoroutine(Request("organization", args, (json) => ProcessOrganization(json, tabName)));
	}

	public void Citizen(int citizenId, string tabName = "Main")
    {
		var args = new string[]{citizenId.ToString()};
		StartCoroutine(Request("citizen", args, (json) => ProcessCitizen(json, tabName)));
	}
	
	public void MemberDelete(int organizationId, int citizenId)
    {
		var args = new string[]{organizationId.ToString(), citizenId.ToString()};
		StartCoroutine(Request("member-delete", args, (json) => ProcessOrganization(json, "Members")));
	}

	public void CitizenMove(int citizenId, int parcelId, int x, int y, int z)
    {
		var args = new string[]{citizenId.ToString(), parcelId.ToString(), x.ToString(), y.ToString(), z.ToString()};
		StartCoroutine(Request("citizen-move", args, ProcessCitizenMove));
	}

	public void CitizenEnterRoom(int citizenId, int roomId, string password)
    {
		var args = new string[]{citizenId.ToString(), roomId.ToString(), password};
		StartCoroutine(Request("citizen-enter-room", args, ProcessCitizenMove));
	}

	public void OrganizationSetProperties(int organizationId, OrganizationProperties properties, string tabName = "Main")
    {
		var args = new string[]{organizationId.ToString(), Escape(JsonUtility.ToJson(properties))};
		StartCoroutine(Request("organization-set-properties", args, (json) => ProcessOrganization(json, tabName)));
	}

	public void OrganizationSetTitle(int organizationId, string title)
    {
		var args = new string[]{organizationId.ToString(), Escape(title)};
		StartCoroutine(Request("organization-set-title", args, null));
	}

	public void PageCreate(int organizationId, string content, string path)
    {
		var args = new string[]{organizationId.ToString(), Escape(content), path};
		StartCoroutine(Request("page-create", args, (json) => ProcessOrganization(json, "Pages")));
	}

	public void DealCreate(int marketId, int buyerAccountId, int deliveryAddress, int sellerAccountId, int itemId, int price)
    {
		var args = new string[]{marketId.ToString(), buyerAccountId.ToString(), deliveryAddress.ToString(), sellerAccountId.ToString(), itemId.ToString(), price.ToString()};
		StartCoroutine(Request("deal-create", args, ProcessDealCreate));
	}

	public void DealAccept(int dealId, int quantity)
    {
		var args = new string[]{dealId.ToString(), quantity.ToString()};
		StartCoroutine(Request("deal-accept", args, ProcessDealAccept));
	}

	public void DealDecline(int dealId)
    {
		var args = new string[]{dealId.ToString()};
		StartCoroutine(Request("deal-decline", args, null));
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

	public void InstantiateCreateRoomPopup(int z)
	{
		Instantiate(createRoomPopupPrefab, UiCanvas.transform)
			.GetComponent<Side.CreateRoomPopup>().Z.text = z.ToString();
	}

	public void InstantiateEnterPasswordPopup(RoomItem room)
	{
		Instantiate(EnterPasswordPopupPrefab, UiCanvas.transform)
			.GetComponent<Side.EnterPasswordPopup>().SetRoom(room);
	}

	public void InstantiateInputFieldPopup(string description, UnityAction<string> action)
	{
		var popup = Instantiate(InputFieldPopupPrefab, UiCanvas.transform)
			.GetComponent<Side.InputFieldPopup>();
		popup.Description.text = description;
		popup.Action = action;
	}

	private void InstantiateDealPopup(DealItem deal)
	{
		Instantiate(dealPopupPrefab, UiCanvas.transform)
			.GetComponent<Side.DealPopup>().Deal = deal;
	}

	public void InviteCreate(int inviterId, int organizationId, int citizenId)
    {
		var args = new string[]{inviterId.ToString(), organizationId.ToString(), citizenId.ToString()};
		StartCoroutine(Request("invite-create", args, null));
	}

	public void InviteAccept(int inviteId)
    {
		var args = new string[]{inviteId.ToString()};
		StartCoroutine(Request("invite-accept", args, ProcessInviteAccept));
	}

	public void InviteDecline(int inviteId)
    {
		var args = new string[]{inviteId.ToString()};
		StartCoroutine(Request("invite-decline", args, null));
	}

	public void InstantiateInvitePopup(InviteItem invite)
	{
		Instantiate(InvitePopupPrefab, UiCanvas.transform)
			.GetComponent<Side.InvitePopup>().Invite = invite;
	}

	public void OfferCreate(int offererId, int positionId, int citizenId)
    {
		var args = new string[]{offererId.ToString(), positionId.ToString(), citizenId.ToString()};
		StartCoroutine(Request("offer-create", args, null));
	}

	public void OfferAccept(int offerId)
    {
		var args = new string[]{offerId.ToString()};
		StartCoroutine(Request("offer-accept", args, (json) => {
			InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
		}));
	}

	public void OfferDecline(int offerId)
    {
		var args = new string[]{offerId.ToString()};
		StartCoroutine(Request("offer-decline", args, null));
	}

	public void Vote(int pollId, bool vote)
    {
		var args = new string[]{GameManager.Instance.me.id.ToString(), pollId.ToString(), vote.ToString()};
		StartCoroutine(Request("vote-create", args, null));
	}

	public void InstantiateOfferPopup(OfferItem offer)
	{
		Instantiate(OfferPopupPrefab, UiCanvas.transform)
			.GetComponent<Side.OfferPopup>().Offer = offer;
	}

	public void InstantiatePollPopup(PollItem poll)
	{
		Instantiate(PollPopupPrefab, UiCanvas.transform)
			.GetComponent<Side.PollPopup>().Poll = poll;
	}

	public void ProcessInviteAccept(string json)
	{
		InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
	}

	private void ProcessDealAccept(string json)
	{
		InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
	}

	public void InstantiateNoticePopup(string caption, string description)
	{
		var noticePopup = Instantiate(noticePopupPrefab, UiCanvas.transform)
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
			default:
				borderColor = new Color(0.0f, 0.0f, 1.0f, 1.0f);
				backgroundColor = new Color(0.8f, 0.8f, 0.1f, 1.0f);
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

	public void InstantiateQuickMenu(Transform transform)
	{
		var quickMenuController = Instantiate(quickMenuPrefab, UiCanvas.transform)
			.GetComponent<Side.QuickMenuController>();
		quickMenuController.Entity = transform.GetComponent<Entity>();
		quickMenuController.UpdateButtons();
	}

	public void Chat(int citizenId, int roomId, string text)
    {
		var args = new string[]{citizenId.ToString(), roomId.ToString(), Escape(text)};
		StartCoroutine(Request("chat", args, null));
	}

	public void Tasks(List<int> organizationIds)
	{
		if (organizationIds.Count == 0)
		{
			return;
		}

		var value = "";
		foreach (var organizationId in organizationIds)
		{
			value += $",{organizationId}";
		}

		var args = new string[]{value.Substring(1)};
		StartCoroutine(Request("tasks", args, (result) => InstantiateOrganization("Tasks")));
	}

	public void TaskAccept(int citizenId, int taskId)
	{
		var args = new string[]{citizenId.ToString(), taskId.ToString()};
		StartCoroutine(Request("task-accept", args, (result) => {}));
	}

	private void ProcessCitizenMove(string json)
	{
		GameManager.Instance.me = JsonUtility.FromJson<CitizenMoveResponse>(json).citizen;
		ProcessParcel(json);
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

		DestroyAll();
		foreach(var galaxy in universe.galaxies)
		{
			var x = galaxy.number * 2;

			var prefab = galaxy.systems_count == 0 ? galaxyUnknownPrefab : galaxyExploredPrefab;

			var instance = Instantiate(prefab, new Vector3(x, 0, 5), Quaternion.identity, GroundCanvas.transform);
			instance.name = $"Galaxy#{galaxy.id} ({galaxy.number})";
			instance.GetComponent<Entities.Cells.Galaxy>().galaxyItem = galaxy;
			CenterCameraButton();
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

		DestroyAll();
		foreach(var system in galaxy.systems)
		{
			var x = system.number * 2;

			var prefab = system.planets_count == 0 ? systemUnknownPrefab : systemExploredPrefab;

			var instance = Instantiate(prefab, new Vector3(x, 0, 8), Quaternion.identity, GroundCanvas.transform);
			instance.name = $"System#{system.id} ({system.number})";
			instance.GetComponent<Entities.Cells.System>().systemItem = system;
			CenterCameraButton();
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

		DestroyAll();
		var starInstance = Instantiate(systemExploredPrefab, new Vector3(0, 4f, 0), Quaternion.identity, GroundCanvas.transform);
		starInstance.transform.localScale = Vector3.one * 8;
		foreach(var planet in system.planets)
		{
			var x = -planet.number * 2 - 4;

			var prefab = planet.continents_count == 0 ? planetUnknownPrefab : planetExploredPrefab;

			var instance = Instantiate(prefab, new Vector3(x, 0, 0), Quaternion.identity, GroundCanvas.transform);
			instance.name = $"Planet#{planet.id} ({planet.number})";
			instance.GetComponent<Entities.Cells.Planet>().planetItem = new Models.PlanetItem(planet);
		}
		CenterCameraButton();
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

		DestroyAll();
		foreach(var continent in planet.continents)
		{
			var z = -continent.number * 3 + 8;

			var continentPrefab = continent.regions_count == 0 ? continentUnknownPrefab : continentExploredPrefab;

			var continentInstance = Instantiate(continentPrefab, new Vector3(0, z, 0), Quaternion.identity, GroundCanvas.transform);
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
			CenterCameraButton();
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
			
			var instance = Instantiate(prefab, new Vector3(region.x, 0, region.y), Quaternion.identity, GroundCanvas.transform);
			instance.name = $"Region#{region.id} ({region.x}/{region.y})";
			instance.GetComponent<Entities.Cells.Region>().regionItem = region;
			if (region.cities.Count == 0)
			{
				var color = region.z > 0 ? new Color(0.5f, 1.0f, 0.5f) : new Color(0.0f, 0.5f, 1.0f);
				instance.GetComponentInChildren<Renderer>().material.color = color;
			}
			CenterCameraButton();
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

		DestroyAll();
		foreach(var city in region.cities)
		{
			var prefab = city.blocks_count == 0 ? cityUnknownPrefab : cityExploredPrefab;
			var instance = Instantiate(prefab, new Vector3(city.x, 0, city.y), Quaternion.identity, GroundCanvas.transform);
			instance.name = $"City#{city.id} ({city.x}/{city.y})";
			instance.GetComponent<Entities.Cells.City>().cityItem = city;
		}
		CenterCameraButton();
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

		DestroyAll();
		foreach(var block in city.blocks)
		{
			var prefab = block.parcels_count == 0 ? blockUnknownPrefab : blockExploredPrefab;
			var instance = Instantiate(prefab, new Vector3(block.x, 0, block.y), Quaternion.identity, GroundCanvas.transform);
			instance.name = $"Block#{block.id} ({block.x}/{block.y})";
			instance.GetComponent<Entities.Cells.Block>().blockItem = block;
		}
		CenterCameraButton();
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

		DestroyAll();
		foreach(var parcel in block.parcels)
		{
			var prefab = parcel.rooms_count == 0 ? parcelUnknownPrefab : parcelExploredPrefab;
			var instance = Instantiate(prefab, new Vector3(parcel.x, 0, parcel.y), Quaternion.identity, GroundCanvas.transform);
			instance.name = $"Parcel#{parcel.id} ({parcel.x}/{parcel.y})";
			instance.GetComponent<Entities.Cells.Parcel>().parcelItem = parcel;
		}
		CenterCameraButton();
	}

	public void ProcessParcel(string json)
	{
		var parcel = JsonUtility.FromJson<ParcelResponse>(json).parcel;
		GameManager.Instance.currentParcel = parcel;
		GameManager.Instance.state = GameManager.Parcel;

		InstantiateParcel(parcel);
		CenterCameraButton();
	}

	public void InstantiateParcel(ParcelItem parcel)
	{
		var width = Width * Width;
		var height = Height * Height;
		var depth = Depth * Depth;
		var layers = 3;
		var parcelMap = new Models.Item[width, height, depth, layers];
		for (var i = 0; i < width; i++)
		{
			for (var j = 0; j < height; j++)
			{
				for (var k = 0; k < depth; k++)
				{
					if (k == 31)
					{
						var ground = new GroundItem();
						ground.parcel_id = parcel.id;
						ground.x = i+1;
						ground.y = j+1;
						ground.z = k+1 - depth / 2;
						ground.id = i * width + j * height + k * depth;
						ground.title = $"{ground.x},{ground.y},{ground.z}";
						parcelMap[i, j, k, 0] = ground;
					}

					foreach(var room in parcel.rooms)
					{
						if (i+1 >= room.x && i+1 < room.x + room.w
							&& j+1 <= room.y && j+1 > room.y - room.h
							&& k == room.z)
						{
							parcelMap[i, j, k, 1] = room;
						}
					}

					foreach(var citizen in parcel.citizens)
					{
						if (i+1 == citizen.x
							&& j+1 == citizen.y
							&& k == citizen.z)
						{
							parcelMap[i, j, k, 2] = citizen;
						}
					}
				}
			}
		}

		DestroyAll();
		for (var i = 0; i < width; i++)
		{
			for (var j = 0; j < height; j++)
			{
				for (var k = 0; k < depth; k++)
				{
					for (var l = 0; l < layers; l++)
					{
						switch (parcelMap[i, j, k, l])
						{
							case GroundItem ground:
								InstantiateGround(ground, new Vector3(i+1, k+1 - depth / 2, j+1));
								break;
							case RoomItem room:
								InstantiateRoom(room, new Vector3(i+1, k+1, j+1));
								break;
							case CitizenItem citizen:
								InstantiateCitizen(citizen, new Vector3(i+1, k+1, j+1));
								break;
							case null:
								break;
							default:
								Debug.LogError("entity type unknown");
								break;
						}
					}
				}
			}
		}
	}

	private void InstantiateGround(GroundItem ground, Vector3 pos)
	{
		var prefab = RoomGroundPrefab;
		var instance = Instantiate(prefab, pos, Quaternion.identity, GroundCanvas.transform);
		instance.name = $"Ground#{ground.id} ({ground.x}/{ground.y}/{ground.z})";
		var component = instance.GetComponent<Entities.Cells.Ground>();
		component.GetComponentInChildren<Renderer>().material.color = new Color(UnityEngine.Random.Range(0.4f, 0.6f), UnityEngine.Random.Range(0.8f, 1.0f), UnityEngine.Random.Range(0.4f, 0.6f));
		component.groundItem = ground;
	}

	private void InstantiateRoom(RoomItem room, Vector3 pos)
	{
		var prefab = room.id == GameManager.Instance.me.room.id ? openRoomPrefab : closedRoomPrefab;
		var instance = Instantiate(prefab, pos, Quaternion.identity, RoomCanvas.transform);
		instance.name = $"Room#{room.id} ({room.x}/{room.y}/{room.z})";
		var component = instance.GetComponent<Entities.Cells.Room>();
		component.GetComponentInChildren<Renderer>().material.color = room.type.properties.color;
		component.roomItem = room;
	}

	private void InstantiateCitizen(CitizenItem citizen, Vector3 pos)
	{
		var prefab = CitizenPrefab;
		var instance = Instantiate(prefab, pos, Quaternion.identity, CitizenCanvas.transform);
		instance.name = $"Citizen#{citizen.id} ({citizen.x}/{citizen.y}/{citizen.z})";
		var component = instance.GetComponent<Entities.Citizen>();
		component.citizenItem = citizen;
	}

	public void ProcessOrganization(string json, string tabName = "Main")
	{
		var response = JsonUtility.FromJson<OrganizationResponse>(json);
        if (response == null)
        {
            return;
        }

		GameManager.Instance.currentOrganization = response.organization;
		InstantiateOrganization(tabName);
	}

	public void ProcessCitizen(string json, string tabName = "Main")
	{
		var response = JsonUtility.FromJson<CitizenResponse>(json);
        if (response == null)
        {
            return;
        }

		GameManager.Instance.me = response.citizen;
		InstantiateCitizen("Items");
	}

	public void InstantiateOrganization(string tabName = "Main")
	{
		DestroyWindows();
		Instantiate(organizationWindowPrefab, UiCanvas.transform)
			.GetComponent<Side.WindowManager>().SwitchTab(tabName);
	}

	public void InstantiateCitizen(string tabName = "Main")
	{
		DestroyWindows();
		Instantiate(citizenWindowPrefab, UiCanvas.transform)
			.GetComponent<Side.WindowManager>().SwitchTab(tabName);
	}

	public void InstantiateComputer(string tabName = "Main")
	{
		DestroyWindows();
		Instantiate(computerWindowPrefab, UiCanvas.transform)
			.GetComponent<Side.WindowManager>().SwitchTab(tabName);
	}

	private void ProcessExec(string json)
	{
		var response = JsonUtility.FromJson<ExecResponse>(json);
		if (response == null)
		{
			return;
		}

		GameObject.Find("ComputerWindow(Clone)")
			.GetComponentInChildren<Side.ComputerInternetTab>().Content.text = response.result;
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

	public string Escape(string text)
	{
		text = text.Replace("'", "''");
		text = UnityWebRequest.EscapeURL(text);
		return text;
	}
}
