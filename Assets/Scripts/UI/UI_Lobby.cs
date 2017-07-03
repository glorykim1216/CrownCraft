using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lobby : BaseObject
{
    // LobbyIcon btn
    UIButton CardBtn = null;
    UIButton BattleBtn = null;
    UIButton GachaBtn = null;

    // GachaGround Btn
    UIButton BoxBtn = null;
    Transform chest = null;
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
        if(trans == null)
        {
            Debug.LogError("ChestUIBtn is Not Founded");
            return;
        }
        BoxBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(BoxBtn.onClick, new EventDelegate(this, "BoxOpen"));

        GameObject ChestPrefab = Resources.Load("Prefabs/UI/ChestCamera") as GameObject;
        GameObject Chest = Instantiate(ChestPrefab);
        chest = Chest.transform.FindChild("Chest/Chest_cover");
    }

    void CardGround()
    {
        Transform trans = FindInChild("LOBBYGROUND");
        trans.localPosition = new Vector3(720.0f, 0.0f, 0.0f);
        trans.GetComponent<UIPanel>().clipOffset = new Vector2(-720.0f, 0.0f);
        chest.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    void BattleGround()
    {
        Transform trans = FindInChild("LOBBYGROUND");
        trans.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        trans.GetComponent<UIPanel>().clipOffset = new Vector2(0.0f, 0.0f);
        chest.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    void GachaGround()
    {
        Transform trans = FindInChild("LOBBYGROUND");
        trans.localPosition = new Vector3(-720.0f, 0.0f, 0.0f);
        trans.GetComponent<UIPanel>().clipOffset = new Vector2(720.0f, 0.0f);
        //GameObject go = UITools.Instance.ShowUI(eUIType.ChestCamera);
        //UI_CardPopup popup = go.GetComponent<UI_CardPopup>();

        chest.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

    }

    void BoxOpen()
    {
        //FindInChild("Chest_cover");
        chest.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        Debug.Log(chest.localRotation);
    }
}
