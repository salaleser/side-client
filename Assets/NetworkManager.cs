using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    private const string host = "65.21.107.192";
    private const string port = "65278";

	public Text text;
	public GameObject buttonsPanel;
	public GameObject buttonPrefab;
	public GameObject locationPrefab;

	private readonly List<GameObject> buttonsList = new();

	public NetworkManager()
    {
    }

	public IEnumerator Location(int locationId)
    {
		var query = $"location_id={locationId}";
		var request = UnityWebRequest.Get($"http://{host}:{port}/location?{query}");
		yield return request.SendWebRequest();

		if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

		var response = JsonConvert.DeserializeObject<LocationResponse>(request.downloadHandler.text);

		foreach(var c in CellManager.instance.cells)
		{
			foreach(var l in response.Locations)
            {
				if (c.X == l.X && c.Y == l.Y)
				{
					Instantiate(locationPrefab, c.O.transform);
				}
			}
		}
	}

	public IEnumerator Route(string command, string userId, string userName, string chatId, string chatTitle)
	{
		var query = $"command={command}&user_id={userId}&user_name={userName}&chat_id={chatId}&chat_title={chatTitle}";
		var request = UnityWebRequest.Get($"http://{host}:{port}/route?{query}");
		yield return request.SendWebRequest();

		if (request.result != UnityWebRequest.Result.Success)
		{
			Debug.Log(request.error);
			yield break;
		}

		var r = Parse(request.downloadHandler.text);
		text.text = r.Text;

		var pt = buttonsPanel.transform;
		var px = pt.position.x;
		var py = pt.position.y;
		var pz = pt.position.z;
		var pw = buttonsPanel.GetComponent<RectTransform>().rect.width;
		var ph = buttonsPanel.GetComponent<RectTransform>().rect.height;
		var bw = buttonPrefab.GetComponent<RectTransform>().rect.width;
		var bh = buttonPrefab.GetComponent<RectTransform>().rect.height;
		for (var i = 0; i < r.Buttons.Length; i++)
		{
			for (var j = 0; j < r.Buttons[i].Length; j++)
			{
				var b = Instantiate(buttonPrefab, pt);
				b.GetComponentInChildren<Text>().text = r.Buttons[i][j].Text;
				var bp = new Vector3(px - pw / 2 + bw / 2 + bh * j, py + ph / 2 - bh / 2 - bh * i, pz);
				b.transform.SetPositionAndRotation(bp, Quaternion.identity);

				var data = r.Buttons[i][j].Data;
				b.GetComponent<Button>().onClick.AddListener(() => {
					buttonsList.ForEach(o => Destroy(o));
					StartCoroutine(Route(data, userId, userName, chatId, chatTitle));
				});

				buttonsList.Add(b);
			}
		}
	}

	struct SideResponse
	{
		public SideResponse(string text, side.Button[][] buttons)
		{
			Text = text;
			Buttons = buttons;
		}

		public string Text { get; }
		public side.Button[][] Buttons { get; }
	}

	private SideResponse Parse(string rawSideResponse)
	{
		var separatorIndex = rawSideResponse.IndexOf("\",\"");
		var text = rawSideResponse[2..separatorIndex];

		var rawButtons = rawSideResponse
			.Substring(separatorIndex + 3, rawSideResponse.Length - separatorIndex - 7)
			.Replace("\"\"", "'");
		var buttons = JsonConvert.DeserializeObject<side.Button[][]>(rawButtons);

		var result = new SideResponse(text, buttons);
		return result;
	}
}
