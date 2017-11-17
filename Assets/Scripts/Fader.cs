using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour {
	private float m_Alpha = 0.0f;
	private int m_FadeDir = -1;
	private bool m_Loading = false;
	private float m_FadeSpeed = 1.0f;
	private Material m_FadeMaterial;

	public void Fade(int direction, float speed)
	{
		m_FadeDir = direction;
		m_FadeSpeed = speed;
		m_FadeMaterial = transform.GetComponent<MeshRenderer>().sharedMaterial;

		StartCoroutine(RunFade());
	}

	IEnumerator RunFade()
	{
		m_Alpha += m_FadeDir * m_FadeSpeed * Time.deltaTime;
		m_Alpha = Mathf.Clamp01(m_Alpha);
		Color c = Color.black;
		c.a = m_Alpha;

		m_FadeMaterial.SetColor("Color", c);
		transform.GetComponent<MeshRenderer>().material = m_FadeMaterial;

		yield return null;
	}
}
