using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Models;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class OpenHyperlinks : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text Content;

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(Content, Mouse.current.position.ReadValue(), null);
        if (linkIndex == -1)
        {
            return;
        }

        TMP_LinkInfo linkInfo = Content.textInfo.linkInfo[linkIndex];
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
        var args = new string[]{};
        if (c.Length > 1)
        {
            args = c[1].Split(",");
        }

        Debug.Log(linkId);

        switch (command)
        {
            case "deal":
                NetworkManager.Instance.DealCreate(int.Parse(GameManager.Instance.currentPage.address), GameManager.Instance.me.account_id,
                    GameManager.Instance.me.delivery_address.id != 0 ? GameManager.Instance.me.delivery_address.id : GameManager.Instance.me.room.id,
                    int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]));
                break;
            case "invite":
                StartCoroutine(NetworkManager.Instance.Request("invite", new string[]{args[0]}, (json) => {
                    var invite = JsonUtility.FromJson<InviteResponse>(json).invite;
                    NetworkManager.Instance.InstantiateInvitePopup(invite);
                }));
                break;
            case "offer":
                StartCoroutine(NetworkManager.Instance.Request("offer", new string[]{args[0]}, (json) => {
                    var offer = JsonUtility.FromJson<OfferResponse>(json).offer;
                    NetworkManager.Instance.InstantiateOfferPopup(offer);
                }));
                break;
            case "task":
                StartCoroutine(NetworkManager.Instance.Request("task-accept", new string[]{GameManager.Instance.me.id.ToString(), args[0]}, null));
                break;
            case "member-create":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{args[0], GameManager.Instance.me.id.ToString()}, null));
                break;
            case "member-delete":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{args[0], args[1]}, null));
                break;
            default:
                // NetworkManager.Instance.Exec(command, args);
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

        var args = new string[]{GameManager.Instance.me.id.ToString(), address, path};
        StartCoroutine(NetworkManager.Instance.Request("page", args, (json) => {
            var page = JsonUtility.FromJson<PageResponse>(json).page;
            var window = GameObject.Find("ComputerWindow(Clone)")
                .GetComponentInChildren<Side.ComputerInternetTab>();
            window.Content.text = page.content;
            window.AddressBar.text = $"{page.address}/{page.path}";
        }));
    }
}
