using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CardDeck : BaseObject
{
    List<string> cardDeckList = new List<string>();
    List<string> useCardList = new List<string>();

    GameObject newCard = null;
    Transform redZone = null;
    //int CardCount = 0;

    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
        }
        // 카드덱 추가
        cardDeckList = CardManager.Instance.GetPlayerDeck();
        //cardDeckList.Add(eActor.DRAGON.ToString());
 
        //cardDeckList.Add(eActor.DRAGON.ToString());
        //cardDeckList.Add(eActor.WIZARD.ToString());
        //cardDeckList.Add(eActor.BAT.ToString());
        //cardDeckList.Add(eActor.SKELETON.ToString());
        //cardDeckList.Add(eActor.DEATHKNIGHT.ToString());
        //cardDeckList.Add(eActor.GHOST.ToString());
        //cardDeckList.Add(eActor.BARBARIAN.ToString());

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

        redZone = FindInChild("RedZone");

        // 초기화
        CreateCard(new Vector3(-130, -500, 0), cardDeckList[0], true);
        CreateCard(new Vector3(-10, -500, 0), cardDeckList[0], true);
        CreateCard(new Vector3(110, -500, 0), cardDeckList[0], true);
        CreateCard(new Vector3(230, -500, 0), cardDeckList[0], true);
        CreateNextCard();
    }

    public GameObject CreateCard(Vector3 _orgPos, string _name, bool Init = false)
    {
        // 프리펩 생성
        GameObject CardPrefab = Resources.Load("Prefabs/Card") as GameObject;

        newCard = NGUITools.AddChild(this.transform.gameObject, CardPrefab);
        newCard.GetComponent<CardDrag>().Init(_orgPos, _name, redZone);

        cardDeckList.RemoveAt(0);

        // CoolTime 활성화 ( 처음 초기화 4장을 위해 )
        if (Init == true)
            newCard.GetComponent<CardDrag>().RePos(_orgPos);

        return newCard;
    }

    // 처음 초기화 할때만 사용
    public void CreateNextCard()
    {
        GameObject Nextcard = CreateCard(new Vector3(-290, -570, 0), cardDeckList[0]);
        Nextcard.transform.localScale = Vector3.one * 0.7f;
    }

    public void CreateNextCard(string _name)
    {
        GameObject Nextcard = CreateCard(new Vector3(-290, -570, 0), cardDeckList[0]);
        Nextcard.transform.localScale = Vector3.one * 0.7f;
        cardDeckList.Add(_name);
    }

    public void MoveCard(Vector3 _DestPos, string _name)
    {
        GameObject moveCard = newCard;
        moveCard.GetComponent<UISprite>().depth = 3;

        StartCoroutine(Move(moveCard, _DestPos));
        CreateNextCard(_name);
    }

    IEnumerator Move(GameObject moveCard, Vector3 _DestPos)
    {
        yield return new WaitForSeconds(1);
        moveCard.GetComponent<CardDrag>().RePos(_DestPos);
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
                moveCard.transform.localPosition = _DestPos;
                break;
            }
        }
    }


}
