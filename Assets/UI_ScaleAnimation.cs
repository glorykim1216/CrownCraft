using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ScaleAnimation: MonoBehaviour
{

	float duration = 0.4f; // 애니메이션의 길이입니다.(시간)
	float startDelay = 0.2f; // 애니메이션 시작 전 딜레이입니다.
	Vector3 scaleTo = new Vector3(1f, 1f, 1f); // 오브젝트의 최종 Scale 입니다.

	// 부풀었다가 줄어드는 효과를 위한 AnimationCurve 입니다.
	AnimationCurve animationCurve = new AnimationCurve(
	new Keyframe(0f, 0f, 0f, 1f), // 0%일때 0의 값에서 시작해서 
	new Keyframe(0.7f, 1.2f, 1f, 1f), // 애니메이션 시작후 70% 지점에서 1.2의 사이즈까지 커졌다가 
	new Keyframe(1f, 1f, 1f, 0f)); // 100%로 애니메이션이 끝날때는 1.0의 사이즈가 됩니다.

	void Awake()
	{
		this.transform.localScale = new Vector3(0, 0, 0);
	}

	//SetActive(true) 호출 시 실행됩니다.
	void OnEnable()
	{
		open();
	}

	// 팝업 열기
	void open()
	{
		init();

	}


	// 초기화
	void init()
	{
		transform.localScale = new Vector3(0, 0, 0); 
		TweenScale tween = TweenScale.Begin(gameObject, duration, scaleTo);
		tween.duration = duration;
		tween.delay = startDelay;
		//tween.method = UITweener.Method.BounceIn; // AnimationCurve 대신 이것도 한번 써보세요.
		tween.animationCurve = animationCurve;

	}

	// 팝업 닫기
	public void close()
	{
		setDisable();
	}

	// 오브젝트 Disable
	void setDisable()
	{
		gameObject.transform.localScale = new Vector3(0, 0, 0);
		gameObject.SetActive(false);
	}
}

