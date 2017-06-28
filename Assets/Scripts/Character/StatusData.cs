using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusData
{
    Dictionary<eStatusData, double> DicData = new Dictionary<eStatusData, double>();

    public void InitData()
    {
        DicData.Clear();
    }

    public void Copy(StatusData data)
    {
        foreach (KeyValuePair<eStatusData, double> pair in data.DicData)
        {
            IncreaseData(pair.Key, pair.Value);
        }
    }

    public void IncreaseData(eStatusData statusData, double valueData)
    {
        double preValue = 0.0;  // 다음라인에서 키값이 없으면 아무것도 반환하지 않아서 초기화 필수!
        DicData.TryGetValue(statusData, out preValue); // out -> c++ 에서 참조(ref)
        DicData[statusData] = preValue + valueData;
    }

    public void DecreaseData(eStatusData statusData, double valueData)
    {
        double preValue = 0.0;
        DicData.TryGetValue(statusData, out preValue);
        DicData[statusData] = preValue - valueData;
    }

    public void SetData(eStatusData statusData, double valueData)
    {
        DicData[statusData] = valueData;
    }

    public void RemoveData(eStatusData statusData, double valueData)
    {
        if (DicData.ContainsKey(statusData) == true)
            DicData.Remove(statusData);
    }

    public double GetStatusData(eStatusData statusData)
    {
        double preValue = 0.0;
        DicData.TryGetValue(statusData, out preValue);
        return preValue;
    }

    public string StatusString()
    {
        string returnStr = string.Empty;

        foreach (var pair in DicData)
        {
            returnStr += pair.Key.ToString();
            returnStr += " " + pair.Value.ToString();
            returnStr += "\n";
        }
        return returnStr;
    }
}
