using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OkEvent();

public class UI_Popup : BaseObject
{
    UILabel TitleLabel;
    UILabel ContentsLabel;

    UIButton OkBtn;

    OkEvent Ok;

    private void Awake()
    {
        TitleLabel = FindInChild("Title").GetComponent<UILabel>();
        ContentsLabel = FindInChild("Contents").GetComponent<UILabel>();

        OkBtn = FindInChild("OkBtn").GetComponent<UIButton>();

        EventDelegate.Add(OkBtn.onClick, new EventDelegate(this, "OnClickedOkBtn"));
    }

    public void Set(OkEvent _ok, string _title, string _contents)
    {
        Ok = _ok;
        TitleLabel.text = _title;
        ContentsLabel.text = _contents;
    }

    public void OnClickedOkBtn()
    {
        if (Ok != null)
            Ok();
    }
}