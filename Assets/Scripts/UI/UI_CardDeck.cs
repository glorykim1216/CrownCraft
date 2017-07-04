using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CardDeck : BaseObject
{
    public Transform[] Cards = new Transform[4];

    public List<string> list = new List<string>();
    int CardCount = 0;

    void Start()
    {
        // List Shuffle
        for (int i = 0; i < 8; i++)
        {
        }
        list.Add(eActor.ARCHER.ToString());
        list.Add(eActor.BARBARIAN.ToString());
        list.Add(eActor.KNIGHT.ToString());
        list.Add(eActor.WIZARD.ToString());
        list.Add(eActor.ARCHER.ToString());
        list.Add(eActor.BARBARIAN.ToString());
        list.Add(eActor.WIZARD.ToString());
        list.Add(eActor.KNIGHT.ToString());

        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(0, list.Count);
            string temp = list[i];
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

        Transform trans = FindInChild("Cards");

        for (int i = 0; i < 4; i++)
        {
            Cards[i] = trans.FindChild("Card_" + i);
        }

        CreateCard(new Vector3(-140, -500, 0), list[0]);
    }

    void Update()
    {

    }

    public void NextCard()
    {
        
    }

    public void CreateCard(Vector3 _orgPos, string _name)
    {
        // 프리펩 생성
        GameObject newCardUI = Resources.Load("Prefabs/Card") as GameObject;
        NGUITools.AddChild(this.transform.gameObject, newCardUI);

        newCardUI.GetComponent<CardDrag>().Init(_orgPos, _name);
        //go.GetComponent<Actor>().TEAM_TYPE = eTeamType.TEAM_2;
    }
}
