using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lobby : BaseObject
{
    // LobbyIcon Btn
    UIButton CardBtn = null;
    UIButton BattleBtn = null;
    UIButton GachaBtn = null;

    // GachaGround Btn
    UIButton BoxBtn = null;
    // Chest Open
    Transform chest = null;
    Transform boxOpenEff = null;

    // BattleStart Btn
    UIButton BattleStartBtn = null;

    ////////////////////////////////////////////
    //PlayerDeck PlayerDeck = null;

    // Coin
    UILabel CoinLabel;
    int CoinValue = 0;
    int test1 = 0;

    // Trophy
    UILabel TrophyLabel;
    int TrophyValue = 2;
    int test2 = 0;

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

        // Coin
        //------------------------------------------------------
        CoinLabel = FindInChild("Coin").FindChild("Text").GetComponent<UILabel>();
        PlayerPrefs.GetInt("Test").ToString();

        // Trophy
        //------------------------------------------------------
        TrophyLabel = FindInChild("Trophy").FindChild("Text").GetComponent<UILabel>();
        PlayerPrefs.GetInt("Test2").ToString();
    }

    private void Update()
    {
        CoinLabel.text = PlayerPrefs.GetInt("Test").ToString();
        TrophyLabel.text = PlayerPrefs.GetInt("Test2").ToString();
    }

    void CardGround()
    {
        Transform trans = FindInChild("LOBBYGROUND");
        trans.localPosition = new Vector3(720.0f, 0.0f, 0.0f);
        trans.GetComponent<UIPanel>().clipOffset = new Vector2(-720.0f, 0.0f);
        chest.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        boxOpenEff.gameObject.SetActive(false);
    }

    void BattleGround()
    {
        Transform trans = FindInChild("LOBBYGROUND");
        trans.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        trans.GetComponent<UIPanel>().clipOffset = new Vector2(0.0f, 0.0f);
        chest.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        boxOpenEff.gameObject.SetActive(false);
    }

    void GachaGround()
    {
        Transform trans = FindInChild("LOBBYGROUND");
        trans.localPosition = new Vector3(-720.0f, 0.0f, 0.0f);
        trans.GetComponent<UIPanel>().clipOffset = new Vector2(720.0f, 0.0f);

        chest.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        boxOpenEff.gameObject.SetActive(false);
    }

    void BoxOpen()
    {
        //Test--------------------------------------------------
        CoinValue = PlayerPrefs.GetInt("CoinValue");
        test1 = CoinValue++;
        PlayerPrefs.SetInt("CoinValue", CoinValue);
        PlayerPrefs.SetInt("Test", test1);

        TrophyValue = PlayerPrefs.GetInt("TrophyValue");
        test2 = TrophyValue++;
        PlayerPrefs.SetInt("TrophyValue", TrophyValue);
        PlayerPrefs.SetInt("Test2", test2);
        //------------------------------------------------------

        StartCoroutine(BoxOpenEff());
    }

    IEnumerator BoxOpenEff()
    {
        boxOpenEff.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        chest.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
    }

    void BattleStart()
    {
        Scene_Manager.Instance.LoadScene(eSceneType.test);
    }


}
