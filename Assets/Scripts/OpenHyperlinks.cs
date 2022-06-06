using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Models;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class OpenHyperlinks : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text Content;

    private Side.ComputerInternetTab _pda;

    private void Start()
    {
        _pda = GameObject.Find("ComputerWindow(Clone)")
            .GetComponentInChildren<Side.ComputerInternetTab>();
    }

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
            _pda.LoadPage(linkId);
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

        UnityAction<string> action;
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
            case "task-accept":
                StartCoroutine(NetworkManager.Instance.Request("task-accept", new string[]{GameManager.Instance.me.id.ToString(), args[0]}, (json) => {
                    NetworkManager.Instance.InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
                }));
                break;
            case "member-create":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{args[0], GameManager.Instance.me.id.ToString()}, (json) => {
                    _pda.LoadPath("m*mb*rs");
                }));
                break;
            case "member-delete":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{args[0], args[1]}, (json) => {
                    _pda.ReloadPage();
                }));
                break;
            case "room-input-organization-id":
                action = (text) => StartCoroutine(NetworkManager.Instance.Request("room-attach", new string[]{args[0], text}, (json) => {
                    NetworkManager.Instance.InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
                }));
                NetworkManager.Instance.InstantiateInputFieldPopup("Enter Organization ID", action);
                break;
            case "room-detach":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{GameManager.Instance.me.id.ToString(), args[0]}, (json) => {
                    _pda.LoadPath("root");
                }));
                break;
            case "organization-create":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{GameManager.Instance.me.id.ToString()}, (json) => {
                    _pda.LoadPage($"{JsonUtility.FromJson<OrganizationResponse>(json).organization.id}/root");
                }));
                break;
            case "organization-input-parent-id":
                action = (text) => StartCoroutine(NetworkManager.Instance.Request("organization-attach", new string[]{args[0], text}, (json) => {
                    NetworkManager.Instance.InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
                }));
                NetworkManager.Instance.InstantiateInputFieldPopup("Enter Parent Organization ID", action);
                break;
            case "organization-detach":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{GameManager.Instance.me.id.ToString(), args[0]}, (json) => {
                    _pda.ReloadPage();
                }));
                break;
            case "organization-input-title":
                action = (text) => StartCoroutine(NetworkManager.Instance.Request("organization-set-title", new string[]{args[0], NetworkManager.Instance.Escape(text)}, (json) => {
                    _pda.ReloadPage();
                }));
		        NetworkManager.Instance.InstantiateInputFieldPopup("Enter Organization Title", action);
                break;
            default:
                NetworkManager.Instance.InstantiateNoticePopup("ERROR", $"Command \"{command}\" not supported");
                // NetworkManager.Instance.Exec(command, args);
                break;
        }
    }
}
