using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    List<int> list = new List<int>();
    float mana;
    // Use this for initialization
    void Start()
    {
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

        // 드레그드롭하면 UIManager호출->
        // 없어지고(숨기고)
        // Next카드 날라오고(이미지 Next로 바꿈, 다시 Next카드 이미지 바뀌고)
        // 다시 생기고(숨긴거 켜줌)
    }

    // Update is called once per frame
    void Update()
    {
        mana += Time.deltaTime * 0.05f;
        UI_Manager.Instance.SetMana(mana);
    }
}
