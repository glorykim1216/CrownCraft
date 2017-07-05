using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Logo : BaseObject
{
    UIButton StartBtn = null;
    void Start()
    {
        Transform temp = FindInChild("StartBtn");
        if (temp == null)
        {
            Debug.LogError(gameObject.name + " 에 StartBtn 이" + "없습니다.");
            return;
        }
        StartBtn = temp.gameObject.GetComponent<UIButton>();
        EventDelegate.Add(StartBtn.onClick, new EventDelegate(this, "GoLobby"));

        //// Lamda식 사용
        //EventDelegate.Add(StartBtn.onClick, () =>
        // {
        //	 Scene_Manager.Instance.LoadScene(eSceneType.SCENE_LOBBY);
        // });

    }

    void GoLobby()
    {
        Scene_Manager.Instance.LoadScene(eSceneType.SCENE_LOBBY);
    }
}
