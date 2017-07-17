using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoSingleton<UI_Manager>
{
    UI_CardDeck CardDeckUI;
    UI_ManaBar manaBarUI;

    public override void Init()
    {
    }

    public void Load()
    {
        CardDeckUI = transform.FindChild("PF_UI_CARDDECK").GetComponent<UI_CardDeck>();
        manaBarUI = CardDeckUI.transform.FindChild("UI_ManaBar").GetComponent<UI_ManaBar>();
    }
    void Start()
    {
        
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

    public void LoadGameOverUI(int EnemyTowerDestroyCount, int PlayerTowerDestroyCount)
    {
        GameObject GameOverUI = UI_Tools.Instance.ShowUI(eUIType.PF_UI_GAMEOVER);
        GameOverUI.GetComponent<UI_GameOver>().Result(EnemyTowerDestroyCount, PlayerTowerDestroyCount);
    }
}
