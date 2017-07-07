using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CardDeck : BaseObject
{
    List<string> cardDeckList = new List<string>();
    List<string> useCardList = new List<string>();
    
    GameObject newCard = null;

    int CardCount = 0;

    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
        }
        // 카드덱 추가
        cardDeckList.Add(eActor.ARCHER.ToString());
        cardDeckList.Add(eActor.DRAGON.ToString());
        cardDeckList.Add(eActor.KNIGHT.ToString());
        cardDeckList.Add(eActor.WIZARD.ToString());
        cardDeckList.Add(eActor.CACTUS.ToString());
        cardDeckList.Add(eActor.DEATHKNIGHT.ToString());
        cardDeckList.Add(eActor.GHOST.ToString());
        cardDeckList.Add(eActor.GOLEM.ToString());

        // List Shuffle
        for (int i = 0; i < cardDeckList.Count; i++)
        {
            int rand = Random.Range(0, cardDeckList.Count);
            string temp = cardDeckList[i];
            cardDeckList[i] = cardDeckList[rand];
            cardDeckList[rand] = temp;
        }

        // testCode
        for (int i = 0; i < cardDeckList.Count; i++)
        {
            Debug.Log(cardDeckList[i]);
        }

        // 초기화
        CreateCard(new Vector3(-140, -500, 0), cardDeckList[0]);
        CreateCard(new Vector3(-15, -500, 0), cardDeckList[1]);
        CreateCard(new Vector3(110, -500, 0), cardDeckList[2]);
        CreateCard(new Vector3(240, -500, 0), cardDeckList[3]);

        CardCount = 4;
        NextCard();
    }



    public GameObject CreateCard(Vector3 _orgPos, string _name)
    {
        // 사용중인 카드 추가
        for (int i = 0; i < useCardList.Count; i++)
        {
            if (useCardList[i].Equals(_name))
                continue;
            else
                useCardList.Add(_name);
        }

        // 프리펩 생성
        GameObject CardPrefab = Resources.Load("Prefabs/Card") as GameObject;

        newCard = NGUITools.AddChild(this.transform.gameObject, CardPrefab);
        newCard.GetComponent<CardDrag>().Init(_orgPos, _name);

        return newCard;
    }

    public void NextCard()
    {
        CreateCard(new Vector3(-290, -570, 0), cardDeckList[CardCount]);

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
