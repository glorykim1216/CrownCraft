using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class CardInfo : MonoBehaviour
{
	string StrKey = string.Empty;
	int CardLevel = -1;
	int CardHP = -1;
	int CardAttack = -1;
	int CardCost = -1;
	double CardSpeed = -1;
	int CardRange = -1;
	string CardTarget = string.Empty;
	int CardCount = -1;
	string CardTransport = string.Empty;
	string CardType = string.Empty;

	public string KEY { get { return StrKey; } } 
	public int LEVEL { get { return CardLevel; } }
	public int HP { get { return CardHP; } }
	public int ATTACK { get { return CardAttack; } }
	public int COST { get { return CardCost; } }
	public double SPEED { get { return CardSpeed; } }
	public int RANGE { get { return CardRange; } }
	public string TARGET { get { return CardTarget; } }
	public int COUNT { get { return CardCount; } }
	public string TRANSPORT { get { return CardTransport; } }
	public string TYPE { get { return CardType; } }

	public CardInfo(string _strKey , JSONNode nodeData	)
	{
		StrKey = _strKey;
		CardLevel = nodeData["LEVEL"].AsInt;
		CardHP = nodeData["HP"].AsInt;
		CardAttack = nodeData["ATTACK"].AsInt;
		CardCost = nodeData["COST"].AsInt;
		CardSpeed = nodeData["SPEED"].AsDouble;
		CardRange = nodeData["RANGE"].AsInt;
		CardTarget = nodeData["TARGET"];
		CardCount = nodeData["COUNT"].AsInt;
		CardTransport = nodeData["TRANSPORT"];
		CardType = nodeData["TYPE"];
	}
	//public string GetSlotString()
	//{
	//	string returnStr = string.Empty;
	//	returnStr = Slot
	//}
}
