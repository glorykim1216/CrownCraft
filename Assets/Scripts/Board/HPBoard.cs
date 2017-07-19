using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBoard : BaseBoard
{
    // [SerializeField] -> Force Unity to serialize a private field.
    // private 변수 유니티 강제 동기화 ( 인스펙터 동기화 )

    [SerializeField]
    UIProgressBar ProgressBar = null;
    [SerializeField]
    UILabel HPLabel = null;

    public override eBoardType BOARD_TYPE
    {
        get
        {
            return eBoardType.BOARD_HP;
        }
    }

    public override void SetData(string strKey, params object[] datas)
    {
        if(strKey.Equals(ConstValue.SetData_HP))
        {
            double MaxHP = (double)datas[0];
            double CurrHP = (double)datas[1];

            ProgressBar.value = (float)(CurrHP / MaxHP);                    // (CurrHP / MaxHP) -> 퍼센트(0 ~ 1)
            //HPLabel.text = CurrHP.ToString() + " / " + MaxHP.ToString();
        }
    }

    
}
