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
            if (mana > 1)
                mana = 1;
        }
    }
    //bool IsInit = false;

    public int EnemyTowerDestroyCount = 0;
    public int PlayerTowerDestroyCount = 0;

    // Use this for initialization
    void Start()
    {
        IsGameOver = false;
    }
    public void LoadGame()
    {
        IsGameOver = false;
        mana = 0.5f;
        EnemyTowerDestroyCount = 0;
        PlayerTowerDestroyCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsGameOver == true)
            return;

        if (EnemyTowerDestroyCount >= 3 || PlayerTowerDestroyCount >= 3)
        {
            IsGameOver = true;
            Debug.Log("끝");
            UI_Manager.Instance.LoadGameOverUI(EnemyTowerDestroyCount, PlayerTowerDestroyCount);
        }

        MANA += Time.deltaTime * 0.05f;
        UI_Manager.Instance.SetMana(MANA);
    }

    public void DecreaseMana(float _cost)
    {
        mana -= _cost;
    }
}
