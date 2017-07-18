using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_CardGround : MonoSingleton<UI_CardGround>
{
	bool IsEditMode = false;

	List<string> PlayerDeck = new List<string>();

	public List<UI_Card> UI_Cards = new List<UI_Card>();

	private GameCharacter tempCharacterData;

	int OrgSlotNumber = 0;
	string OrgCardKey = string.Empty;

    //public override void Init()
    //{
    //}

    void Start()
	{
		PlayerDeck = CardManager.Instance.PLAYERDECK;
		UpdateCardGround();

	}


	//덱 정보를 최신화하고 UI정보 갱신
	public void UpdateCardGround()
	{
		for (int i = 0; i < UI_Cards.Count; i++)
		{
			UI_Cards[i].Init(PlayerDeck[i]);
			UI_Cards[i].UpdateCard(i, PlayerDeck[i]);
			UI_Cards[i].OnCardClick = OnCardItemClick;
		}
	}

	public void OnCardItemClick(int slotNumber, GameCharacter characterData)
	{


		print("카드 아이템 클릭 : " + slotNumber);
		//평상 시
		if (!IsEditMode)
		{
			ShowCardPopup(slotNumber, characterData);
			OrgSlotNumber = slotNumber;
		}
		//덱 수정 모드 일 때
		else
		{
			string tempKey = CardManager.Instance.GetCardKey(slotNumber);

			// 원본 카드
			CardManager.Instance.SetCard(OrgSlotNumber, tempKey);
			UI_Cards[OrgSlotNumber].UpdateCard(OrgSlotNumber, tempKey);

			// 바뀌는 카드
			CardManager.Instance.SetCard(slotNumber, tempCharacterData.CHARACTER_TEMPLATE.KEY);
			UI_Cards[slotNumber].UpdateCard(slotNumber, tempCharacterData.CHARACTER_TEMPLATE.KEY);

			IsEditMode = false;
			print("덱 수정 완료");
		}
	}



	public void ShowCardPopup(int orgSlotNumber, GameCharacter characterData)
	{
		if (characterData == null)
		{
			print("카드정보가 없어서 팝업을 표출 할 수 없다잉.");
			return;
		}
		GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_CARDPOPUP);
		UI_CardPopup popup = go.GetComponent<UI_CardPopup>();
		popup.SetCardInfo(characterData);
		popup.Set(
			() =>
			{
				IsEditMode = true;
				tempCharacterData = characterData;
				print("수정 할 덱 위치를 클릭하세요");
				UI_Tools.Instance.HideUI(eUIType.PF_UI_CARDPOPUP);
			},
			() =>
			{
				UI_Tools.Instance.HideUI(eUIType.PF_UI_CARDPOPUP);

			}
			);

	}
}
