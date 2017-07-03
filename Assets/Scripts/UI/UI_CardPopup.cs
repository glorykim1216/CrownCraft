using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UpgradeEvent();
public delegate void ClosedEvent();

public class UI_CardPopup : BaseObject
{
    UILabel TitleLabel;
    UILabel ContentsLabel;

    UIButton UpgradeBtn;
    UIButton ClosedBtn;

    UpgradeEvent Upgrade;
    ClosedEvent Closed;

    private void Awake()
    {
        //TitleLabel = FindInChild("Name").GetComponent<UILabel>();
        //ContentsLabel = FindInChild("Contents").GetComponent<UILabel>();

        //UpgradeBtn = FindInChild("UpgradeBtn").GetComponent<UIButton>();
        //EventDelegate.Add(UpgradeBtn.onClick,
        //    new EventDelegate(this, "OnClickedUpgradeBtn"));

        ClosedBtn = FindInChild("ClosedBtn").GetComponent<UIButton>();
        EventDelegate.Add(ClosedBtn.onClick,
            new EventDelegate(this, "OnClickedClosedBtn"));
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

    public void OnClickedClosedBtn()
    {
        if (Closed != null)
            Closed();
    }

    //public void OnClickedUpgradeBtn()
    //{
    //    if (Upgrade != null)
    //        Upgrade();
    //}

}
