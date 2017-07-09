using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_CardGround : BaseObject
{
	public bool IsEditMode = false;

	public UI_Card[] CardSlots;

	private GameCharacter tempCharacterData;
	//public Action<int, string> OnCardItemSelect;
	// Use this for initialization
	void Start() {
		UpdateCardGround();
	}




	// Update is called once per frame
	void Update() {

	}

	//덱 정보를 최신화하고 UI정보 갱신
	public void UpdateCardGround()
	{
		for (int i = 0; i < CardSlots.Length; i++)
		{
			//카드 매니저에서 데이터를 불러온다.					
			CardSlots[i].UpdateCard(CardManager.Instance.GetCardKey(i));
			CardSlots[i].OnCardClick = OnCardItemClick;
		}
	}

	public void OnCardItemClick(int slotNumber, GameCharacter characterData)
	{
		print("카드 아이템 클릭 : " + slotNumber);
		//평상 시
		if (!IsEditMode)
			ShowCardPopup(characterData);
		//덱 수정 모드 일 때
		else
		{
			CardManager.Instance.SetCard(slotNumber, tempCharacterData.CHARACTER_TEMPLATE.KEY);
			CardSlots[slotNumber].UpdateCard(tempCharacterData.CHARACTER_TEMPLATE.KEY);
			IsEditMode = false;
			CardManager.Instance.SetLocalData();
			print("덱 수정 완료");
		}
	}


	public void ShowCardPopup(GameCharacter characterData)
	{
		if(characterData == null)
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
