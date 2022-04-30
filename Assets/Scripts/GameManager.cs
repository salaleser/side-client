using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Models;

public class GameManager : MonoBehaviour
{
	public const int Floor = 1;
	public const int Map = 2;
	public const int Market = 3;
	public const int Inventory = 4;

	public int state;
	
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
