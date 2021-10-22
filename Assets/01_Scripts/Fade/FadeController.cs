using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
	public static float fadeColor = 0;

	private static FadeController instance;

	public static FadeController Instance => instance;

	public FadeController()
	{
		instance = this;
	}

	internal static class YieldInstructionCache
	{
		public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
	}

	public void FadeIn(float fadeOutTime, System.Action nextEvent = null)
	{
		StartCoroutine(CoFadeIn(fadeOutTime, nextEvent));
	}

	public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
	{
		StartCoroutine(CoFadeOut(fadeOutTime, nextEvent));
	}

	// 투명 -> 불투명
	IEnumerator CoFadeIn(float fadeOutTime, System.Action nextEvent = null)
	{
		Image ima = this.gameObject.GetComponent<Image>();
		Color tempColor = ima.color;

		while (tempColor.a < 1f)
		{
			tempColor.a += Time.deltaTime / fadeOutTime;
			ima.color = tempColor;

			fadeColor = tempColor.a;

			if (tempColor.a >= 1f)
			{
				tempColor.a = 1f;
			}

			ima.color = tempColor;

			if (nextEvent != null) nextEvent();

			yield return YieldInstructionCache.WaitForFixedUpdate;
		}
	}

	// 불투명 -> 투명
	IEnumerator CoFadeOut(float fadeOutTime, System.Action nextEvent = null)
	{
		Image ima = this.gameObject.GetComponent<Image>();
		Color tempColor = ima.color;

		while (tempColor.a > 0)
		{
			tempColor.a -= Time.deltaTime / fadeOutTime;
			ima.color = tempColor;

			fadeColor = tempColor.a;

			if (tempColor.a <= 0f) tempColor.a = 0f;

			ima.color = tempColor;

			if (nextEvent != null) nextEvent();

			yield return YieldInstructionCache.WaitForFixedUpdate;
		}
	}
	
}