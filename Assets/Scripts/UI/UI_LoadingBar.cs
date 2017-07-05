using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LoadingBar : BaseObject
{
    UIProgressBar ProgressBar;
	UILabel LoadingPercent;

	private void Awake()
	{
		LoadingPercent = FindInChild("LoadingPercent").GetComponent<UILabel>();
	}

	private void OnEnable()
    {
        if (ProgressBar == null)
        {
            ProgressBar = this.GetComponentInChildren<UIProgressBar>();
        }
    }

    public void SetValue(float _value)
    {
        if (ProgressBar == null)
            return;

        ProgressBar.value = _value;
		int temp = (int)(_value * 100) ;
		LoadingPercent.text = temp.ToString()+"%" ;
    }
}
