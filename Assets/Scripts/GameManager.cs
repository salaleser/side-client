using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Models;
using TMPro;

public class GameManager : MonoBehaviour
{
	public const int Organization = -3;
	public const int Universe = 1;
	public const int Galaxy = 2;
	public const int System = 3;
	public const int Planet = 4;
	public const int Continent = 5;
	public const int Region = 6;
	public const int City = 7;
	public const int Block = 8;
	public const int Parcel = 9;
	public const int Room = 11;

	public int state;
	
	public UniverseItem currentUniverse;
	public GalaxyItem currentGalaxy;
	public SystemItem currentSystem;
	public PlanetItem currentPlanet;
	public ContinentItem currentContinent;
	public RegionItem currentRegion;
	public CityItem currentCity;
	public BlockItem currentBlock;
	public ParcelItem currentParcel;
    public OrganizationItem currentOrganization;
	public CitizenItem me = new();
    public PageItem currentPage;
    
	public GameObject CursorPrefab;
	public GameObject Cursor { get; private set; }

	public GameObject DescriptionPrefab;
	public GameObject Description { get; private set; }

	public GameObject MiniMapPrefab;
	public static GameObject MiniMap { get; private set; }

	public GameObject ChatPrefab;
	public static GameObject Chat { get; private set; }

	public GameObject ShortcutsActiveSign;
	public static bool QuickMenuActive { get; private set; }
	public static bool WindowActive { get; private set; }
	public static bool PopupActive { get; private set; }
	public static bool ShortcutsActive { get; private set; }

	public static GameManager Instance { get; private set; }

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
		Description = Instantiate(DescriptionPrefab, NetworkManager.Instance.UiCanvas.transform);
		MiniMap = Instantiate(MiniMapPrefab, NetworkManager.Instance.UiCanvas.transform);
		Chat = Instantiate(ChatPrefab, NetworkManager.Instance.UiCanvas.transform);
		Cursor = Instantiate(CursorPrefab, NetworkManager.Instance.UiCanvas.transform);
		SetShortcutsActive(true);
	}

	public static void SetShortcutsActive(bool isActive)
	{
		ShortcutsActive = isActive;
		Instance.ShortcutsActiveSign.GetComponent<Image>().color = isActive ? Color.green : Color.red;
		Instance.ShortcutsActiveSign.GetComponentInChildren<TMP_Text>().text = isActive ? "SHORCUTS ENABLED" : "SHORCUTS DISABLED";
	}

	public static void SetDescription(string text)
	{
		Instance.Description.GetComponentInChildren<TMP_Text>().text = text;
	}

	public static void SetQuickMenuActive(bool isActive)
	{
		QuickMenuActive = isActive;
	}

	public static void SetWindowActive(bool isActive)
	{
		WindowActive = isActive;
		Chat.SetActive(!isActive);
	}

	public static void SetPopupActive(bool isActive)
	{
		PopupActive = isActive;
		Chat.SetActive(!isActive);
	}
}
