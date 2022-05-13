using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Models;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class OpenHyperlinks : MonoBehaviour, IPointerClickHandler
{
	public GameObject buyPopupPrefab;
    public TMP_Text content;

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(content, Input.mousePosition, null);
        if (linkIndex == -1)
        {
            return;
        }

        TMP_LinkInfo linkInfo = content.textInfo.linkInfo[linkIndex];
        var linkId = linkInfo.GetLinkID();
        var linkText = linkInfo.GetLinkText();
        if (linkId.StartsWith("?"))
        {
            ExecCommand(linkId, linkText);
        }
        else
        {
            LoadPage(linkId);
        }
    }

    private void ExecCommand(string linkId, string linkText)
    {
        var c = linkId.Substring(1).Split(":");
        var command = c[0];
        var parameters = new string[]{};
        if (c.Length > 1)
        {
            parameters = c[1].Split(",");
        }

        switch (command)
        {
            case "buy":
                NetworkManager.Instance.DealCreate(GameManager.Instance.currentPage.organization_id, GameManager.Instance.me.account_id,
                    GameManager.Instance.me.delivery_address != 0 ? GameManager.Instance.me.delivery_address : GameManager.Instance.me.room_id,
                    int.Parse(parameters[0]), int.Parse(parameters[1]), int.Parse(parameters[2]));
                break;
            default:
                NetworkManager.Instance.Exec(command, parameters);
                break;
        }
    }

    private void LoadPage(string link)
    {
        var a = link.Split("/");

        var address = a[0];

        var path = "";
        if (a.Length > 1)
        {
            path = a[1];
        }

        NetworkManager.Instance.Page(address, path);
    }
}
