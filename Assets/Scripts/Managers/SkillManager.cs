﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public class SkillManager : MonoSingleton<SkillManager>
{
    Dictionary<BaseObject, List<BaseSkill>> DicUseSkill = new Dictionary<BaseObject, List<BaseSkill>>();    // BaseObject를 관리( BaseObject -> 여기서 Actor ), 스킬 추가 삭제

    Dictionary<string, SkillTemplate> DicSkillTemplate = new Dictionary<string, SkillTemplate>();           // 스킬을 구성하는 한가지 동작 ( 스킬퍼즐조각 )
    Dictionary<string, SkillData> DicSkillData = new Dictionary<string, SkillData>();                       // 하나의 스킬 ( 템플릿의 모음 )

    Dictionary<eSkillModelType, GameObject> DicModel = new Dictionary<eSkillModelType, GameObject>();

    private void Awake()
    {
        LoadSkillData(ConstValue.SkillDataPath);
        LoadSkillTemplate(ConstValue.SkillTemplatePath);
        LoadSkillModel();
    }

    void LoadSkillData(string strFilePath)
    {
        TextAsset skillAssetData = Resources.Load("JSON/SKILL_DATA") as TextAsset;

        if (skillAssetData == null)
        {
            Debug.LogError("Skill Data Load Failed");
            return;
        }

        JSONNode rootNode = JSON.Parse(skillAssetData.text);
        if (rootNode == null)
            return;

        JSONObject skillDataNode = rootNode["SKILL_DATA"] as JSONObject;
        foreach (KeyValuePair<string, JSONNode> pair in skillDataNode)
        {
            SkillData skillData = new SkillData(pair.Key, pair.Value);
            DicSkillData.Add(pair.Key, skillData);
        }
    }

    void LoadSkillTemplate(string strFilePath)
    {
        TextAsset skillAssetData = Resources.Load(strFilePath) as TextAsset;

        if (skillAssetData == null)
        {
            Debug.LogError("Skill Template Load Failed");
            return;
        }

        JSONNode rootNode = JSON.Parse(skillAssetData.text);
        if (rootNode == null)
            return;

        JSONObject skillDataNode = rootNode["SKILL_TEMPLATE"] as JSONObject;
        foreach (KeyValuePair<string, JSONNode> pair in skillDataNode)
        {
            SkillTemplate skillTemplate = new SkillTemplate(pair.Key, pair.Value);
            DicSkillTemplate.Add(pair.Key, skillTemplate);
        }
    }

    public void LoadSkillModel()
    {
        for (int i = 0; i < (int)eSkillModelType.MAX; i++)
        {
            GameObject go = Resources.Load("Prefabs/Skill_Models/" + ((eSkillModelType)i).ToString()) as GameObject;

            if (go == null)
            {
                Debug.LogError("Prefabs/Skill_Models/" + ((eSkillModelType)i).ToString() + "파일을 찾지 못함");
                continue;
            }
            DicModel.Add((eSkillModelType)i, go);
        }
    }

    public GameObject GetModel(eSkillModelType type)
    {
        if (DicModel.ContainsKey(type))
        {
            return DicModel[type];
        }
        else
        {
            Debug.LogError(type.ToString() + " is null");
            return null;
        }
    }

    public SkillData GetSkillData(string _strKey)
    {
        SkillData skillData = null;
        DicSkillData.TryGetValue(_strKey, out skillData);
        return skillData;
    }

    public SkillTemplate GetSkillTemplate(string _strKey)
    {
        SkillTemplate skillTemplate = null;
        DicSkillTemplate.TryGetValue(_strKey, out skillTemplate);
        return skillTemplate;
    }

    public void RunSkill(BaseObject keyObject, string strSkillTemplateKey)
    {
        SkillTemplate template = GetSkillTemplate(strSkillTemplateKey);
        if (template == null)
        {
            Debug.LogError(strSkillTemplateKey + " is null");
            return;
        }
        BaseSkill runSkill = CreateSkill(keyObject, template);
        RunSkill(keyObject, runSkill);
    }

    void RunSkill(BaseObject keyObject, BaseSkill runSkill) // keyObject ->Actor
    {
        List<BaseSkill> listSkill = null;
        if (DicUseSkill.ContainsKey(keyObject) == false)
        {
            listSkill = new List<BaseSkill>();
            DicUseSkill.Add(keyObject, listSkill);
        }
        else
            listSkill = DicUseSkill[keyObject];

        listSkill.Add(runSkill);    // 아래서 넣어도 참조형식이라 위에 DicUseSkill에 value값으로 들어감.
    }

    BaseSkill CreateSkill(BaseObject owner, SkillTemplate skillTemplate)
    {
        BaseSkill makeSkill = null;
        GameObject skillObject = new GameObject();

        Transform parentTransform = null;
        switch (skillTemplate.SKILL_TYPE)
        {
            case eSkillTemplateType.TARGET_ATTACK:
                makeSkill = skillObject.AddComponent<MeleeSkill>();
                parentTransform = owner.SelfTransform;
                break;
            case eSkillTemplateType.RANGE_ATTACK:
                makeSkill = skillObject.AddComponent<RangeSkill>();
                //parentTransform = owner.SelfTransform;//owner.FindInChild("FirePos"); 
                parentTransform = owner.FindInChild("FirePos");
                if (parentTransform == null)
                    parentTransform = owner.SelfTransform;

                makeSkill.ThrowEvent(ConstValue.EventKey_SelectModel, GetModel(skillTemplate.MODEL_TYPE));   // 스킬모델
                break;
            case eSkillTemplateType.BARBARIAN_ATTACK:
                //makeSkill = skillObject.AddComponent<MeleeSkill>();
                makeSkill = skillObject.AddComponent<BarbarianSkill>();
                parentTransform = owner.SelfTransform;
                makeSkill.ThrowEvent(ConstValue.EventKey_SelectModel, GetModel(skillTemplate.MODEL_TYPE));   // 스킬모델

                break;
            case eSkillTemplateType.SPLASH_RANGE_ATTACK:
                makeSkill = skillObject.AddComponent<SplashRangeSkill>();
                //parentTransform = owner.SelfTransform;//owner.FindInChild("FirePos"); 
                parentTransform = owner.FindInChild("FirePos");
                if (parentTransform == null)
                    parentTransform = owner.SelfTransform;

                makeSkill.ThrowEvent(ConstValue.EventKey_SelectModel, GetModel(skillTemplate.MODEL_TYPE));   // 스킬모델
                break;
        }

        skillObject.name = skillTemplate.SKILL_TYPE.ToString();

        if (makeSkill != null)
        {
            makeSkill.transform.position = parentTransform.position;
            makeSkill.transform.rotation = parentTransform.rotation;

            makeSkill.OWNER = owner;
            makeSkill.SKILL_TEAMPLATE = skillTemplate;
            makeSkill.TARGET = owner.GetData(ConstValue.ActorData_GetTarget) as BaseObject;

            makeSkill.InitSkill();  // 초기화
        }
        switch (skillTemplate.RANGE_TYPE)
        {
            case eSkillAttackRangeType.RANGE_BOX:
                {
                    BoxCollider collider = skillObject.AddComponent<BoxCollider>();
                    collider.size = new Vector3(skillTemplate.RANGE_DATA_1, 3, skillTemplate.RANGE_DATA_2);
                    collider.center = new Vector3(0, 0, skillTemplate.RANGE_DATA_2 * 0.5f);
                    collider.isTrigger = true;
                }
                break;
            case eSkillAttackRangeType.RANGE_SPHERE:
                {
                    SphereCollider collider = skillObject.AddComponent<SphereCollider>();
                    collider.radius = skillTemplate.RANGE_DATA_1;
                    collider.isTrigger = true;
                }
                break;
        }
        return makeSkill;
    }

    public void Update()
    {
        //if (GameManager.Instance.GAME_OVER == true)
        //    return;

        foreach (KeyValuePair<BaseObject, List<BaseSkill>> pair in DicUseSkill)
        {
            List<BaseSkill> list = pair.Value;
            for (int i = 0; i < list.Count; i++)
            {
                BaseSkill updateSkill = list[i];
                updateSkill.UpdateSkill();
                if (updateSkill.END)
                {
                    list.Remove(updateSkill);
                    Destroy(updateSkill.gameObject);
                }
            }
        }
    }

    public void ClearSkill()
    {
        foreach (KeyValuePair<BaseObject, List<BaseSkill>> pair in DicUseSkill)
        {
            List<BaseSkill> list = pair.Value;
            for (int i = 0; i < list.Count; i++)
            {
                BaseSkill updateSkill = list[i];
                list.Remove(updateSkill);
                Destroy(updateSkill.gameObject);
            }
        }
        DicUseSkill.Clear();
    }
}
