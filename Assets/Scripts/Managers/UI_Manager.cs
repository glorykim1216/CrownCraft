using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoSingleton<UI_Manager>
{
    UI_CardDeck CardDeckUI;
    UI_ManaBar manaBarUI;

    public UILabel RedLabel;
    public UILabel BlueLabel;
    public UILabel Time;

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

    public void ScoreUpdate(int red, int blue)
    {
        RedLabel.text = red.ToString();
        BlueLabel.text = blue.ToString();
    }

    public void LimitTime(float _time)
    {
        int minute = (int)_time/60;
        int second = (int)_time%60;

        Time.text = string.Format("{0:00} : {1:00}", minute, second);
    }
}
