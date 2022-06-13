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

    private Side.WebBrowser _webBrowser;

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(Content, Mouse.current.position.ReadValue(), null);
        if (linkIndex == -1)
        {
            return;
        }

        TMP_LinkInfo linkInfo = Content.textInfo.linkInfo[linkIndex];
        var linkId = linkInfo.GetLinkID();

        if (linkId.StartsWith("?"))
        {
            _webBrowser = NetworkManager.Instance.InstantiateWebBrowser("");
            _webBrowser.gameObject.SetActive(false);
            ExecCommand(linkId.Substring(1));
        }
        else
        {
            NetworkManager.Instance.InstantiateWebBrowser(linkId);
        }
    }

    private void ExecCommand(string commandWithArgs)
    {
        var c = commandWithArgs.Split(":");
        var command = c[0];
        var args = new string[]{};
        if (c.Length > 1)
        {
            args = c[1].Split(",");
        }

        UnityAction<string> action;
        switch (command)
        {
            case "goto":
                Application.OpenURL($"https://t.me/{args[0]}");
                break;
            case "chat":
                NetworkManager.Instance.CitizenChatButton(args[0]);
                break;
            case "deal":
                var (marketId, _) = GameManager.ParseInternetAddress(_webBrowser.AddressBar.text);
                NetworkManager.Instance.DealCreate(int.Parse(marketId), GameManager.Instance.Citizen.account_id,
                    GameManager.Instance.Citizen.delivery_address.id != 0
                        ? GameManager.Instance.Citizen.delivery_address.id
                        : GameManager.Instance.Citizen.room.id,
                    int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]));
                break;
            case "invite":
                StartCoroutine(NetworkManager.Instance.Request("invite", new string[]{args[0]}, (json) => {
                    NetworkManager.Instance.InstantiateInvitePopup(JsonUtility.FromJson<InviteResponse>(json).invite);
                }));
                break;
            case "offer":
                StartCoroutine(NetworkManager.Instance.Request("offer", new string[]{args[0]}, (json) => {
                    NetworkManager.Instance.InstantiateOfferPopup(JsonUtility.FromJson<OfferResponse>(json).offer);
                }));
                break;
            case "poll":
                StartCoroutine(NetworkManager.Instance.Request("poll", new string[]{GameManager.Instance.Citizen.id.ToString(), args[0]}, (json) => {
                    NetworkManager.Instance.InstantiatePollPopup(JsonUtility.FromJson<PollResponse>(json).poll);
                }));
                break;
            case "task-accept":
                StartCoroutine(NetworkManager.Instance.Request("task-accept", new string[]{GameManager.Instance.Citizen.id.ToString(), args[0]}, (json) => {
                    NetworkManager.Instance.InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
                }));
                break;
            case "member-create":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{GameManager.Instance.Citizen.id.ToString(), args[0]}, (json) => {
                    _webBrowser.LoadPath("community");
                }));
                break;
            case "member-delete":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{GameManager.Instance.Citizen.id.ToString(), args[0]}, (json) => {
                    _webBrowser.ReloadPage();
                }));
                break;
            case "room-input-organization-id":
                action = (text) => StartCoroutine(NetworkManager.Instance.Request("room-attach", new string[]{args[0], text}, (json) => {
                    NetworkManager.Instance.InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
                }));
                NetworkManager.Instance.InstantiateInputFieldPopup("Enter Organization ID", "-", action);
                break;
            case "room-detach":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{GameManager.Instance.Citizen.id.ToString(), args[0]}, (json) => {
                    _webBrowser.LoadPath("root");
                }));
                break;
            case "organization-input-parent-id":
                action = (text) => StartCoroutine(NetworkManager.Instance.Request("organization-attach", new string[]{GameManager.Instance.Citizen.id.ToString(), args[0], text}, (json) => {
                    NetworkManager.Instance.InstantiateNoticePopup("STATUS", JsonUtility.FromJson<Response>(json).status);
                }));
                NetworkManager.Instance.InstantiateInputFieldPopup("Enter Parent Organization ID", "-", action);
                break;
            case "organization-change-join-type":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{GameManager.Instance.Citizen.id.ToString(), args[0]}, (json) => {
                    _webBrowser.ReloadPage();
                }));
                break;
            case "organization-change-leave-type":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{GameManager.Instance.Citizen.id.ToString(), args[0]}, (json) => {
                    _webBrowser.ReloadPage();
                }));
                break;
            case "organization-detach":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{GameManager.Instance.Citizen.id.ToString(), args[0]}, (json) => {
                    _webBrowser.ReloadPage();
                }));
                break;
            case "organization-input-title":
                action = (text) => StartCoroutine(NetworkManager.Instance.Request("organization-set-title", new string[]{args[0], NetworkManager.Instance.Escape(text)}, (json) => {
                    _webBrowser.ReloadPage();
                }));
		        NetworkManager.Instance.InstantiateInputFieldPopup("Enter Organization Title", "", action);
                break;
            case "rule-change":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{GameManager.Instance.Citizen.id.ToString(), args[0], args[1]}, (json) => {
                    _webBrowser.ReloadPage();
                }));
                break;
            case "poll-create":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{GameManager.Instance.Citizen.id.ToString(), args[0], args[1]}, (json) => {
                    _webBrowser.LoadPath("*oll");
                }));
                break;
            case "poll-delete":
                StartCoroutine(NetworkManager.Instance.Request(command, new string[]{GameManager.Instance.Citizen.id.ToString(), args[0]}, (json) => {
                    _webBrowser.LoadPath("root");
                }));
                break;
            default:
                NetworkManager.Instance.InstantiateNoticePopup("ERROR", $"Command \"{command}\" not supported");
                break;
        }
    }
}
