using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacter
{
    public BaseObject TargetComponenet = null;                          // 관리대상( Actor )
    
    CharacterTemplateData TemplateData = null;                          // 원본 Status( 수정X )

    CharacterStatusData CharacterStatus = new CharacterStatusData();    // 최신화 데이터

    public CharacterTemplateData CHARACTER_TEMPLATE
    { get { return TemplateData; } }

    public CharacterStatusData CHARACTER_STATUS
    { get { return CharacterStatus; } }

    double CurrentHP = 0;
    public double CURRENT_HP
    { get { return CurrentHP; } }

    // 스킬 데이터
    public SkillData SELECT_SKILL
    {
        get;
        set;
    }
    List<SkillData> listSkill = new List<SkillData>();


    public void IncreaseCurrentHP(double valueData)
    {
        CurrentHP += valueData;
        if (CurrentHP < 0)
            CurrentHP = 0;

        double maxHP = CharacterStatus.GetStatusData(eStatusData.HP);
        if (CurrentHP > maxHP)
            CurrentHP = maxHP;

        if (CurrentHP == 0)
            TargetComponenet.OBJECT_STATE = eBaseObjectState.STATE_DIE;

        Debug.Log(CurrentHP);
    }

    public void SetTemplate(CharacterTemplateData _templateData)
    {
        TemplateData = _templateData;
        CharacterStatus.AddStatusData(ConstValue.CharacterStatusDataKey, TemplateData.STATUS);
        CurrentHP = CharacterStatus.GetStatusData(eStatusData.HP);
    }

    public void AddSkill(SkillData skillData)
    {
        listSkill.Add(skillData);
    }

    public SkillData GetSkillByIndex(int index)
    {
        if(listSkill.Count > index)
        {
            return listSkill[index];
        }
        return null;
    }
}
