﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Card : BaseObject
{
	// TeamType
	[SerializeField]
	string TemplateKey = string.Empty;

	private void Awake()
	{
		GameCharacter CharacterData = CharacterManager.Instance.AddCharacter(TemplateKey);

	}

	private void Start()
	{
		Transform temp = FindInChild("Texture");
		temp.GetComponent<UISprite>().name = TemplateKey;
	}




	void OnClick()
	{
		GameObject go = UITools.Instance.ShowUI(eUIType.PF_UI_CARDPOPUP);
		UI_CardPopup popup = go.GetComponent<UI_CardPopup>();

		popup.Set(
			//        () =>
			//        {
			//            ItemManager.Instance.EquipItem(itemInstance);
			//            UI_Tools.Instance.HideUI(eUIType.PF_UI_POPUP);
			//        },
			() =>
			{
				UITools.Instance.HideUI(eUIType.PF_UI_CARDPOPUP);
			}

			//        "장비 장착"
			//        ,
			//        "이 장비를 장착 하시겠습니까?"
			);
	}

}