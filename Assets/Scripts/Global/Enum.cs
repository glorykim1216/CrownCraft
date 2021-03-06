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
	COUNT,
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
    BARBARIAN_ATTACK,
    SPLASH_RANGE_ATTACK,
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
    BAT_SKILL,
    DRAGON_SKILL,
    ARROW,
    WIZARD_SKILL,
    BARBARIAN_SKILL,
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
    SCENE_LOBBY,
    SCENE_GAME,
}

public enum eUIType
{
    // PF -> prefab
    PF_UI_LOGO,
    PF_UI_LOADING,
    PF_UI_CARD,
    PF_UI_CARDDECK,
    PF_UI_CARDPOPUP,
    PF_UI_LOBBY,
    PF_UI_POPUP,
    PF_UI_STAGE,
    ChestCamera,
	PF_UI_GACHA,
    PF_UI_GAME,
    PF_UI_GAMEOVER,
    //PF_UI_INVENTORY,
    //PF_UI_GACHA,

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

public enum eActor
{
    KNIGHT,
    ARCHER,
    BARBARIAN,
    WIZARD,
    CACTUS,
    DEATHKNIGHT,
    GHOST,
    DRAGON,
    GOLEM,
    SKELETON,
    BAT,
    MAX
}