using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Card : BaseObject
{
	// TeamType
	[SerializeField]
	string TemplateKey = string.Empty;

	public string TEMPLATEKEY
	{
		get { return TemplateKey; }
	}

	GameCharacter CharacterData;
	public GameCharacter CHARACTER_DATA
	{
		get { return CharacterData; }
	}


	UISprite CardImage;

	private void Awake()
	{
		CharacterData = CharacterManager.Instance.AddCharacter(TemplateKey);

		CardImage = FindInChild("Texture").GetComponent<UISprite>();
	}

	private void Start()
	{
		//Transform temp = FindInChild("Texture");
		//temp.GetComponent<UISprite>().name = TemplateKey;

		CardImage.spriteName = TemplateKey;
	}



	void OnClick()
	{
		GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_CARDPOPUP);
		UI_CardPopup popup = go.GetComponent<UI_CardPopup>();
		popup.SetCardInfo(CharacterData);
		popup.Set(
			//        () =>
			//        {
			//            ItemManager.Instance.EquipItem(itemInstance);
			//            UI_Tools.Instance.HideUI(eUIType.PF_UI_POPUP);
			//        },
			() =>
			{
				UI_Tools.Instance.HideUI(eUIType.PF_UI_CARDPOPUP);
			}

			//        "장비 장착"
			//        ,
			//        "이 장비를 장착 하시겠습니까?"
			);
	}

}