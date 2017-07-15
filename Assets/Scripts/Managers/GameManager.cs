using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    bool IsGameOver = false;
    public bool GAME_OVER { get { return IsGameOver; } }

    float mana = 0.5f;
    public float MANA
    {
        get { return mana; }
        set
        {
            mana = value;
            if (mana > 10)
                mana = 10;
        }
    }
    //bool IsInit = false;

    public override void Init()
    {
    }

    // Use this for initialization
    void Start()
    {
        IsGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        MANA += Time.deltaTime * 0.05f;
        UI_Manager.Instance.SetMana(MANA);
    }

    public void DecreaseMana(float _cost)
    {
        mana -= _cost;
    }
}
