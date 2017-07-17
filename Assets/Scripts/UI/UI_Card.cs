using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Card : BaseObject
{
	// TeamType
	string TemplateKey = string.Empty;

	public int slotNumber = 0;
	public int SLOTNUM { get { return slotNumber; } }

	public System.Action<int, GameCharacter> OnCardClick;

	public string TEMPLATEKEY
	{
		get { return TemplateKey; }
		set { TemplateKey = value; }
	}

	GameCharacter CharacterData;
	public GameCharacter CHARACTER_DATA
	{
		get { return CharacterData; }
	}


	UISprite CardImage;

	UILabel CardLevel;
	UILabel CardMana;


	public void Init(string _templatekey)
	{
		CharacterData = CharacterManager.Instance.AddCharacter(_templatekey);
		CardImage = FindInChild("Texture").GetComponent<UISprite>();
		CardLevel = FindInChild("Level").GetComponent<UILabel>();
		CardMana = FindInChild("ManaValue").GetComponent<UILabel>();
		TemplateKey = _templatekey;

	}



	public void UpdateCard(int slotNum, string CardKey)
	{
		print("UI_Card :: 카드 정보 업데이트 = " + CardKey);

		if (CardKey != string.Empty)
			TemplateKey = CardKey;

		CharacterData = CharacterManager.Instance.AddCharacter(TemplateKey);

		if (CharacterData != null)
			print(CharacterData.CHARACTER_TEMPLATE.KEY);
		else
			Debug.LogError("캐릭터 정보가 없습니다.");
		slotNumber = slotNum;
		CardImage.spriteName = TemplateKey;
		int tempLevel;
		CardManager.Instance.DIC_CARDLEVEL.TryGetValue(TemplateKey, out tempLevel);
		CardLevel.text = tempLevel.ToString()+" LEVEL"; 
		CardMana.text = CharacterData.CHARACTER_STATUS.GetStatusData(eStatusData.COST).ToString();
	}

	void OnClick()
	{
		print("카드 클릭함");
		if (OnCardClick != null)
		{
			OnCardClick(slotNumber, CharacterData);
		}
	}

}