using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill : BaseSkill
{
    float StackTime = 0;
    public override void InitSkill()
    {

    }

    public override void UpdateSkill()
    {
        StackTime += Time.deltaTime;
        if(StackTime>=0.1f)
        {
            END = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (END == true)
            return;

        GameObject collObject = other.gameObject;
        BaseObject actorObject = collObject.GetComponent<BaseObject>();

        //if (actorObject.GetComponent<Actor>().TEAM_TYPE == OWNER.GetComponent<Actor>().TEAM_TYPE)
        //    return;
        //if (actorObject == OWNER)
        //    return;

        if (actorObject != TARGET)
            return; // 여기 없으면 충돌한거 다 대미지

        TARGET.ThrowEvent(ConstValue.EventKey_Hit, OWNER.GetData(ConstValue.ActorData_Character), SKILL_TEAMPLATE, collObject);
    }
}
