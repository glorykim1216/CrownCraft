using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NextAI
{
    public eStateType StateType;
    public BaseObject TargetObject;
    public Vector3 Position;
}

public class BaseAI : BaseObject
{
    protected List<NextAI> ListNextAI = new List<NextAI>();
    protected eStateType CurrentAISatae = eStateType.STATE_IDLE;
    public eStateType CURRENT_AI_STATE
    {
        get { return CurrentAISatae; }
    }

    bool bUpdataAI = false;     // 처음에 bUpdataAI를 true로
    bool bAttack = false;
    public bool IS_ATTACK
    {
        get { return bAttack; }
        set { bAttack = value; }
    }

    bool bEnd = false;          // AI를 삭제할때 사용
    public bool END
    {
        get { return bEnd; }
        set { bEnd = value; }
    }

    protected Vector3 MovePosition = Vector3.zero;
    protected Vector3 PreMovePosition = Vector3.zero;             // 현재위치와 이전위치를 비교해서 위치가 같으면 이동실행 X

    Animator Anim = null;
    //NavMeshAgent NavAgent = null;

    public Animator ANIMATOR
    {
        get
        {
            if (Anim == null)
            {
                Anim = SelfObject.GetComponentInChildren<Animator>(); // SelfObject -> 타겟을 넣어주면, 타겟(Actor)의 자식의 Animator에 접근
            }
            return Anim;
        }
    }

    protected bool bIsAttack = false;

    //public NavMeshAgent NAV_MESH_AGENT
    //{
    //    get
    //    {
    //        if (NavAgent == null)
    //        {
    //            NavAgent = SelfObject.GetComponent<NavMeshAgent>();
    //        }
    //        return NavAgent;
    //    }
    //}

    void ChangeAnimation()
    {
        if (ANIMATOR == null)
        {
            Debug.LogError(SelfObject.name + "에 Animator가 없습니다.");
            return;
        }

        ANIMATOR.SetInteger("State", (int)CurrentAISatae);
    }

    public virtual void AddNextAI(eStateType nextStatType, BaseObject targetObject = null, Vector3 position = new Vector3())
    {
        NextAI nextAI = new NextAI();
        nextAI.StateType = nextStatType;
        nextAI.TargetObject = targetObject;
        nextAI.Position = position;

        ListNextAI.Add(nextAI);

    }

    protected virtual void ProcessIdle()    // 전처리 과정
    {
        CurrentAISatae = eStateType.STATE_IDLE;
        ChangeAnimation();
    }

    protected virtual void ProcessMove()
    {
        CurrentAISatae = eStateType.STATE_WALK;
        ChangeAnimation();
    }

    // 스킬
    protected virtual void ProcessAttack()
    {
        Target.ThrowEvent(ConstValue.EventKey_SelectSkill, 0);  // 캐릭터 스킬중에 0번째꺼 사용 ( 1.여기서 스킬을 선택하거나, 2.ProcessAttack()함수를 여러개 만들어서 사용. )
        CurrentAISatae = eStateType.STATE_ATTACK;
        ChangeAnimation();
    }

    protected virtual void ProcessDie()
    {
        CurrentAISatae = eStateType.STATE_DEAD;
        ChangeAnimation();
    }

    protected virtual IEnumerator Idle()    // 순수가상이 아닌이유 -> override 함수 실행 후 부모 함수 실행
    {
        bUpdataAI = false;
        yield break;
    }

    protected virtual IEnumerator Move()
    {
        bUpdataAI = false;
        yield break;
    }

    protected virtual IEnumerator Attack()
    {
        AnimationEnd();
        bUpdataAI = false;
        yield break;
    }

    protected virtual IEnumerator Die()
    {
        bUpdataAI = false;
        yield break;
    }

    void SetNextAI(NextAI nextAI)
    {
        if (nextAI.TargetObject != null)
        {
            Target.ThrowEvent(ConstValue.ActorData_SetTarget, nextAI.TargetObject);
        }
        if (nextAI.Position != Vector3.zero)
        {
            MovePosition = nextAI.Position;
        }
        switch (nextAI.StateType)
        {
            case eStateType.STATE_IDLE:
                ProcessIdle();
                break;
            case eStateType.STATE_ATTACK:
                {
                    if (nextAI.TargetObject != null)
                    {
                        // 원본
                        //SelfTransform.forward = (nextAI.TargetObject.SelfTransform.position - SelfTransform.position).normalized;

                        Quaternion newRotation = Quaternion.LookRotation(nextAI.TargetObject.SelfTransform.position - SelfTransform.position);
                        newRotation.x = 0;
                        newRotation.z = 0;
                        SelfTransform.rotation = newRotation;
                    }
                    ProcessAttack();
                }
                break;
            case eStateType.STATE_WALK:
                ProcessMove();
                break;
            case eStateType.STATE_DEAD:
                ProcessDie();
                break;
        }
    }

    public void UpdateAI()
    {
        if (bUpdataAI == true)
            return;

        if (ListNextAI.Count > 0)   // ListNextAI.Count가 2개 이상 될 수 없음.
        {
            SetNextAI(ListNextAI[0]);
            ListNextAI.RemoveAt(0);
        }

        if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
        {
            ListNextAI.Clear();
            ProcessDie();
        }

        bUpdataAI = true;

        switch (CurrentAISatae)
        {
            case eStateType.STATE_IDLE:
                StartCoroutine("Idle");
                break;
            case eStateType.STATE_ATTACK:
                StartCoroutine("Attack");
                break;
            case eStateType.STATE_WALK:
                StartCoroutine("Move");
                break;
            case eStateType.STATE_DEAD:
                StartCoroutine("Die");
                break;
        }
    }

    public void ClearAI()
    {
        ListNextAI.Clear();
    }

    public void ClearAI(eStateType stateType)
    {
        //// #1
        //List<int> removeIndex = new List<int>();
        //for (int i = 0; i < ListNextAI.Count; i++)
        //{
        //    if (ListNextAI[i].StateType == stateType)
        //        removeIndex.Add(i);
        //}
        //for (int i = 0; i < ListNextAI.Count; i++)
        //{
        //    ListNextAI.RemoveAt(removeIndex[i]);
        //}
        //removeIndex.Clear();


        //// #2 Predicate 메서드를 이용한 삭제   -> 이름이 있는 메소드
        //// List<T>.RemoveAll 메서드 (Predicate<T>) -> 지정된 조건자에 정의된 조건과 일치하는 요소를 모두 제거합니다.
        //tempState = stateType;
        //ListNextAI.RemoveAll(RemovePredicate);


        // #3 Lamda식 사용                         -> 이름이 없는 메소드  , 단점-디버그가 어려움
        // () => {} Lmada
        ListNextAI.RemoveAll((nextAI) =>
        {
            return nextAI.StateType == stateType;
        });

        // 위에 람다신 한줄로
        // ListNextAI.RemoveAll((nextAI) => nextAI.StateType == stateType);
    }
    //// #2
    //eStateType tempState;
    //public bool RemovePredicate(NextAI nextAI)
    //{
    //    return nextAI.StateType == tempState;
    //}

    //protected bool MoveCheck()  // 경로가 없으면 true, 있으면 false
    //{
    //    if (NAV_MESH_AGENT.pathStatus == NavMeshPathStatus.PathComplete) // PathComplete -> 경로완료(이동 끝)
    //    {
    //        if (NAV_MESH_AGENT.hasPath == false || NAV_MESH_AGENT.pathPending == false)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    protected void TurnToTarget(Vector3 position)    // 타겟을 바라봄
    {
        Quaternion newRotation = Quaternion.Slerp(SelfTransform.rotation, Quaternion.LookRotation(position - SelfTransform.position), 2*Time.deltaTime);
        newRotation.x = 0;
        newRotation.z = 0;
        SelfTransform.rotation = newRotation;
    }

    protected void Stop()
    {
        MovePosition = Vector3.zero;
        //NAV_MESH_AGENT.Stop();
    }

    protected void AnimationEnd()
    {
        if (ANIMATOR.GetCurrentAnimatorStateInfo(0).IsName("Attack02"))
        {
            if (ANIMATOR.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Target.SelfComponent<Actor>().AI.IS_ATTACK = false;
                AddNextAI(eStateType.STATE_IDLE);
            }
            if (bIsAttack == false && ANIMATOR.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.5f)
            {
                bIsAttack = true;
            }
            if (bIsAttack == true && ANIMATOR.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                bIsAttack = false;
                Target.SelfComponent<Actor>().RunSkill();
            }
        }
    }
}
