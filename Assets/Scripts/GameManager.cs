using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Models;

public class GameManager : MonoBehaviour
{
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
	
	public UniverseItem universe;
	public GalaxyItem galaxy;
	public SystemItem system;
	public PlanetItem planet;
	public ContinentItem continent;
	public RegionItem region;
	public CityItem city;
	public BlockItem block;
	public ParcelItem parcel;
	public FloorItem floor;
	public CitizenItem citizen = new();
    public OrganizationItem organization;

	public int cursorX;
	public int cursorY;

	public static GameManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			citizen.id = 263; // FIXME
			citizen.parcel_id = 1; // FIXME

			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
