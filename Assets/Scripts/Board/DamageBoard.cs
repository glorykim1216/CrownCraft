using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 커브 애니메이션 적용시 데미지 텍스트를 날라가게 효과 줄 수 있음.
public class DamageBoard : BaseBoard
{
    [SerializeField]
    UILabel DamageLabel = null;

    public override eBoardType BOARD_TYPE
    {
        get
        {
            return eBoardType.BOARD_DAMAGE;
        }
    }

    public override void SetData(string strKey, params object[] datas)
    {
        if (strKey.Equals(ConstValue.SetData_Damage))
        {
            double damage = (double)datas[0];
            DamageLabel.text = damage.ToString();

            base.UpdateBaord(); // 한번만 실행 -> 초기 위치 설정
        }
    }

    public override void UpdateBaord()
    {
        // CheckDestroyTime() 
        CurrTime += Time.deltaTime;
        transform.position += Vector3.up * Time.deltaTime * 0.5f;
    }
}
