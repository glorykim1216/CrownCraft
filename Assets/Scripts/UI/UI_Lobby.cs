using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lobby : BaseObject
{
    UIButton CardBtn = null;
    UIButton BattleBtn = null;
    UIButton GachaBtn = null;

    private void Awake()
    {
        Transform trans = FindInChild("CardBtn");
        if (trans == null)
        {
            Debug.LogError("CardBtn is Not Founded");
            return;
        }
        CardBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(CardBtn.onClick,
            new EventDelegate(this, "CardGround"));

        trans = FindInChild("BattleBtn");
        if (trans == null)
        {
            Debug.LogError("BattleBtn is Not Founded");
            return;
        }
        BattleBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(BattleBtn.onClick,
            new EventDelegate(this, "BattleGround"));

        trans = FindInChild("GachaBtn");
        if (trans == null)
        {
            Debug.LogError("GachaBtn is Not Founded");
            return;
        }
        GachaBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(GachaBtn.onClick,
            new EventDelegate(this, "GachaGround"));

       
    }

    void CardGround()
    {
        Transform trans = FindInChild("LOBBYGROUND");
        trans.localPosition = new Vector3(720.0f, 0.0f, 0.0f);
        trans.GetComponent<UIPanel>().clipOffset = new Vector2(-720.0f, 0.0f);
    }

    void BattleGround()
    {
        Transform trans = FindInChild("LOBBYGROUND");
        trans.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        trans.GetComponent<UIPanel>().clipOffset = new Vector2(0.0f, 0.0f);
    }

    void GachaGround()
    {
        Transform trans = FindInChild("LOBBYGROUND");
        trans.localPosition = new Vector3(-720.0f, 0.0f, 0.0f);
        trans.GetComponent<UIPanel>().clipOffset = new Vector2(720.0f, 0.0f);
    }
}
