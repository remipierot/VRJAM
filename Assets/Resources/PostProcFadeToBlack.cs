using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcFadeToBlack : MonoBehaviour {
	public Material mat;

	public bool IsFadedOut
	{
		get
		{
			return mat.GetFloat("_Fade") == 0.0f;
		}
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit (src, dest, mat);
		
	}

	public void FadeOut(float secondsInDark = 1.0f)
	{
		StartCoroutine(RunFadeOut(secondsInDark));
	}

	IEnumerator RunFadeOut(float secondsInDark)
	{
		float alpha = mat.GetFloat("_Fade");

		if(alpha == 1.0f)
		{
			do
			{
				alpha -= Time.deltaTime;
				alpha = Mathf.Clamp01(alpha);
				mat.SetFloat("_Fade", alpha);
				yield return null;
			} while (alpha > 0.0f);

			do
			{
				secondsInDark -= Time.deltaTime;
				yield return null;
			} while (secondsInDark > 0.0f);

			do
			{
				alpha += Time.deltaTime;
				alpha = Mathf.Clamp01(alpha);
				mat.SetFloat("_Fade", alpha);
				yield return null;
			} while (alpha < 1.0f);
		}

		yield return null;
	}
}
