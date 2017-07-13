using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoSingleton<CardManager>
{
	bool IsInit = false;

	List<string> playerDeck = new List<string>();
	List<string> TotalCardList = new List<string>();

	public List<string> PLAYERDECK
	{
		get { return playerDeck; }
	}

	Dictionary<string, bool> DicCardList = new Dictionary<string, bool>();

	bool IsUse = false;

	private void Awake()
	{
		bool CardNULL = DeckInit();
		if (CardNULL == true)
			TotalCardInit();
	}

	void TotalCardInit()
	{
		for (int i = 0; i < (int)eActor.MAX; i++)
		{
			playerDeck.Add(((eActor)i).ToString());
		}
		// 플레이어 덱 초기화
	}



	public bool DeckInit()
	{
		if (IsInit == true)
			return true;

		print("덱정보 초기화");

		IsInit = true;
		return GetLocalData();

	}

	public List<string> GetPlayerDeck()
	{
		List<string> tempList = new List<string>();
		for (int i = 0; i < 8; i++)
		{
			tempList.Add(playerDeck[i]);
		}

		return tempList;
	}

	public bool GetLocalData()
	{
		string instanceStr = PlayerPrefs.GetString(ConstValue.LocalSave_DeckInstance, string.Empty);
		string[] array = instanceStr.Split('|');
		Debug.Log(instanceStr);

		if (array[0].Equals(""))
			return true;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Length <= 0)
				continue;

			playerDeck.Add(array[i]);
		}


		return false;
	}

	public void SetCard(int CardIndex, string CardKey)
	{
		playerDeck[CardIndex] = CardKey;

		SetLocalData();
	}
	public List<string> SetCardCollection()
	{
		List<string> templist = new List<string>();
		foreach (KeyValuePair<string, bool> pair in DicCardList)
		{
			if (pair.Value == false)
			{
				templist.Add(pair.Key);
			}
		}

		return templist;
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

		for (int i = 0; i < ConstValue.PlayerDeck_Size; i++)
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
