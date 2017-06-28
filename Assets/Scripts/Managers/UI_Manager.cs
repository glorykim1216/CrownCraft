using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoSingleton<UI_Manager>
{
    UI_ManaBar manaBarUI;
    // Use this for initialization
    void Start()
    {
         manaBarUI = transform.FindChild("PF_UI_CARDDECK/ManaBar").GetComponent<UI_ManaBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMana(float mana)
    {
        if (manaBarUI == null)
            return;

        manaBarUI.SetValue(mana);
    }
}
