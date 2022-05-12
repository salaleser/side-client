using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Models;

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
	public const int Floor = 10;
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
	public FloorItem currentFloor;
    public OrganizationItem currentOrganization;
    public CitizenItem currentCitizen;
	public CitizenItem me = new();
    public ItemItem currentItem;
    public OrganizationItem newOrganization;

	public int cursorX;
	public int cursorY;

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
}
