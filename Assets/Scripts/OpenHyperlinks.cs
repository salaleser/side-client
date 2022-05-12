using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Models;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class OpenHyperlinks : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text content;

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(content, Input.mousePosition, null);
        if (linkIndex == -1)
        {
            return;
        }

        TMP_LinkInfo linkInfo = content.textInfo.linkInfo[linkIndex];
        var link = linkInfo.GetLinkID();
        if (link.StartsWith("?"))
        {
            ExecCommand(link);
        }
        else
        {
            LoadPage(link);
        }
    }

    private void ExecCommand(string link)
    {
        var c = link.Substring(1).Split(":");
        var command = c[0];
        var parameters = new string[]{};
        if (c.Length > 1)
        {
            parameters = c[1].Split(",");
        }

        NetworkManager.Instance.Exec(command, parameters);
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
