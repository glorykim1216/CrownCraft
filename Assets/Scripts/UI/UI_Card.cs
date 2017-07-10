using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Card : BaseObject
{
	// TeamType
	[SerializeField]
	string TemplateKey = string.Empty;

	public int slotNumber = 0;
	public System.Action<int, GameCharacter> OnCardClick;
	
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
		//CharacterData = CharacterManager.Instance.AddCharacter(TemplateKey);
		CardImage = FindInChild("Texture").GetComponent<UISprite>();
	}

	private void Start()
	{
		//Transform temp = FindInChild("Texture");
		//temp.GetComponent<UISprite>().name = TemplateKey;

		//CardImage.spriteName = TemplateKey;
	}

	public void UpdateCard(string CardKey = "")
	{
		print("UI_Card :: 카드 정보 업데이트 = "+CardKey);

		if(CardKey != "")
			TemplateKey = CardKey;

		if (TemplateKey == "")
			return;
		
		CharacterData = CharacterManager.Instance.AddCharacter(TemplateKey);

		if (CharacterData != null)
			print(CharacterData.CHARACTER_TEMPLATE.KEY);
		else
			Debug.LogError("캐릭터 정보가 없습니다.");
		
		CardImage.spriteName = TemplateKey;
	}

	void OnClick()
	{
		print("카드 클릭함");
		if(OnCardClick != null)
		{
			OnCardClick(slotNumber, CharacterData);			
		}		
	}

}