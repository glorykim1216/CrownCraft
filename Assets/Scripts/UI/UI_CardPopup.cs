using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UseEvent();
public delegate void ClosedEvent();

public class UI_CardPopup : BaseObject
{
    UILabel NameLabel;
	UILabel ContentsLabel;
	UILabel AttackLabel;
	UILabel HPLabel;
	UILabel TargetLabel;
	UILabel SpeedLabel;
	UILabel RangeLabel;
	UILabel CountLabel;



	UIButton UseBtn;
    UIButton ClosedBtn;

    UseEvent Use;
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

		NameLabel = FindInChild("Name").GetComponent<UILabel>();
		AttackLabel = FindInChild("Attack").FindChild("Value").GetComponent<UILabel>();
		HPLabel = FindInChild("HP").FindChild("Value").GetComponent<UILabel>();
		TargetLabel = FindInChild("Target").FindChild("Value").GetComponent<UILabel>();
		SpeedLabel = FindInChild("Speed").FindChild("Value").GetComponent<UILabel>();
		RangeLabel = FindInChild("Range").FindChild("Value").GetComponent<UILabel>();
		CountLabel= FindInChild("Count").FindChild("Value").GetComponent<UILabel>();
	}

	private void Start()
	{
		EventDelegate.Add(ClosedBtn.onClick,new EventDelegate(this, "OnClickedClosedBtn"));
		EventDelegate.Add(UseBtn.onClick,new EventDelegate(this,"OnClickUseBtn"));

	}


	public void Set(
        ClosedEvent _closed
        //,UpgradeEvent _upgrade
        //,string _name
        //,string _contents
        )
    {
        Closed = _closed;
        //Upgrade = _upgrade;
        //TitleLabel.text = _name;
        //ContentsLabel.text = _contents;
    }
	//public void Set(UseEvent _use)
	//{
	//	Use = _use;
	//}

    public void OnClickedClosedBtn()
    {
        if (Closed != null)
            Closed();
    }

	public void OnClickUseBtn()
	{
		if (Use != null)
			Use();
	}

	public void SetCardInfo(GameCharacter _gameCharacter)
	{

		NameLabel.text = _gameCharacter.CHARACTER_TEMPLATE.KEY;
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
