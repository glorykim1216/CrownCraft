using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public class CharacterTemplateData
{
    string StrKey = string.Empty;

    StatusData Status = new StatusData();
    List<string> ListSkill = new List<string>();

	string StrStatus = string.Empty;

    public string KEY { get { return StrKey; } }
    public StatusData STATUS { get { return Status; } }
    public List<string> LIST_SKILL { get { return ListSkill; } }
	public string STRSTATUS { get { return StrStatus; } }

    public CharacterTemplateData(string _strKey, JSONNode nodeData)
    {
        StrKey = _strKey;

        for (int i = 0; i < (int)eStatusData.MAX; i++)
        {
            eStatusData statusData = (eStatusData)i;
            double valueData = nodeData[statusData.ToString("F")].AsDouble;
            Status.IncreaseData(statusData, valueData);
        }

		for (int i =0; i<(int)eStatusDataStr.MAX; i++)
		{
			eStatusDataStr statusDataStr = (eStatusDataStr)i;
			string valueData = nodeData[statusDataStr.ToString("F")];
			Status.IncreaseData(statusDataStr, valueData);
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
