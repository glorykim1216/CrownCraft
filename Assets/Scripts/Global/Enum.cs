﻿
public enum eBaseObjectState
{
    STATE_NORMAL,
    STATE_DIE,

}

public enum eStateType
{
    STATE_NONE = 0,
    STATE_IDLE,
    STATE_ATTACK,
    STATE_WALK,
    STATE_DEAD
}

public enum eStatusData
{
	LEVEL,
    HP,
    ATTACK,
    COST,
	SPEED,
	RANGE,
	TARGET,
	COUNT,
	TRANSPORT,
	TYPE,
    MAX,
}

public enum eTeamType
{
    TEAM_1,
    TEAM_2,
}

// Monster 관련
public enum eRegeneratorType
{
    NONE,
    REGENTIME_EVENT,
    TRIGGER_EVENT,
}

public enum eMonsterType
{
    A_Monster,
    B_Monster,
    C_Monster,
    MAX
}

// Skill 관련
public enum eSkillTemplateType
{
    TARGET_ATTACK,
    RANGE_ATTACK,
}

public enum eSkillAttackRangeType
{
    RANGE_BOX,
    RANGE_SPHERE,
}

public enum eSkillModelType
{
    CIRCLE,
    BOX,
    MAX
}

public enum eBoardType
{
    BOARD_NONE,
    BOARD_HP,
    BOARD_DAMAGE,
}

public enum eClearType
{
    CLEAR_KILLCOUNT = 0,
    CLEAR_TIME,
}

public enum eSceneType
{
    SCENE_NONE,
    SCENE_LOGO,
    SCENE_GAME,
    SCENE_LOBBY,
}

public enum eUIType
{
    // PF -> prefab

    PF_UI_LOGO,
    PF_UI_LOADING,
    PF_UI_LOBBY,
    PF_UI_INVENTORY,
    PF_UI_POPUP,
    PF_UI_STAGE,
    PF_UI_GACHA,
}

public enum eSlotType
{
    SLOT_NONE = -1,
    SLOT_WEAPON = 0,
    SLOT_ARMOR,
    SLOT_HELMET,
    SLOT_GUNTLET,
    SLOT_MAX,
}