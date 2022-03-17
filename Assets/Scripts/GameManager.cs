using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public NetworkManager networkManager;
	public LocationItem currentLocation;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine(networkManager.Location(-1));
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

	public void ExitButton()
	{
		if (currentLocation.address.type_id > 0)
		{
			StartCoroutine(networkManager.Location(currentLocation.parent_id));
		}
	}

	public void InventoryButton(int citizenId)
	{
		Debug.Log("InventoryButton");
	}
}
