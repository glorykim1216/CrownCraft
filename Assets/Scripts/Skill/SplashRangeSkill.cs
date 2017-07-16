using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashRangeSkill : BaseSkill
{
    GameObject ModelPrefab;
    float StackTime = 0;

    public override void InitSkill()
    {
        if (ModelPrefab == null)
            return;

        GameObject model = Instantiate(ModelPrefab, Vector3.zero, Quaternion.Euler(-90, 0, 0));
        model.transform.SetParent(this.transform, false);
    }

    public override void UpdateSkill()
    {
        StackTime += Time.deltaTime;
        if (StackTime >= 1f)
        {
            END = true;
        }

        //if (TARGET == null)
        //{
        //    END = true;
        //    return;
        //}
        if (TARGET != null)
        {
            Vector3 targetPosition = SelfTransform.position + (TARGET.SelfTransform.position - SelfTransform.position).normalized * 5 * Time.deltaTime;
            SelfTransform.position = targetPosition;
        }
    }

    public override void ThrowEvent(string keyData, params object[] datas)
    {
        if (keyData == ConstValue.EventKey_SelectModel)
        {
            ModelPrefab = datas[0] as GameObject;
        }
        else
            base.ThrowEvent(keyData, datas);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (END == true)
            return;

        GameObject collObject = other.gameObject;
        BaseObject actorObject = collObject.GetComponent<BaseObject>();

        if (actorObject.GetComponent<Actor>().TEAM_TYPE == OWNER.GetComponent<Actor>().TEAM_TYPE)
            return;
        if (actorObject == OWNER)
            return;

        //if (actorObject != TARGET)
        //    return; // 여기 없으면 충돌한거 다 대미지

        actorObject.ThrowEvent(ConstValue.EventKey_Hit, OWNER.GetData(ConstValue.ActorData_Character), SKILL_TEAMPLATE);
        //END = true;
    }
}
