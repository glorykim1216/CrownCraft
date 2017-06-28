using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : BaseObject // abstract 추상 클래스
{
    public BaseObject OWNER
    {
        get;
        set;
    }

    public BaseObject TARGET
    {
        get;
        set;
    }

    public SkillTemplate SKILL_TEAMPLATE
    {
        get;
        set;
    }

    public bool END
    {
        get;
        protected set;
    }

    abstract public void InitSkill(); // abstract 추상(순수가상 함수)
    abstract public void UpdateSkill();
}
