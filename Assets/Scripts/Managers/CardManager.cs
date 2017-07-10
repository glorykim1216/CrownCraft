using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoSingleton<CardManager>
{
	bool IsInit = false;
	[SerializeField]
	List<string> playerDeck = new List<string>();
	public int DeckSize = 8;
	
	public List<string> PLAYERDECK
	{
		get { return playerDeck; }
	}

	private void Start()
	{
		DeckInit();
	}

	public void DeckInit()
	{
		if (IsInit == true)
			return;

		//테스트 코드
		print("덱정보 초기화");



		//GetLocalData();
		AddCard("KNIGHT");
		AddCard("WIZARD");
		AddCard("ARCHER");
		AddCard("");
		AddCard("");
		AddCard("");
		AddCard("");
		AddCard("");

		IsInit = true;

	}

	public void GetLocalData()
	{
		string instanceStr = PlayerPrefs.GetString(ConstValue.LocalSave_DeckInstance, string.Empty);
		string[] array = instanceStr.Split('|');

		for (int i = 0; i < DeckSize; i++)
		{
			AddCard("");
		}

		print(array.Length);

		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Length <= 0)
				continue;
			SetCard(i, array[i]);
		}
	}


	public void AddCard(string CardKey)
	{
		playerDeck.Add(CardKey);
		//for (int i = 0; i < playerDeck.Count; i++)
		//{
		//	if (playerDeck[i] == CardKey)
		//	{
		//		playerDeck.Add(CardKey);
		//	}
		//}
	}

	public void SetCard(int CardIndex, string CardKey)
	{		
		playerDeck[CardIndex] = CardKey;		
	}

	public string GetCardKey(int CardIndex)
	{
		if (playerDeck.Count < CardIndex)
			return "";
		else
			return playerDeck[CardIndex];
	}

	public void DeleteCard(string CardKey)
	{
		for (int i = 0; i < playerDeck.Count; i++)
		{
			if (playerDeck[i] == CardKey)
			{
				playerDeck.RemoveAt(i);
			}
		}
	}
	public void SetLocalData()
	{
		string resultStr = string.Empty;

		for (int i = 0; i < playerDeck.Count; i++)
		{
			string DeckStr = string.Empty;
			DeckStr += playerDeck[i].ToString();

			if (i != playerDeck.Count - 1)
				DeckStr += "|";

			resultStr += DeckStr;
		}
		PlayerPrefs.SetString(ConstValue.LocalSave_DeckInstance, resultStr);
		Debug.Log(resultStr);
	}



}
