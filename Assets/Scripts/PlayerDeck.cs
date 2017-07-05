﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
	List<string> playerDeck = new List<string>();
	public List<string> PLAYERDECK
	{
		get { return playerDeck; }
	}

	public void AddCard(string CardKey)
	{
		playerDeck.Add(CardKey);
	}

	public void DeleteCard(string CardKey)
	{
		for (int i = 0; i < playerDeck.Count; i++)
		{
			if (playerDeck[i] == CardKey)
			{
				playerDeck.Remove(CardKey);
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

	public void GetLocalData()
	{
		string instanceStr = PlayerPrefs.GetString(ConstValue.LocalSave_DeckInstance, string.Empty);

		string[] array = instanceStr.Split('|');

		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Length <= 0)
				continue;

			AddCard(array[i]);
		}
	}
	public void ReplaceDeck(string CardKey)
	{
		for(int i=0; i<playerDeck.Count;i++		)
		{

		}

	}


}
