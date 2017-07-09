using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameEnd : BaseObject
{
	UIButton OKBtn = null;

	private void Awake()
	{
		// LobbyIcon Btn
		//----------------------------------------------------
		Transform trans = FindInChild("OKBtn");
		if (trans == null)
		{
			Debug.LogError("OKBtn is Not Founded");
			return;
		}
		OKBtn = trans.GetComponent<UIButton>();
		EventDelegate.Add(OKBtn.onClick,
			new EventDelegate(this, "SceneChange"));
	}

	void SceneChange()
	{
		Scene_Manager.Instance.LoadScene(eSceneType.SCENE_LOBBY);
	}
}
