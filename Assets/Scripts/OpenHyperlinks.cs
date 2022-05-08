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
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = content.textInfo.linkInfo[linkIndex];

            var a = linkInfo.GetLinkID().Split("/");

            var address = a[0];

            var path = "";
            if (a.Length > 1)
            {
                path = a[1];
            }

            NetworkManager.Instance.Page(address, path);
        }
    }
}
