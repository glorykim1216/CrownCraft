﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public class CharacterTemplateData
{
    string StrKey = string.Empty;

    StatusData Status = new StatusData();
    List<string> ListSkill = new List<string>();


	string CardTarget = string.Empty;
	string CardTransport = string.Empty;
	string CardContents = string.Empty;

    public string KEY { get { return StrKey; } }
    public string CARDTARGET { get { return CardTarget; } }
    public string CARDTRANSPORT { get { return CardTransport; } }
	public string CARDCONTENTS { get { return CardContents; } }

    public StatusData STATUS { get { return Status; } }
    public List<string> LIST_SKILL { get { return ListSkill; } }

    public CharacterTemplateData(string _strKey, JSONNode nodeData)
    {
        StrKey = _strKey;

        CardTarget = nodeData["TARGET"];
        CardTransport = nodeData["TRANSPORT"];
		CardContents = nodeData["CONTENTS"];

		for (int i = 0; i < (int)eStatusData.MAX; i++)
        {
            eStatusData statusData = (eStatusData)i;
            double valueData = nodeData[statusData.ToString("F")].AsDouble;
            Status.IncreaseData(statusData, valueData);
        }
		
        JSONArray arrSkill = nodeData["SKILL"].AsArray;  // 쉼표를 넣으면 배열로 저장
        if (arrSkill != null)
        {
            for (int i = 0; i < arrSkill.Count; i++)
            {
                ListSkill.Add(arrSkill[i]);
            }
        }
    }
}
