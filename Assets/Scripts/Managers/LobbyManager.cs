using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoSingleton<LobbyManager>
{
    public void LoadLobby()
    {
        UITools.Instance.ShowUI(eUIType.PF_UI_LOBBY);
    }

    public void DisableLobby()
    {
        UITools.Instance.HideUI(eUIType.PF_UI_LOBBY);
    }
}
