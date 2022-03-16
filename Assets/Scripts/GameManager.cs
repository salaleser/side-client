using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public NetworkManager networkManager;
	public Location currentLocation;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			StartCoroutine(networkManager.Location(-1));
		}
		if (Input.GetKeyDown(KeyCode.N))
		{
			StartCoroutine(networkManager.Location(-2));
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			if (currentLocation.address.type_id > 0)
			{
				StartCoroutine(networkManager.Location(currentLocation.parent_id));
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			SceneManager.LoadScene("Main");
		}
	}

	public void EnterButton(int locationId)
	{
		StartCoroutine(networkManager.Location(locationId));
	}

	public void ExitButton(int locationId)
	{
		StartCoroutine(networkManager.Location(locationId));
	}
}
