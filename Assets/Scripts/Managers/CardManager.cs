using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoSingleton<CardManager>
{
	bool IsInit = false;

	List<string> playerDeck = new List<string>();
	//List<int> CardLevel = new List<int>();

	public List<string> PLAYERDECK
	{
		get { return playerDeck; }
	}

	Dictionary<string, int> DicCardLevel = new Dictionary<string, int>();

	public Dictionary<string, int> DIC_CARDLEVEL
	{
		get { return DicCardLevel; }
	}


	bool IsUse = false;

	private void Awake()
	{
		bool IsCardNull = DeckInit();
		if (IsCardNull == true)
			TotalCardInit();
	}

	public override void Init()
	{
	}
	void TotalCardInit()
	{
		for (int i = 0; i < (int)eActor.MAX; i++)
		{
			playerDeck.Add(((eActor)i).ToString());
			DicCardLevel.Add(playerDeck[i], 1);
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
	public int  GetCardLevel(int index)
	{
		int tempLevel = 0;

		return tempLevel;
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
			string[] detail = array[i].Split('_');

			if (detail.Length < 2)
				continue;

			playerDeck.Add(detail[0]);


			DicCardLevel.Add(detail[0],int.Parse( detail[1]));		//	TryGetValue(detail[0],out tempLevel);		//	(int.Parse(detail[1]));

		}


		return false;
	}
	public void SetLocalData()
	{
		string resultStr = string.Empty;

		for (int i = 0; i < ConstValue.PlayerDeck_Size; i++)
		{
			string DeckStr = string.Empty;
			DeckStr += playerDeck[i].ToString() + "_";
			int tempLevel = 0;
			DicCardLevel.TryGetValue(playerDeck[i].ToString(), out tempLevel);
			DeckStr += tempLevel;

			if (i != playerDeck.Count - 1)
				DeckStr += "|";

			resultStr += DeckStr;
		}
		PlayerPrefs.SetString(ConstValue.LocalSave_DeckInstance, resultStr);
		Debug.Log(resultStr);
	}

	public void Gacha()
	{
		string tempCardKey = null;
		int tempInt = 0;
		
		int no = Random.Range(0, DicCardLevel.Count );
		tempCardKey = ((eActor)no).ToString();

		DicCardLevel.TryGetValue(tempCardKey, out tempInt);
		tempInt++;
		DicCardLevel[tempCardKey] = tempInt;
		SetLocalData();

		//// 가챠 UI
		
		GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_GACHA);
		UI_Gacha popup = go.GetComponent<UI_Gacha>();
		
		popup.Init(tempCardKey);

		popup.transform.SetParent(GameObject.Find("GachaGround").transform);


		//go.GetComponent<UI_Gacha>().Init(instance);
	}

	public void SetCard(int CardIndex, string CardKey)
	{
		playerDeck[CardIndex] = CardKey;

		SetLocalData();
	}
	//public List<string> SetCardCollection()
	//{
	//	List<string> templist = new List<string>();
	//	foreach (KeyValuePair<string, int> pair in DicCardLevel)
	//	{
	//		if (pair.Value == 0)
	//		{
	//			templist.Add(pair.Key);
	//		}
	//	}

	//	return templist;
	//}


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


}
