using UnityEngine;

public class GameManager : MonoBehaviour
{
	public NetworkManager networkManager;

	private void Start()
	{
		var command = "r_inspect";
		var userId = "802496341";
		var userName = "salaleser";
		var chatId = "-590600288";
		var chatTitle = "test_chat_title";

		StartCoroutine(networkManager.Route(command, userId, userName, chatId, chatTitle));
	}
}
