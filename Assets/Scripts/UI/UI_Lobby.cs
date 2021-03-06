﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lobby : BaseObject
{
    public Transform OriginalTransform;
    // LobbyIcon Btn
    UIButton CardBtn = null;
    UIButton BattleBtn = null;
    UIButton GachaBtn = null;

    UIButton ConnectCancelBtn =null;

    // GachaGround Btn
    UIButton BoxBtn = null;
    // Chest Open
    Transform chest = null;
    Transform boxOpenEff = null;

    // BattleStart Btn
    UIButton BattleStartBtn = null;

    Transform serverConnect = null;

    ////////////////////////////////////////////
    //PlayerDeck PlayerDeck = null;

    float dTime;

    // Coin
    UILabel CoinLabel;
    int CoinValue = 0;

    // Trophy
    UILabel TrophyLabel;
    int TrophyValue = 0;

    // Medal
    UILabel MedalLabel;
    int MedalValue = 0;

    private void Awake()
    {
        // LobbyIcon Btn
        //----------------------------------------------------
        Transform trans = FindInChild("CardBtn");
        if (trans == null)
        {
            Debug.LogError("CardBtn is Not Founded");
            return;
        }
        CardBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(CardBtn.onClick,
            new EventDelegate(this, "CardGround"));

        // LobbyIcon Btn
        //----------------------------------------------------
        trans = FindInChild("BattleBtn");
        if (trans == null)
        {
            Debug.LogError("BattleBtn is Not Founded");
            return;
        }
        BattleBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(BattleBtn.onClick,
            new EventDelegate(this, "BattleGround"));

        // LobbyIcon Btn
        //----------------------------------------------------
        trans = FindInChild("GachaBtn");
        if (trans == null)
        {
            Debug.LogError("GachaBtn is Not Founded");
            return;
        }
        GachaBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(GachaBtn.onClick,
            new EventDelegate(this, "GachaGround"));

        // GachaGround Btn
        //-----------------------------------------------------
        trans = FindInChild("ChestUIBtn");
        if (trans == null)
        {
            Debug.LogError("ChestUIBtn is Not Founded");
            return;
        }
        BoxBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(BoxBtn.onClick, new EventDelegate(this, "BoxOpen"));
        // Chest Instantiate
        GameObject ChestPrefab = Resources.Load("Prefabs/UI/ChestCamera") as GameObject;
        GameObject Chest = Instantiate(ChestPrefab);
        chest = Chest.transform.FindChild("Chest/Chest_cover");
        boxOpenEff = Chest.transform.FindChild("Eff_BoxOpenShine");

        // BattleStart Btn
        //-----------------------------------------------------
        trans = FindInChild("StartBtn");
        if (trans == null)
        {
            Debug.LogError("BattleStartBtn is Not Founded");
            return;
        }
        BattleStartBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(BattleStartBtn.onClick, new EventDelegate(this, "BattleStart"));

        ////////////////////
        //PlayerDeck = FindInChild("BattleDeck").GetComponent<PlayerDeck>();

        // Level
        //------------------------------------------------------
        //LevelLabel = gameObject.transform.Find("PF_UI_LOBBYICON").transform.Find("Level").transform.Find("Text").GetComponent<UILabel>();
        MedalLabel = FindInChild("Medal").FindChild("Text").GetComponent<UILabel>();

        // Trophy
        //------------------------------------------------------
        TrophyLabel = FindInChild("Trophy").FindChild("Text").GetComponent<UILabel>();

        // Coin
        //------------------------------------------------------
        CoinLabel = FindInChild("Coin").FindChild("Text").GetComponent<UILabel>();

        // Server Connect
        serverConnect = FindInChild("serverConnect");
        trans = serverConnect.FindChild("Button");
        if (trans == null)
        {
            Debug.LogError("GachaBtn is Not Founded");
            return;
        }
        ConnectCancelBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(ConnectCancelBtn.onClick,
            new EventDelegate(this, "ConnectCancel"));


        ScoreUpdate();
    }

    private void Update()
    {
        dTime += Time.deltaTime;
    }

    void CardGround()
    {
        Transform trans = FindInChild("LOBBYGROUND");
        StartCoroutine(PageChange(OriginalTransform.localPosition, new Vector3(720.0f, 0f, 0f), dTime));
        //trans.localPosition = new Vector3(720.0f, 0.0f, 0.0f);
        trans.GetComponent<UIPanel>().clipOffset = new Vector2(-720.0f, 0.0f);
        chest.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        boxOpenEff.gameObject.SetActive(false);
    }

    void BattleGround()
    {
        Transform trans = FindInChild("LOBBYGROUND");
        StartCoroutine(PageChange(OriginalTransform.localPosition, new Vector3(0f, 0f, 0f), dTime));
        //trans.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        trans.GetComponent<UIPanel>().clipOffset = new Vector2(0.0f, 0.0f);
        chest.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        boxOpenEff.gameObject.SetActive(false);
    }

    void GachaGround()
    {
        Transform trans = FindInChild("LOBBYGROUND");
        StartCoroutine(PageChange(OriginalTransform.localPosition, new Vector3(-720.0f, 0f, 0f), dTime));
        //trans.localPosition = new Vector3(-720.0f, 0.0f, 0.0f);
        trans.GetComponent<UIPanel>().clipOffset = new Vector2(720.0f, 0.0f);

        chest.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        boxOpenEff.gameObject.SetActive(false);
    }

    void BoxOpen()
    {
        if (CoinValue >= 100)
        {
            CoinValue = PlayerPrefs.GetInt("CoinValue");
            CoinValue -= 100;
            if (CoinValue <= 0)
                CoinValue = 0;
            PlayerPrefs.SetInt("CoinValue", CoinValue);

            CardManager.Instance.Gacha();
            UI_CardGround.Instance.UpdateCardGround();

            // 이펙트 효과
            StartCoroutine(BoxOpenEff());
        }
        else
        {
            GachaFail();
        }

        ScoreUpdate();
    }

    void ScoreUpdate()
    {
        CoinLabel.text = PlayerPrefs.GetInt("CoinValue").ToString();
        TrophyLabel.text = PlayerPrefs.GetInt("TrophyValue").ToString();
        TrophyValue = PlayerPrefs.GetInt("TrophyValue");

        MedalValue = TrophyValue / 30;
        if (MedalValue == 0)
            MedalValue++;
        PlayerPrefs.SetInt("MedalValue", MedalValue);

        MedalLabel.text = PlayerPrefs.GetInt("MedalValue").ToString();
    }

    IEnumerator PageChange(Vector3 orgPos, Vector3 targetPos, float dTime)
    {
        float delta = 0f;
        float time = 1f;
        Transform trans = FindInChild("LOBBYGROUND");
        while (delta <= time)
        {
            trans.localPosition = NGUIMath.SpringLerp(orgPos, targetPos, 10f, delta);
            delta += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator BoxOpenEff()
    {
        boxOpenEff.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        chest.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
    }

    void GachaFail()
    {
        CoinValue = PlayerPrefs.GetInt("CoinValue");

        if (CoinValue < 100)
        {
            GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_POPUP);
            UI_Popup popup = go.GetComponent<UI_Popup>();

            popup.Set(
                () =>
                {
                    UI_Tools.Instance.HideUI(eUIType.PF_UI_POPUP);
                }
                ,
                "실패하였습니다."
                ,
                "코인이 부족하여 가챠를 뽑을 수 없습니다. " +
                "코인을 다시 모아주세요~!!"
                );
        }
    }

    void BattleStart()
    {
        serverConnect.gameObject.SetActive(true);
        GameManager.Instance.IssueConnect();
        //Scene_Manager.Instance.LoadScene(eSceneType.SCENE_GAME);
    }

    void ConnectCancel()
    {
        GameManager.Instance.GoToLobby();
        serverConnect.gameObject.SetActive(false);
    }
}
