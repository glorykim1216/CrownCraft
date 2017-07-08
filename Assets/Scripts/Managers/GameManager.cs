using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    float mana;
    
    public float MANA
    {
        get { return mana; }
        set { mana = value; }
    }
    //bool IsInit = false;

    // Use this for initialization
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        mana += Time.deltaTime * 0.05f;
        UI_Manager.Instance.SetMana(mana);
    }

    public void DecreaseMana(float _cost)
    {
        mana -= _cost;
    }
}
