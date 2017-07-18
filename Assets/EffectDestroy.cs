using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
	float StackTime = 0f;
	private void Update()
	{
		StackTime += Time.deltaTime;
		if (StackTime > 2f)
			Destroy(gameObject);
	} 
}
