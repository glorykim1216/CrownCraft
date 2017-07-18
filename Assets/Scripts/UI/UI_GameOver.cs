using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameOver : BaseObject
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

	public void Result(int EnemyTowerDestroyCount, int PlayerTowerDestroyCount)
	{
		// Star UI
		Transform trans = FindInChild("Red");
		StartCoroutine(ActiveStar(trans, EnemyTowerDestroyCount, PlayerTowerDestroyCount));

		trans = FindInChild("Blue");
		StartCoroutine(ActiveStar(trans, EnemyTowerDestroyCount, PlayerTowerDestroyCount));


		// Win UI
		if (EnemyTowerDestroyCount > PlayerTowerDestroyCount)
		{
			trans = FindInChild("Blue").FindChild("Win");
			trans.gameObject.SetActive(true);

			int CoinValue = PlayerPrefs.GetInt("CoinValue");
			CoinValue += 120;
			PlayerPrefs.SetInt("CoinValue", CoinValue);

			int TrophyValue = PlayerPrefs.GetInt("TrophyValue");
			TrophyValue += 15;
			PlayerPrefs.SetInt("TrophyValue", TrophyValue);
		}
		else if (EnemyTowerDestroyCount < PlayerTowerDestroyCount)
		{
			trans = FindInChild("Red").FindChild("Win");
			trans.gameObject.SetActive(true);
		}
		else
		{
			// 무승부
		}

	}
	IEnumerator ActiveStar(Transform trans, int EnemyTowerDestroyCount, int PlayerTowerDestroyCount)
	{
		if (trans.name.Equals("Red"))
		{
			for (int i = 0; i < PlayerTowerDestroyCount; i++)
			{
				Transform star = trans.FindChild("Star" + i);

				star.gameObject.SetActive(true);
				yield return new WaitForSeconds(0.4f);
			}
		}
		else
		{
			for (int i = 0; i < EnemyTowerDestroyCount; i++)
			{
				Transform star = trans.FindChild("Star" + i);

				star.gameObject.SetActive(true);
				yield return new WaitForSeconds(0.4f);
			}
		}


	}
}
