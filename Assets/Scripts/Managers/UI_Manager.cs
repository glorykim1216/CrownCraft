using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoSingleton<UI_Manager>
{
    UI_CardDeck CardDeckUI;
    UI_ManaBar manaBarUI;

    // Use this for initialization
    void Start()
    {
        CardDeckUI = transform.FindChild("PF_UI_CARDDECK").GetComponent<UI_CardDeck>();
        manaBarUI = CardDeckUI.transform.FindChild("UI_ManaBar").GetComponent<UI_ManaBar>();
    }

    public void SetMana(float mana)
    {
        if (manaBarUI == null)
            return;

        manaBarUI.SetValue(mana);
    }

    public void MoveCard(Vector3 _DestPos, string _name)
    {
        CardDeckUI.MoveCard(_DestPos, _name);
    }
}
