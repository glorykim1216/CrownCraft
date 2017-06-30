﻿using UnityEngine;
using System.Collections;

public class ClearDialogInit : Dialog {

	public int stars;

	override public void OpenComplete()
	{
		base.OpenComplete ();
		gameObject.GetComponentInChildren<StarsModule> ().Show (stars);
	}

	override public void Open(){
		if (_isOpened)
			return;
		gameObject.GetComponentInChildren<StarsModule> ().Hide();
		base.Open ();

	}

}
