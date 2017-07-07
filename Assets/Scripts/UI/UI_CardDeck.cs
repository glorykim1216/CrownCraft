using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CardDeck : BaseObject
{
    public List<string> list = new List<string>();
    GameObject newCard = null;

    int CardCount = 0;

    void Start()
    {
        // List Shuffle
        for (int i = 0; i < 8; i++)
        {
        }
        list.Add(eActor.ARCHER.ToString());
        list.Add(eActor.DRAGON.ToString());
        list.Add(eActor.KNIGHT.ToString());
        list.Add(eActor.WIZARD.ToString());
        list.Add(eActor.CACTUS.ToString());
        list.Add(eActor.DEATHKNIGHT.ToString());
        list.Add(eActor.GHOST.ToString());
        list.Add(eActor.GOLEM.ToString());

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

        CreateCard(new Vector3(-140, -500, 0), list[0]);
        CreateCard(new Vector3(-15, -500, 0), list[1]);
        CreateCard(new Vector3(110, -500, 0), list[2]);
        CreateCard(new Vector3(240, -500, 0), list[3]);
        CardCount = 4;
        NextCard();
    }

    void Update()
    {

    }

    public GameObject CreateCard(Vector3 _orgPos, string _name)
    {
        // 프리펩 생성
        GameObject CardPrefab = Resources.Load("Prefabs/Card") as GameObject;

        newCard = NGUITools.AddChild(this.transform.gameObject, CardPrefab);
        newCard.GetComponent<CardDrag>().Init(_orgPos, _name);

        return newCard;
    }

    public void NextCard()
    {
        CreateCard(new Vector3(-290, -570, 0), list[CardCount]);

        if (CardCount < 7)
            CardCount++;
        else
            CardCount = 0;
    }

    public void MoveCard(Vector3 _DestPos)
    {
        GameObject moveCard = newCard;
        moveCard.GetComponent<CardDrag>().RePos(_DestPos);
        moveCard.GetComponent<UISprite>().depth = 3;

        StartCoroutine(Move(moveCard, _DestPos));
        NextCard();
    }

    IEnumerator Move(GameObject moveCard, Vector3 _DestPos)
    {
        yield return new WaitForSeconds(1);
        moveCard.GetComponent<UISprite>().depth = 2;

        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            if (time < 0.5f)
            {
                moveCard.transform.localPosition = Vector3.Lerp(moveCard.transform.localPosition, _DestPos, 0.3f);// Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                break;
            }
        }
    }


}
