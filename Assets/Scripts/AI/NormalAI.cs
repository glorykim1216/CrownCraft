using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAI : BaseAI
{
    Rigidbody rigid;
    float movePath = -4.1f;
    float straightRotate = 0;
    public float pathRotate = 45f;

    public bool bDeadEnd = false;
    int team;
    public void Start()
    {
        rigid = transform.parent.GetComponent<Rigidbody>();
    }

    public void Team(int _team)
    {
        team = _team;
        if (team == 1)
        {
            straightRotate = 0;
            pathRotate = 45;
        }
        else
        {
            straightRotate = 180;
            pathRotate = 135;
        }
    }

    public void SetMovePath(Vector3 _pos)
    {
        if (_pos.x > 0)
            movePath = 4.1f;
        else
            movePath = -4.1f;
    }

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
            SkillData sData = Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;

            float attackRange = 2f;

            // 스킬 데이터 적용
            if (sData != null)
                attackRange = sData.RANGE;


            // 거리검사*
            float distance = Vector3.Distance(targetObject.SelfTransform.position, SelfTransform.position);

            // 공격
            if (distance < attackRange)
            {
                //Stop();
                rigid.Sleep();
                AddNextAI(eStateType.STATE_ATTACK, targetObject);
            }
            // 이동
            else
            {
                AddNextAI(eStateType.STATE_WALK);
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
            SkillData sData = Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;

            float attackRange = 1.5f;
            float searchRange = 5f;

            //스킬 데이터 적용
            if (sData != null)
                attackRange = sData.RANGE;

            float distance = Vector3.Distance(targetObject.SelfTransform.position, SelfTransform.position);

            if (distance < attackRange)
            {
                //Stop();
                rigid.Sleep();
                AddNextAI(eStateType.STATE_ATTACK, targetObject);
            }
            else if (distance < searchRange && bDeadEnd == false)
            {
                Run();
                TurnToTarget(targetObject.SelfTransform.position);
            }
            else
            {
                Run();
                Turn();
            }
        }

        yield return StartCoroutine(base.Move());

    }

    void Run()
    {
        if (team == 1)
            rigid.MovePosition(rigid.position + transform.forward * Time.deltaTime);
        else
            rigid.MovePosition(rigid.position - transform.forward * Time.deltaTime);
        StartCoroutine(VelocityZero());
    }
    void Turn()
    {
        float movePathDistance = transform.parent.position.x - movePath;
        //if (Vector3.Distance(transform.position, Line.transform.position) > 0)
        if (movePathDistance > -0.5f && movePathDistance < 0.5f)
        {
            rigid.rotation = Quaternion.Slerp(rigid.rotation, Quaternion.Euler(0, straightRotate, 0), 2 * Time.deltaTime);
            if (bDeadEnd == true)
                bDeadEnd = false;
        }
        else if (movePathDistance > 0)
        {
            rigid.rotation = Quaternion.Slerp(rigid.rotation, Quaternion.Euler(0, -pathRotate, 0), 2 * Time.deltaTime);
        }
        else
            rigid.rotation = Quaternion.Slerp(rigid.rotation, Quaternion.Euler(0, pathRotate, 0), 2 * Time.deltaTime);
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
        //IS_ATTACK = true;
        //bIsAttack = true;

        //AddNextAI(eStateType.STATE_IDLE);

        yield return StartCoroutine(base.Attack());
    }

    protected override IEnumerator Die()
    {
        END = true;
        yield return StartCoroutine(base.Die());
    }

    IEnumerator VelocityZero()
    {
        if (rigid.velocity != Vector3.zero)
        {
            yield return new WaitForSeconds(0.5f);
            rigid.velocity = Vector3.zero;
        }
    }
}
