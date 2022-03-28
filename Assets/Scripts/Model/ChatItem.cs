using System;
using System.Collections.Generic;

[System.Serializable]
public class ChatItem
{
    public string citizen_id;
    public string text;
    public string created_at;

    public override string ToString() => @$"[{DateTime.Parse(created_at).ToString("dd.MM.yyyy hh:mm:ss")}] {citizen_id}: {text}";
}
