using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public const int Floor = 1;
	public const int Map = 2;
	public const int Market = 3;
	public const int Inventory = 4;

	public CitizenItem currentCitizen;
	public int state;
	public int mapLocationId;
	public int? mapAddressId;
	public int mapAddressParentId;
	public int mapFloorId;

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
