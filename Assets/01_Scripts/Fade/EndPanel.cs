using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
	internal static class YieldInstructionCache
	{
		public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
	}

	public static float fadeColor = 0;

	// Start is called before the first frame update
	void Start()
    {
		StartCoroutine(CoFadeIn(3.0f, null));
	}

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
				yield break;
			}

			ima.color = tempColor;

			if (nextEvent != null) nextEvent();

			yield return YieldInstructionCache.WaitForFixedUpdate;
		}
	}
}
