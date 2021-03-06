﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoSingleton<ActorManager>
{
    // 하이어라키 관리용
    Transform ActorRoot = null;

    // 모든 엑터 관리
    Dictionary<eTeamType, List<Actor>> DicActor = new Dictionary<eTeamType, List<Actor>>();

    // 몬스터 프리팹 관리
    Dictionary<eMonsterType, GameObject> DicMonsterPrefab = new Dictionary<eMonsterType, GameObject>();


    //private void Awake()
    //{
        //MonsterPrefabInit();
    //}

    //void MonsterPrefabInit()
    //{
    //    for (int i = 0; i < (int)eMonsterType.MAX; i++)
    //    {
    //        GameObject go = Resources.Load("Prefabs/" + ((eMonsterType)i).ToString("F")) as GameObject;
    //        if (go == null)
    //        {
    //            Debug.LogError(((eMonsterType)i).ToString("F") + " Load Failed"); // ToString("F") -> enum 문자형 그대로 가져옴
    //        }
    //        else
    //            DicMonsterPrefab.Add((eMonsterType)i, go);
    //    }
    //}

    //public GameObject GetMonsterPrefab(eMonsterType type)
    //{
    //    if (DicMonsterPrefab.ContainsKey(type) == true)
    //    {
    //        return DicMonsterPrefab[type];
    //    }
    //    else
    //    {
    //        Debug.LogError(type.ToString() + "타입의 몬스터 프리팹이 없습니다.");
    //        return null;
    //    }
    //}

    //public Actor InstantiateOnce(GameObject prefab, Vector3 pos)
    //{
    //    if (prefab == null)
    //    {
    //        Debug.LogError("prefab is null [ActorManager.Instantiate()]");
    //        return null;
    //    }

    //    GameObject go = Instantiate(prefab, pos, Quaternion.identity) as GameObject;

    //    if (ActorRoot == null)
    //    {
    //        GameObject temp = new GameObject();
    //        temp.name = "ActorRoot";
    //        ActorRoot = temp.transform;
    //    }

    //    go.transform.SetParent(ActorRoot);
    //    return go.GetComponent<Actor>();
    //}
    public void Clear()
    {
        DicActor.Clear();
    }

    public void AddActor(Actor actor)
    {
        List<Actor> listActor = null;
        eTeamType teamType = actor.TEAM_TYPE;

        // 리스트 생성 또는 로드
        if (DicActor.ContainsKey(teamType) == false)
        {
            listActor = new List<Actor>();
            DicActor.Add(teamType, listActor);
        }
        else
        {
            DicActor.TryGetValue(teamType, out listActor);
        }

        listActor.Add(actor);   // 리스트가 클래스여서 참조, 따라서 dictionary에 먼저 넣고 리스트에 넣어도 적용됨.
    }

    public void RemoveActor(Actor actor, bool bDelete = false)
    {
        eTeamType teamType = actor.TEAM_TYPE;

        if (DicActor.ContainsKey(teamType) == true)
        {
            List<Actor> listActor = null;
            DicActor.TryGetValue(teamType, out listActor);
            listActor.Remove(actor);
        }
        else
        {
            Debug.LogError("존재 하지 않는 엑터를 삭제하려고 합니다.");
        }

        if (bDelete)
            Destroy(actor.gameObject);
    }

    public BaseObject GetSearchEnemy(BaseObject actor, float radius = 100.0f)
    {
        eTeamType teamType = (eTeamType)actor.GetData(ConstValue.ActorData_Team);

        Vector3 myPosition = actor.SelfTransform.position;

        float nearDistance = radius;
        Actor nearActor = null;

        // 근거리 대상 검색
        foreach (KeyValuePair<eTeamType, List<Actor>> pair in DicActor)
        {
            if (pair.Key == teamType)
                continue;

            for (int i = 0; i < pair.Value.Count; i++)
            {
                if (pair.Value[i].SelfObject.activeSelf == false)
                    continue;

                if (pair.Value[i].OBJECT_STATE == eBaseObjectState.STATE_DIE)
                    continue;

                float distance = Vector3.Distance(myPosition, pair.Value[i].SelfTransform.position);
                if (distance < nearDistance)
                {
                    nearDistance = distance;
                    nearActor = pair.Value[i];
                }
            }
        }
        return nearActor;
    }

    //-----------------------------------------------------------------------------------------------
    // Test Code
    // out 매개변수 사용법
    public BaseObject GetSearchEnemy(BaseObject actor, out float returnDist, float radius = 100.0f)
    {
        eTeamType teamType = (eTeamType)actor.GetData(ConstValue.ActorData_Team);

        Vector3 myPosition = actor.SelfTransform.position;

        float nearDistance = radius;
        Actor nearActor = null;

        //---------------------------초기화
        returnDist = 0f;

        // 근거리 대상 검색
        foreach (KeyValuePair<eTeamType, List<Actor>> pair in DicActor)
        {
            if (pair.Key == teamType)
                continue;

            for (int i = 0; i < pair.Value.Count; i++)
            {
                if (pair.Value[i].SelfObject.activeSelf == false)
                    continue;

                if (pair.Value[i].OBJECT_STATE == eBaseObjectState.STATE_DIE)
                    continue;

                float distance = Vector3.Distance(myPosition, pair.Value[i].SelfTransform.position);
                if (distance < nearDistance)
                {
                    nearDistance = distance;
                    nearActor = pair.Value[i];

                    //------------------------------------매개변수에 값 넣어줌
                    returnDist = nearDistance;
                }
            }
        }
        return nearActor;
    }
}
