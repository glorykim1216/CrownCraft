using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleAI : BaseAI
{
    protected override IEnumerator Idle()
    {
        // 근거리 대상 검색
        BaseObject targetObject = ActorManager.Instance.GetSearchEnemy(Target);

        ////----------------------------------------------------------------------------------------
        //// Test Code

        //// out 매개변수 사용해서 targetDistance값 까지 한번에 가져옴 -> 아래서 (거리검사*)를 할 필요 없음
        //float targetDistance = 0;
        //BaseObject targetObject_Test = ActorManager.Instance.GetSearchEnemy(Target, out targetDistance);
        ////----------------------------------------------------------------------------------------


        // 공격 범위 < 거리
        if (targetObject != null)
        {
            //SkillData sData = Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;

            //// 스킬 데이터 적용
            //if (sData != null)
            //    attackRange = sData.RANGE;

            float attackRange = 5f;
            float searchRange = 5f;

            // 거리검사*
            float distance = Vector3.Distance(targetObject.SelfTransform.position, SelfTransform.position);

            // 공격
            if (distance < attackRange)
            {
                Stop();
                AddNextAI(eStateType.STATE_ATTACK, targetObject);
            }
            else if (distance < searchRange)
            {
                TurnToTarget(targetObject.SelfTransform.position);
            }
        }

        // yield return StartCoroutine() -> 코루틴이 끝날때까지 기다림.
        yield return StartCoroutine(base.Idle());

    }

    protected override IEnumerator Move()
    {
        BaseObject targetObject = ActorManager.Instance.GetSearchEnemy(Target);

        if (targetObject != null)
        {
            //SkillData sData = Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;

            // 스킬 데이터 적용
            //if (sData != null)
            //    attackRange = sData.RANGE;

            float attackRange = 5f;
            float searchRange = 10f;
            float distance = Vector3.Distance(targetObject.SelfTransform.position, SelfTransform.position);

            if (distance < attackRange)
            {
                Stop();
                AddNextAI(eStateType.STATE_ATTACK, targetObject);
            }
            else if (distance < searchRange)
            {
                TurnToTarget(targetObject.SelfTransform.position);
            }
        }

        yield return StartCoroutine(base.Move());

    }


    protected override IEnumerator Attack()
    {
        //yield return new WaitForEndOfFrame();

        while (IS_ATTACK)
        {
            if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
                break;
            yield return new WaitForEndOfFrame();
        }

        //AddNextAI(eStateType.STATE_IDLE);

        yield return StartCoroutine(base.Attack());
    }

    protected override IEnumerator Die()
    {
        END = true;
        yield return StartCoroutine(base.Die());
    }
}
