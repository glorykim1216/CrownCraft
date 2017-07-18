using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : BaseObject
{
	bool IsPlayer = false;
	public bool IS_PLAYER
	{
		get { return IsPlayer; }
		set { IsPlayer = value; }
	}

	// TeamType
	[SerializeField]
	eTeamType TeamType;
	public eTeamType TEAM_TYPE
	{
		get { return TeamType; }
		set { TeamType = value; }
	}

	[SerializeField]
	string TemplateKey = string.Empty;


	// TemplateKey -> Status
	// GameCharacter
	//--------------------------------------
	GameCharacter SelfCharacter = null;
	public GameCharacter SELF_CHARACTER
	{ get { return SelfCharacter; } }


	// AI
	BaseAI ai = null;
	public BaseAI AI
	{ get { return ai; } }


	BaseObject TargetObject = null;


	// Attack Target

	// Board -> HP Bar
	[SerializeField]
	bool bEnableBoard = true;

	// Tower
	[SerializeField]
	GameObject Tower = null;

	// DeathEffect
	ParticleSystem DeathParticle = null;

	private void Awake()
	{
		//if (TeamType == eTeamType.TEAM_2)
		//{
		//    GameObject aiObject = new GameObject();
		//    aiObject.name = "TestAI";
		//    //ai = aiObject.AddComponent<TestAI>();
		//    aiObject.transform.SetParent(SelfTransform);
		//    // 없으면 동작 X
		//    ai.Target = this;
		//}



		GameCharacter gameCharacter = CharacterManager.Instance.AddCharacter(TemplateKey);
		gameCharacter.TargetComponenet = this;
		SelfCharacter = gameCharacter;

		for (int i = 0; i < gameCharacter.CHARACTER_TEMPLATE.LIST_SKILL.Count; i++)
		{
			SkillData data = SkillManager.Instance.GetSkillData(gameCharacter.CHARACTER_TEMPLATE.LIST_SKILL[i]);

			if (data == null)
			{
				Debug.LogError(gameCharacter.CHARACTER_TEMPLATE.LIST_SKILL[i] + " 스킬 키를 찾을 수 없습니다.");
			}
			// 2017-06-14
			// 아이템에 스킬이 있을 경우 여기서 설정한다.
			// 캐릭터에 스킬을 셋팅
			else
				gameCharacter.AddSkill(data);
		}

		if (bEnableBoard == true)
		{
			BaseBoard board = BoardManager.Instance.AddBoard(this, eBoardType.BOARD_HP);

			board.SetData(ConstValue.SetData_HP, GetStatusData(eStatusData.HP), SelfCharacter.CURRENT_HP);
		}

		if (transform.name.Contains("Castle"))
		{
			GameObject aiObject = new GameObject();
			aiObject.name = "CastleAI";
			ai = aiObject.AddComponent<CastleAI>();
			aiObject.transform.SetParent(SelfTransform);
			ai.Target = this;      // 없으면 동작 X
		}
		else
		{
			GameObject aiObject = new GameObject();
			aiObject.name = "NormalAI";
			ai = aiObject.AddComponent<NormalAI>();
			aiObject.transform.SetParent(SelfTransform);
			ai.Target = this;           // target
			ai.MOVESPEED = (float)gameCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.SPEED);

			// MovePath 설정
			ai.GetComponent<NormalAI>().SetMovePath(transform.position);

			// TestCode
			if (TeamType == eTeamType.TEAM_2)
				ai.GetComponent<NormalAI>().Team(2);
			else
				ai.GetComponent<NormalAI>().Team(1);
		}

		ActorManager.Instance.AddActor(this);

	}

	public double GetStatusData(eStatusData statusData)
	{
		return SelfCharacter.CHARACTER_STATUS.GetStatusData(statusData);
	}

	public override object GetData(string keyData, params object[] datas) // params -> 매개변수들 여러개 받을때 사용
	{
		if (keyData == ConstValue.ActorData_Team)
			return TeamType;
		else if (keyData == ConstValue.ActorData_Character)
			return SelfCharacter;
		else if (keyData == ConstValue.ActorData_GetTarget)
			return TargetObject;
		else if (keyData == ConstValue.ActorData_SkillData)
		{
			int index = (int)datas[0];
			return SelfCharacter.GetSkillByIndex(index);
		}

		// Base 부모클래스 -> BaseObject
		return base.GetData(keyData, datas);
	}

	public override void ThrowEvent(string keyData, params object[] datas)
	{
		if (keyData.Equals(ConstValue.EventKey_Hit))
		{
			if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
				return;

			// 공격 주체의 캐릭터
			GameCharacter casterCharacter = datas[0] as GameCharacter;

			// 2017-06-01------------------------------------------------------
			// 순서 중요
			SkillTemplate skillTemplate = datas[1] as SkillTemplate;
			casterCharacter.CHARACTER_STATUS.AddStatusData("SKILL", skillTemplate.STATUS_DATA);


			double attackDamage = casterCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.ATTACK);


			// 2017-06-01------------------------------------------------------
			// 순서 중요
			casterCharacter.CHARACTER_STATUS.RemoveSattusData("SKILL"); // 스킬을 지움 -> 데미지 복구


			// 피격
			SelfCharacter.IncreaseCurrentHP(-attackDamage);

			//// 죽으면
			if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
			{
				DeathEffect();

				Debug.Log(gameObject.name + " 죽음!");
				//GameManager.Instance.KillCheck(this);
			}

			// HPBoard
			BaseBoard board = BoardManager.Instance.GetBoardData(this, eBoardType.BOARD_HP);
			if (board != null)
				board.SetData(ConstValue.SetData_HP, GetStatusData(eStatusData.HP), SelfCharacter.CURRENT_HP);

			// 피격 효과
			StartCoroutine(DemageEff());

			//// Board 초기화
			//board = null;
			//// DamageBoard
			//board = BoardManager.Instance.AddBoard(this, eBoardType.BOARD_DAMAGE);
			//if (board != null)
			//    board.SetData(ConstValue.SetData_Damage, attackDamage);

			// 피격 애니메이션
			//AI.ANIMATOR.SetInteger("Hit", 1);
		}
		else if (keyData == ConstValue.EventKey_SelectSkill)
		{
			int index = (int)datas[0];
			SkillData data = SelfCharacter.GetSkillByIndex(index);  // 캐릭터에서 내가 원하는 index스킬을 가져와서
			SelfCharacter.SELECT_SKILL = data;                      // SelfCharacter.SELECT_SKILL에 넣음
		}
		else if (keyData == ConstValue.ActorData_SetTarget)
		{
			TargetObject = datas[0] as BaseObject;
		}
		else
			base.ThrowEvent(keyData, datas);
	}

	protected virtual void Update()
	{
		AI.UpdateAI();
		if (AI.END)
		{
			Destroy(SelfObject);
			if (Tower != null)
			{
				Destroy(Tower);
				if (SelfTransform.name.Equals("Castle_King"))
				{
					if (TEAM_TYPE == eTeamType.TEAM_1)
					{
						GameManager.Instance.PlayerTowerDestroyCount = 3;
					}
					else if (TeamType == eTeamType.TEAM_2)
					{
						GameManager.Instance.EnemyTowerDestroyCount = 3;
					}
				}
				else
				{
					if (TEAM_TYPE == eTeamType.TEAM_1)
					{
						GameManager.Instance.PlayerTowerDestroyCount++;
					}
					else if (TeamType == eTeamType.TEAM_2)
					{
						GameManager.Instance.EnemyTowerDestroyCount++;
					}
				}

                UI_Manager.Instance.ScoreUpdate(GameManager.Instance.EnemyTowerDestroyCount, GameManager.Instance.PlayerTowerDestroyCount);

            }
		}
	}

	// SkillData ( 스킬들의 조각 )
	// SkillData에 스킬 뿐만 아니라 여러가지스킬조각,데미지,연출,텍스트,사운드 등을 넣어 조합하여 사용할 수 있다. ( 딜레이까지 넣으면 순차적으로 실행가능 )
	public void RunSkill()
	{
		SkillData selectSill = SelfCharacter.SELECT_SKILL;
		if (selectSill == null)
			return;

		// SKILL_LIST -> 스킬 템플릿 리스트
		for (int i = 0; i < selectSill.SKILL_LIST.Count; i++)
		{
			SkillManager.Instance.RunSkill(this, selectSill.SKILL_LIST[i]);    // melee 스킬이 2개 있으면 2번 실행
		}
	}

	private void OnEnable()
	{
		if (BoardManager.Instance != null)
			BoardManager.Instance.ShowBoard(this, true);
	}

	public void OnDisable()
	{
		if (GameManager.Instance.GAME_OVER == false && BoardManager.Instance != null)
			BoardManager.Instance.ShowBoard(this, false);
	}

	// 매니저로 관리 할때, 추가와 삭제를 신경써야한다. ( 삭제해도 다른곳에서 데이터만 남아 있어 꼬일 수 있음. )
	public void OnDestroy()
	{
		if (BoardManager.Instance != null)
			BoardManager.Instance.ClearBoard(this);

		if (ActorManager.Instance != null)
		{
			ActorManager.Instance.RemoveActor(this);
		}
		Debug.Log("OnDestroy_Actor" + TemplateKey);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Cube")
		{
			AI.GetComponent<NormalAI>().pathRotate = 90f;
			AI.GetComponent<NormalAI>().bDeadEnd = true;
		}
	}
	IEnumerator DemageEff()
	{
		Material m = FindInChild("Model").GetComponentInChildren<SkinnedMeshRenderer>().material;
		m.mainTextureScale = new Vector2(0, 0);
		yield return new WaitForSeconds(0.1f);
		m.mainTextureScale = new Vector2(1, 1);

	}
	void DeathEffect()
	{
		ParticleSystem DeathEffect = null;
		GameObject go = Instantiate(Resources.Load("Prefabs/DeathEffect"),transform.position,Quaternion.identity) as GameObject;
		DeathEffect = (ParticleSystem)go.GetComponent<ParticleSystem>();
	
		if (DeathEffect && DeathEffect.isStopped)
		{
			DeathEffect.Play();
		}
		

	}
}
