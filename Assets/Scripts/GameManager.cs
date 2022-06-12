using System.IO;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Models;
using TMPro;
using Side;

public class GameManager : MonoBehaviour
{
	public const int StateUniverse = 1;
	public const int StateGalaxy = 2;
	public const int StateSystem = 3;
	public const int StatePlanet = 4;
	public const int StateContinent = 5;
	public const int StateRegion = 6;
	public const int StateParcel = 7;

	public int State;
	
	public UserItem User;
	public CitizenItem Citizen;
	public UniverseItem Universe;
	public GalaxyItem Galaxy;
	public SystemItem System;
	public PlanetItem Planet;
	public ContinentItem Continent;
	public RegionItem Region;
	public ParcelItem Parcel;
    
	public GameObject CursorPrefab;
	public GameObject Cursor { get; private set; }

	public GameObject DescriptionPrefab;
	public static GameObject Description { get; private set; }

	public GameObject MiniMapPrefab;
	public static GameObject MiniMap { get; private set; }

	public GameObject ChatPrefab;
	public static GameObject Chat { get; private set; }
	public ConcurrentQueue<Message> ChatMessages = new();

	public GameObject ShortcutsActiveSign;
	public static bool RadialMenuActive { get; private set; }
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
		MiniMap = Instantiate(MiniMapPrefab, NetworkManager.Instance.UiCanvas.transform);

		Chat = Instantiate(ChatPrefab, NetworkManager.Instance.UiCanvas.transform);

		Cursor = Instantiate(CursorPrefab, NetworkManager.Instance.UiCanvas.transform);

		Description = Instantiate(DescriptionPrefab, NetworkManager.Instance.UiCanvas.transform);

		SetShortcutsActive(true);
	}

	public static void SetShortcutsActive(bool isActive)
	{
		ShortcutsActive = isActive;
		Instance.ShortcutsActiveSign.GetComponent<Image>().color = isActive ? Color.green : Color.red;
		Instance.ShortcutsActiveSign.GetComponentInChildren<TMP_Text>().text = isActive ? "SHORCUTS ENABLED" : "SHORCUTS DISABLED";
	}

	public static void SetRadialMenuActive(bool isActive)
	{
		RadialMenuActive = isActive;
	}

	public static void DescriptionSetActive(bool isActive)
	{
		Description.SetActive(isActive);
	}

	public static void DescriptionSetText(string text)
	{
		Description.GetComponent<Description>().Text.text = text;
	}

	public static void SetWindowActive(bool isActive)
	{
		WindowActive = isActive;
	}

	public static void SetPopupActive(bool isActive)
	{
		PopupActive = isActive;
	}

	public static (string, string) ParseInternetAddress(string internetAddress)
	{
		var a = internetAddress.Split("/");

        var address = a[0];

        var path = "root";
        if (a.Length > 1)
        {
            path = a[1];
        }

		return (address, path);
	}
}
