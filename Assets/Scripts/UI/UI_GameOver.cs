using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameOver : BaseObject
{
    UIButton OKBtn = null;

    private void Awake()
    {
        // LobbyIcon Btn
        //----------------------------------------------------
        Transform trans = FindInChild("OKBtn");
        if (trans == null)
        {
            Debug.LogError("OKBtn is Not Founded");
            return;
        }
        OKBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(OKBtn.onClick,
            new EventDelegate(this, "SceneChange"));
    }

    void SceneChange()
    {
        Scene_Manager.Instance.LoadScene(eSceneType.SCENE_LOBBY);
    }

    public void Result(int EnemyTowerDestroyCount, int PlayerTowerDestroyCount)
    {
        // Star UI
        Transform trans = FindInChild("Red");
        for (int i = 0; i < PlayerTowerDestroyCount; i++)
        {
            Transform star = trans.FindChild("Star" + i);
            star.gameObject.SetActive(true);
        }

        trans = FindInChild("Blue");
        for (int i = 0; i < EnemyTowerDestroyCount; i++)
        {
            Transform star = trans.FindChild("Star" + i);
            star.gameObject.SetActive(true);
        }

        // Win UI
        if (EnemyTowerDestroyCount > PlayerTowerDestroyCount)
        {
            trans = FindInChild("Blue").FindChild("Win");
            trans.gameObject.SetActive(true);
        }
        else
        {
            trans = FindInChild("Red").FindChild("Win");
            trans.gameObject.SetActive(true);
        }
        
    }
}
