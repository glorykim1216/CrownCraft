using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UseEvent();
public delegate void ClosedEvent();

public class UI_CardPopup : BaseObject
{
	UILabel LevelLabel;
	UILabel NameLabel;
	UILabel ContentsLabel;
	UILabel AttackLabel;
	UILabel HPLabel;
	UILabel TargetLabel;
	UILabel SpeedLabel;
	UILabel RangeLabel;
	UILabel CountLabel;

	UISprite CardImage;


	UIButton UseBtn;
	UIButton ClosedBtn;

	public UseEvent Use;
	ClosedEvent Closed;

	UI_Card CardInfo;



	private void Awake()
	{

		//TitleLabel = FindInChild("Name").GetComponent<UILabel>();
		//ContentsLabel = FindInChild("Contents").GetComponent<UILabel>();

		//UpgradeBtn = FindInChild("UpgradeBtn").GetComponent<UIButton>();
		//EventDelegate.Add(UpgradeBtn.onClick,
		//    new EventDelegate(this, "OnClickedUpgradeBtn"));

		ClosedBtn = FindInChild("ClosedBtn").GetComponent<UIButton>();
		UseBtn = FindInChild("UseBtn").GetComponent<UIButton>();

		CardImage = FindInChild("Texture").GetComponent<UISprite>();
		LevelLabel = FindInChild("Level").GetComponent<UILabel>();
		NameLabel = FindInChild("Name").GetComponent<UILabel>();
		ContentsLabel = FindInChild("Contents").GetComponent<UILabel>();
		AttackLabel = FindInChild("Attack").FindChild("Value").GetComponent<UILabel>();
		HPLabel = FindInChild("HP").FindChild("Value").GetComponent<UILabel>();
		TargetLabel = FindInChild("Target").FindChild("Value").GetComponent<UILabel>();
		SpeedLabel = FindInChild("Speed").FindChild("Value").GetComponent<UILabel>();
		RangeLabel = FindInChild("Range").FindChild("Value").GetComponent<UILabel>();
		CountLabel = FindInChild("Count").FindChild("Value").GetComponent<UILabel>();
	}

	private void Start()
	{
		EventDelegate.Add(ClosedBtn.onClick, new EventDelegate(this, "OnClickedClosedBtn"));
		EventDelegate.Add(UseBtn.onClick, new EventDelegate(this, "OnClickUseBtn"));

	}
	//public void Set (UseEvent _use	)
	//{
	//	Use = _use;
	//}

	public void Set( UseEvent _use,	ClosedEvent _closed	)
	{
		Use = _use;
		Closed = _closed;
	}


	public void OnClickedClosedBtn()
	{
		if (Closed != null)
			Closed();
	}

	public void OnClickUseBtn()
	{
		if (Use != null)
			Use();

		//1. 교체 할 카드 선택을 기다린다.
		//2. 교체 할 카드를 선택하면 CardManager에 있는 덱 정보를 업데이트해준다.
		//3. 팝업 UI를 업데이트(교체한 내용 적용)


		//CardManager.Instance.SetCard(NameLabel.text);
		//UI_Tools.Instance.HideUI(eUIType.PF_UI_CARDPOPUP);
	}

	public void SetCardInfo(GameCharacter _gameCharacter)
	{
		if (_gameCharacter == null)
			return;

		CardImage.spriteName = _gameCharacter.CHARACTER_TEMPLATE.KEY;

		int tempLevel = 0;
		CardManager.Instance.DIC_CARDLEVEL.TryGetValue(_gameCharacter.CHARACTER_TEMPLATE.KEY, out tempLevel);
		LevelLabel.text = tempLevel.ToString() + " LEVEL";

		NameLabel.text = _gameCharacter.CHARACTER_TEMPLATE.KEY;

		ContentsLabel.text = _gameCharacter.CHARACTER_TEMPLATE.CARDCONTENTS;
		AttackLabel.text = _gameCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.ATTACK).ToString();
		HPLabel.text = _gameCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.HP).ToString();
		TargetLabel.text = _gameCharacter.CHARACTER_TEMPLATE.CARDTARGET;
		SpeedLabel.text = _gameCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.SPEED).ToString();
		RangeLabel.text = _gameCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.RANGE).ToString();
		CountLabel.text = _gameCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.COUNT).ToString();
	}
	//public void OnClickedUpgradeBtn()
	//{
	//    if (Upgrade != null)
	//        Upgrade();
	//}

}
