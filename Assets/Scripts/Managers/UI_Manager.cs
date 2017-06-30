using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoSingleton<UI_Manager>
{
    UI_CardDeck CardDeckUI;
    UI_ManaBar manaBarUI;

    public List<int> list = new List<int>();
    int CardCount = 0;

    // Use this for initialization
    void Start()
    {
        CardDeckUI = transform.FindChild("PF_UI_CARDDECK").GetComponent<UI_CardDeck>();
        manaBarUI = transform.FindChild("UI_ManaBar").GetComponent<UI_ManaBar>();

        // List Shuffle
        for (int i = 0; i < 8; i++)
        {
            list.Add(i);
        }

        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(0, list.Count);
            int temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }

        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i]);
        }

        CardCount++;
        // 드레그드롭하면 UIManager호출->
        // 없어지고(숨기고)
        // Next카드 날라오고(이미지 Next로 바꿈, 다시 Next카드 이미지 바뀌고)
        // 다시 생기고(숨긴거 켜줌)
    }

    public void SetMana(float mana)
    {
        if (manaBarUI == null)
            return;

        manaBarUI.SetValue(mana);
    }

    public void ChangeCard()
    {
        //CardDeckUI.Cards[num].FindChild("CardImage").GetComponent;
    }
}
