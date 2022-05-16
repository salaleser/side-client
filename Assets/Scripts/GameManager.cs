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
	public const int Market = -2;
	public const int Inventory = -1;
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
    public CitizenItem currentCitizen;
	public CitizenItem me = new();
    public RentedRoomItem currentRentedRoom;
    public PageItem currentPage;

	public int cursorX;
	public int cursorY;
	public int cursorZ;

	public GameObject shortcutsActiveSign;
	public static bool IsShortcutsActive { get; private set; }

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
		SetShortcutsActive(true);
	}

	public static void SetShortcutsActive(bool isActive)
	{
		IsShortcutsActive = isActive;
		GameManager.Instance.shortcutsActiveSign.GetComponent<Image>().color = isActive ? Color.green : Color.red;
		GameManager.Instance.shortcutsActiveSign.GetComponentInChildren<TMP_Text>().text = isActive ? "SHORCUTS ENABLED" : "SHORCUTS DISABLED";
	}
}
